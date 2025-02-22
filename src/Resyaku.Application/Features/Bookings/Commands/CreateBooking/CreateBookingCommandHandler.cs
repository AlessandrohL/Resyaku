using MediatR;
using Microsoft.EntityFrameworkCore;
using NanoidDotNet;
using Resyaku.Application.Data;
using Resyaku.Application.Errors;
using Resyaku.Application.Features.ReservationSettings.Services;
using Resyaku.Domain.Entities;
using Resyaku.Domain.Primitives;

namespace Resyaku.Application.Features.Bookings.Commands.CreateBooking
{
    public sealed class CreateBookingCommandHandler(
        IApplicationDbContext dbContext,
        IBookingSettingsService bookingSettingsService)
        : IRequestHandler<CreateBookingCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            TimeSpan bookingStartTime = TimeSpan.Parse(request.BookingTime);
            var bookingSettings = await bookingSettingsService.GetBookingPreferencesAsync(cancellationToken);

            if (!IsBookingTimeValid(bookingStartTime, bookingSettings.DailyOpeningTime, bookingSettings.DailyClosingTime))
            {
                return Result.Failure<string>(BookingErrors.InvalidTime);
            }

            if (!await HasSufficientCapacity(request.GuestCount, request.TableIds, cancellationToken))
            {
                return Result.Failure<string>(BookingErrors.NotEnoughCapacity);
            }

            bool allTablesAvailable = await AreTablesAvailable(
                    tableIds: request.TableIds,
                    bookingDate: request.BookingDate,
                    startTime: bookingStartTime,
                    duration: request.Duration,
                    cancellationToken);

            if (!allTablesAvailable)
            {
                return Result.Failure<string>(BookingErrors.TablesUnavailable);
            }

            int customerId = await dbContext
                .Customers
                .Where(c => c.Dni == request.CustomerDni)
                .Select(c => c.CustomerId)
                .FirstOrDefaultAsync(cancellationToken);

            using var transaction = await dbContext.BeginTransactionAsync();

            try
            {
                if (customerId == 0)
                {
                    if (await dbContext.Customers.AnyAsync(c => c.Email == request.CustomerEmail, cancellationToken))
                    {
                        return Result.Failure<string>(CustomerErrors.EmailAlreadyInUse);
                    }

                    var newCustomer = Customer.Create(
                        name: request.CustomerName,
                        lastname: request.CustomerLastname,
                        phone: request.CustomerPhone,
                        email: request.CustomerEmail,
                        dni: request.CustomerDni,
                        rowUlid: Ulid.NewUlid().ToString());

                    dbContext.Customers.Add(newCustomer);
                    await dbContext.SaveChangesAsync(cancellationToken);

                    customerId = newCustomer.CustomerId;
                }

                var referenceCode = Nanoid.Generate(Nanoid.Alphabets.LettersAndDigits, 10);

                var newBooking = Booking.Create(
                    bookingReferenceCode: referenceCode,
                    customerId: customerId,
                    bookingDate: request.BookingDate,
                    bookingStartTime: bookingStartTime,
                    durationInMinutes: request.Duration,
                    guestCount: request.GuestCount,
                    bookingStatus: request.BookingStatus,
                    privateComment: request.PrivateComment,
                    publicComment: request.PublicComment,
                    contactPhone: request.CustomerPhone,
                    isWalking: false,
                    rowUlid: Ulid.NewUlid().ToString());

                foreach (var tableId in request.TableIds)
                {
                    var bookingTable = new BookingTable
                    {
                        Booking = newBooking,
                        TableId = tableId
                    };
                    newBooking.BookingTables.Add(bookingTable);
                }

                dbContext.Bookings.Add(newBooking);

                await dbContext.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);

                return Result.Success(referenceCode);
            }
            catch (Exception)
            {
                await transaction.RollbackAsync(cancellationToken);
                return Result.Failure<string>(BookingErrors.CreationFailed);
            }
        }

        private async Task<bool> AreTablesAvailable(
            IEnumerable<int> tableIds,
            DateTime bookingDate,
            TimeSpan startTime,
            int duration,
            CancellationToken cancellationToken)
        {
            var bookingEndDateTime = bookingDate.Add(startTime).AddMinutes(duration);

            return await dbContext.Tables
                .Where(t => tableIds.Contains(t.TableId))
                .AllAsync(t => !t.Bookings.Any(b =>
                    b.Status != Domain.Enums.BookingStatus.Cancelled &&
                    b.BookingDate == bookingDate &&
                    b.BookingTime < bookingEndDateTime.TimeOfDay &&
                    b.EndTime.TimeOfDay > startTime), cancellationToken);
        }

        private static bool IsBookingTimeValid(TimeSpan bookingStartTime, TimeSpan openingTime, TimeSpan closingTime)
        {
            return bookingStartTime >= openingTime && bookingStartTime < closingTime;
        }

        private async Task<bool> HasSufficientCapacity(int guestCount, IEnumerable<int> tableIds, CancellationToken cancellationToken)
        {
            int totalCapacity = await dbContext.Tables
                .Where(t => tableIds.Contains(t.TableId))
                .SumAsync(t => t.MaxCapacity, cancellationToken);

            return guestCount <= totalCapacity;
        }
    }
}
