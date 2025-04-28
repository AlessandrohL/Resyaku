using FluentValidation;
using Resyaku.Web.ViewModels.Users;

namespace Resyaku.Web.Validation.Users
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserViewModel>
    {
        private const string NamesRegex = @"^[^\s\d][a-zA-Z\s]*$";
        private const string PhoneRegex = @"^9\d{8}$";
        private const string UsernameRegex = @"^[a-zA-Z0-9\-._@+]+$";
        private const string PasswordRegex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[A-Za-z\d\S]{8,}$";

        public UpdateUserValidator()
        {
            RuleFor(p => p.Firstname)
                .NotEmpty()
                    .WithMessage("El nombre es obligatorio.")
                .Matches(NamesRegex)
                    .WithMessage("El nombre contiene caracteres no permitidos.")
                .MaximumLength(50)
                    .WithMessage("El nombre no puede tener más de 50 caracteres.");

            When(p => !string.IsNullOrWhiteSpace(p.Lastname), () =>
            {
                RuleFor(p => p.Lastname)
                    .Matches(NamesRegex)
                        .WithMessage("El apellido contiene caracteres no permitidos.")
                    .MaximumLength(50)
                        .WithMessage("El apellido no puede tener más de 50 caracteres.");
            });

            RuleFor(p => p.Phone)
                .NotEmpty()
                    .WithMessage("El número de teléfono es obligatorio.")
                .Length(9)
                    .WithMessage("El número de teléfono debe tener 9 dígitos.")
                .Matches(PhoneRegex)
                    .WithMessage("El número de teléfono es inválido.");

            RuleFor(p => p.Username)
                .NotEmpty()
                    .WithMessage("El nombre de usuario es obligatorio.")
                .Matches(UsernameRegex)
                    .WithMessage("El nombre de usuario contiene caracteres no permitidos.");

            RuleFor(p => p.Email)
                .NotEmpty()
                    .WithMessage("El correo electrónico es obligatorio.")
                .EmailAddress()
                    .WithMessage("El correo electrónico no tiene un formato válido.");

            When(p => p.ChangePassword, () =>
            {
                RuleFor(p => p.NewPassword)
                .NotEmpty()
                    .WithMessage("La contraseña es obligatoria.")
                .Matches(PasswordRegex)
                    .WithMessage("La contraseña debe tener al menos 8 caracteres, una letra minúscula, una letra mayúscula y un dígito.");
            });

            RuleFor(p => p.SelectedRoles)
                .NotEmpty()
                    .WithMessage("Debe seleccionar al menos un rol.");
        }
    }
}
