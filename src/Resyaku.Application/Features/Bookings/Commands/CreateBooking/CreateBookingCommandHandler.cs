using Resyaku.Application.Data;
using Resyaku.Application.Errors;
using Resyaku.Domain.Entities;
using Resyaku.Domain.Primitives;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NanoidDotNet;

namespace Resyaku.Application.Features.Bookings.Commands.CreateBooking
{
    public sealed class CreateBookingCommandHandler(IApplicationDbContext dbContext)
        : IRequestHandler<CreateBookingCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            int customerId = await dbContext
                .Customers
                .Where(c => c.Dni == request.CustomerDni)
                .Select(c => c.CustomerId)
                .FirstOrDefaultAsync(CancellationToken.None);

            using var transaction = await dbContext.BeginTransactionAsync();

            try
            {
                if (customerId == 0)
                {
                    if (await dbContext.Customers.AnyAsync(c => c.Email == request.CustomerEmail))
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
                    await dbContext.SaveChangesAsync();

                    customerId = newCustomer.CustomerId;
                }

                TimeSpan bookingStartTime = TimeSpan.Parse(request.BookingTime);
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

                var referenceCode = Nanoid.Generate(Nanoid.Alphabets.LettersAndDigits, 10);

                var newBooking = Booking.Create(
                    referenceCode,
                    customerId,
                    request.BookingDate,
                    bookingStartTime,
                    request.Duration,
                    request.GuestCount,
                    request.BookingStatus,
                    request.PrivateComment,
                    request.PublicComment,
                    request.CustomerPhone,
                    false,
                    Ulid.NewUlid().ToString());

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

                await dbContext.SaveChangesAsync(CancellationToken.None);
                await transaction.CommitAsync(CancellationToken.None);

                return Result.Success(referenceCode);
            }
            catch (Exception)
            {
                await transaction.RollbackAsync(CancellationToken.None);
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
    }
}
