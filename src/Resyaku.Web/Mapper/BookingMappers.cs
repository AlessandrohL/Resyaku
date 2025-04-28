using Resyaku.Application.Features.Bookings.Commands.CreateBooking;
using Resyaku.Web.ViewModels.Bookings;

namespace Resyaku.Web.Mapper
{
    public static class BookingMappers
    {
        public static CreateBookingCommand ToCreateBookingCommand(this CreateBookingViewModel viewModel)
        {
            return new CreateBookingCommand(
                viewModel.BookingDate,
                viewModel.BookingTime,
                viewModel.Duration,
                viewModel.PartySize,
                viewModel.BookingStatus,
                viewModel.PrivateComment,
                viewModel.PublicComment,
                viewModel.CustomerName,
                viewModel.CustomerLastname,
                viewModel.CustomerPhone,
                viewModel.CustomerEmail,
                viewModel.CustomerDni,
                viewModel.TableIds
                    .Split(',')
                    .Select(int.Parse));
        }
    }
}
