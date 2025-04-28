using FluentValidation;

namespace Resyaku.Infrastructure.Authentication.Features.Users.Queries.GetUserById
{
    public sealed class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
    {
        public GetUserByIdQueryValidator()
        {
            RuleFor(p => p.UserId)
                .NotEmpty()
                .WithMessage("El id no puede estar vacío.")
                .Must(p => Guid.TryParse(p, out _))
                .WithMessage("El id ingresado tiene un formato inválido.");
        }

    }
}
