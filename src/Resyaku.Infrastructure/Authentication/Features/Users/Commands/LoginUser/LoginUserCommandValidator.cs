using FluentValidation;

namespace Resyaku.Infrastructure.Authentication.Features.Users.Commands.LoginUser
{
    public sealed class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {

        public LoginUserCommandValidator()
        {
            RuleFor(u => u.Username)
                .NotEmpty()
                .WithMessage("El nombre de usuario no puede estar vacío.");

            RuleFor(u => u.Password)
                .NotEmpty()
                .WithMessage("La contraseña no puede estar vacía.");
        }
    }
}
