using Resyaku.Application.Features.Bookings.Commands.CreateBooking;
using Resyaku.Web.ViewModels.Bookings;

namespace Resyaku.Web.Mappers
{
    public static class BookingMappers
    {
        public static CreateBookingCommand ToCreateBookingCommand(this CreateBookingViewModel viewModel)
        {
            return new CreateBookingCommand(
                BookingDate: viewModel.BookingDate,
                BookingTime: viewModel.BookingTime,
                Duration: viewModel.Duration,
                GuestCount: viewModel.GuestCount,
                BookingStatus: viewModel.BookingStatus,
                PrivateComment: viewModel.PrivateComment,
                PublicComment: viewModel.PublicComment,
                CustomerName: viewModel.CustomerName,
                CustomerLastname: viewModel.CustomerLastname,
                CustomerPhone: viewModel.CustomerPhone,
                CustomerEmail: viewModel.CustomerEmail,
                CustomerDni: viewModel.CustomerDni,
                TableIds: viewModel.TableIds
                .Split(',')
                .Select(int.Parse));
        }
    }
}
