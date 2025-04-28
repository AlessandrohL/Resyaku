using System.Transactions;
using MediatR;
using Resyaku.Application.Abstractions.Services;
using Resyaku.Application.Data.Repositories;
using Resyaku.Application.Data.UnitOfWorks;
using Resyaku.Application.Errors;
using Resyaku.Domain.Abstractions;
using Resyaku.Domain.Entities;
using Resyaku.Domain.Enums;
using Resyaku.Domain.Primitives;

namespace Resyaku.Application.Features.Bookings.Commands.CreateBooking
{
    public record CreateBookingCommand(
        DateOnly BookingDate,
        string BookingStartTime,
        int Duration,
        int PartySize,
        BookingStatus BookingStatus,
        string? PrivateComment,
        string? PublicComment,
        string CustomerName,
        string CustomerLastname,
        string CustomerPhone,
        string CustomerEmail,
        string CustomerDni,
        IEnumerable<int> TableIds
        ) : IRequest<Result<string>>;

    public sealed class CreateBookingCommandHandler(
        IBookingRepository bookingRepository,
        ITableRepository tableRepository,
        ICustomerRepository customerRepository,
        IBookingSettingsService bookingSettingsService,
        IBookingReferenceProvider bookingReferenceProvider,
        IUnitOfWork unitOfWork)
        : IRequestHandler<CreateBookingCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            var bookingStartTime = TimeOnly.Parse(request.BookingStartTime);
            var bookingSettings = await bookingSettingsService.RetrieveSettingsAsync();

            if (!IsBookingTimeValid(bookingStartTime, bookingSettings.DailyOpeningTime, bookingSettings.DailyClosingTime))
            {
                return Result.Failure<string>(BookingErrors.InvalidTime);
            }

            if (await tableRepository.GetTotalCapacityAsync(request.TableIds) < request.PartySize)
            {
                return Result.Failure<string>(BookingErrors.NotEnoughCapacity);
            }

            var bookingEndTime = bookingStartTime.AddMinutes(request.Duration);

            bool allTablesAvailable = await tableRepository.AreTablesAvailableAsync(
                    request.TableIds,
                    request.BookingDate,
                    bookingStartTime,
                    bookingEndTime);

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

                        var newCustomer = new Customer(
                            request.CustomerName,
                            request.CustomerLastname,
                            request.CustomerPhone,
                            request.CustomerEmail,
                            request.CustomerDni);

                        customerRepository.Add(newCustomer);

                        await unitOfWork.SaveChangesAsync(cancellationToken);

                        customer = newCustomer;
                    }

                    var newBooking = new Booking(
                        bookingReferenceProvider.Create(),
                        customer.CustomerId,
                        request.BookingDate,
                        bookingStartTime,
                        request.Duration,
                        request.PartySize,
                        request.BookingStatus,
                        request.PrivateComment,
                        request.PublicComment,
                        request.CustomerPhone);

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

                    transactionScope.Complete();

                    return Result.Success(newBooking.Reference);
                }
                catch (Exception ex)
                {
                    // TODO: Logging exception
                    return Result.Failure<string>(BookingErrors.CreationFailed);
                }
            }
        }

        private static bool IsBookingTimeValid(
            TimeOnly bookingStartTime, 
            TimeOnly openingTime, 
            TimeOnly closingTime)
        {
            return bookingStartTime >= openingTime && bookingStartTime < closingTime;
        }
    }
}
