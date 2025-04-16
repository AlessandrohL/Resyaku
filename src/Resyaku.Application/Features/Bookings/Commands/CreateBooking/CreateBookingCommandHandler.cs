using System.Transactions;
using MediatR;
using NanoidDotNet;
using Resyaku.Application.Data.Repositories;
using Resyaku.Application.Data.UnitOfWorks;
using Resyaku.Application.Errors;
using Resyaku.Application.Features.ReservationSettings.Services;
using Resyaku.Domain.Entities;
using Resyaku.Domain.Primitives;

namespace Resyaku.Application.Features.Bookings.Commands.CreateBooking
{
    public sealed class CreateBookingCommandHandler(
        IBookingRepository bookingRepository,
        ITableRepository tableRepository,
        ICustomerRepository customerRepository,
        IBookingSettingsService bookingSettingsService,
        IUnitOfWork unitOfWork)
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

            if (await tableRepository.GetTotalCapacityAsync(request.TableIds) < request.GuestCount)
            {
                return Result.Failure<string>(BookingErrors.NotEnoughCapacity);
            }

            bool allTablesAvailable = await tableRepository.AreTablesAvailableAsync(
                    tableIds: request.TableIds,
                    bookingDate: request.BookingDate,
                    startTime: bookingStartTime,
                    duration: request.Duration);
                
            if (!allTablesAvailable)
            {
                return Result.Failure<string>(BookingErrors.TablesUnavailable);
            }

            var customer = await customerRepository.GetCustomerByDniAsync(request.CustomerDni);

            using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    if (customer is null)
                    {
                        if (await customerRepository.ExistsByEmailAsync(request.CustomerEmail))
                        {
                            return Result.Failure<string>(CustomerErrors.EmailAlreadyInUse);
                        }

                        var newCustomer = Customer.Create(
                            request.CustomerName,
                            request.CustomerLastname,
                            request.CustomerPhone,
                            request.CustomerEmail,
                            request.CustomerDni,
                            Ulid.NewUlid().ToString());

                        customerRepository.Add(newCustomer);

                        await unitOfWork.SaveChangesAsync(cancellationToken);

                        customer = newCustomer;
                    }

                    var referenceCode = Nanoid.Generate(Nanoid.Alphabets.LettersAndDigits, 10);

                    var newBooking = Booking.Create(
                        referenceCode,
                        customer.CustomerId,
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

                    bookingRepository.Add(newBooking);

                    await unitOfWork.SaveChangesAsync(cancellationToken);

                    return Result.Success(referenceCode);
                }
                catch (Exception ex)
                {
                    return Result.Failure<string>(BookingErrors.CreationFailed);
                }
            }
        }

        private static bool IsBookingTimeValid(TimeSpan bookingStartTime, TimeSpan openingTime, TimeSpan closingTime)
        {
            return bookingStartTime >= openingTime && bookingStartTime < closingTime;
        }
    }
}
