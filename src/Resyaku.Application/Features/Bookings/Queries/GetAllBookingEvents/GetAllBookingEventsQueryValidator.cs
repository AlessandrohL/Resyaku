using FluentValidation;

namespace Resyaku.Application.Features.Bookings.Queries.GetAllBookingEvents
{
    public sealed class GetAllBookingEventsQueryValidator
        : AbstractValidator<GetAllBookingEventsQueryParams>
    {
        public GetAllBookingEventsQueryValidator()
        {
            RuleFor(q => q.StartDate)
                .NotEmpty().WithMessage("start date is required.");

            RuleFor(q => q.EndDate)
                .NotEmpty().WithMessage("end date is required");
        }
    }
}
