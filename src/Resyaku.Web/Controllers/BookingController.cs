using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Resyaku.Application.DTOs.Bookings;
using Resyaku.Application.Features.Bookings.Commands.CreateBooking;
using Resyaku.Application.Features.Bookings.Queries.GetAllBookingEvents;
using Resyaku.Application.Features.Bookings.Queries.GetAllBookings;
using Resyaku.Application.Features.Bookings.Queries.GetBookingAvailability;
using Resyaku.Web.Extensions;
using Resyaku.Web.Mapper;
using Resyaku.Web.ViewModels.Bookings;

namespace Resyaku.Web.Controllers
{
    [Route("bookings")]
    public sealed class BookingController(
        ISender sender,
        IValidator<CreateBookingViewModel> createBookingValidator) : Controller
    {
        public async Task<IActionResult> Index(
            [FromQuery] GetAllBookingsQueryParams queryParameters,
            CancellationToken cancellationToken)
        {
            var pagedBookings = await sender.Send(new GetAllBookingsQuery(queryParameters), cancellationToken);
            var bookingsViewModel = new GetAllBookingsViewModel(queryParameters, pagedBookings);

            return View(bookingsViewModel);
        }

        [HttpGet("calendar")]
        public IActionResult Calendar()
        {
            return View();
        }

        [Route("create")]
        public async Task<IActionResult> CreateBooking()
        {
            var bookingAvailability = await sender.Send(new GetBookingAvailabilityQuery());
            var model = new CreateBookingViewModel(bookingAvailability);

            return View(model);
        }

        [HttpPost]
        [Route("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBooking(
            [FromForm] CreateBookingViewModel model,
            CancellationToken cancellationToken)
        {
            var validationResult = await createBookingValidator.ValidateAsync(model, cancellationToken);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);

                var bookingAvailability = await sender.Send(new GetBookingAvailabilityQuery(), cancellationToken);
                model.SetBookingAvailability(bookingAvailability);

                return View(model);
            }

            CreateBookingCommand command = model.ToCreateBookingCommand();
            var creationBookingResult = await sender.Send(command, cancellationToken);

            if (creationBookingResult.IsFailure)
            {
                var bookingAvailability = await sender.Send(new GetBookingAvailabilityQuery(), cancellationToken);
                model.SetBookingAvailability(bookingAvailability);

                ViewData["Booking.CreationFailure"] = creationBookingResult.Error.Description;

                return View(model);
            }

            string bookingRef = creationBookingResult.Value;
            TempData["Booking.Created"] = $"La reserva se creó correctamente. Código de referencia: {bookingRef}";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("events")]
        public async Task<IActionResult> GetAllBookingEvents(
            [FromQuery] GetAllBookingEventsQueryParams queryParams,
            CancellationToken cancellationToken)
        {
            List<BookingEventDto> bookings = await sender.Send(new GetAllBookingEventsQuery(queryParams));

            return Ok(bookings);
        }
    }
}
