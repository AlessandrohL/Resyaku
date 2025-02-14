using FluentValidation;
using Resyaku.Web.ViewModels.ServiceAreas;

namespace Resyaku.Web.Validation.ServiceAreas
{
    public sealed class UpdateServiceAreaValidator : AbstractValidator<UpdateServiceAreaViewModel>
    {
        public UpdateServiceAreaValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .WithMessage("El nombre no puede estar vacío.")
                .MaximumLength(50)
                .WithMessage("El nombre no puede tener más de 50 caracteres.");
        }
    }
}
