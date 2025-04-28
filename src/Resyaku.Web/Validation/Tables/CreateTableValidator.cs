using FluentValidation;
using Resyaku.Web.ViewModels.Tables;

namespace Resyaku.Web.Validation.Tables
{
    public sealed class CreateTableValidator : AbstractValidator<CreateTableViewModel>
    {
        public CreateTableValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                    .WithMessage("El nombre de la mesa es obligatorio.")
                .MaximumLength(30)
                    .WithMessage("El nombre de la mesa no puede exceder los 30 caracteres.");

            RuleFor(p => p.MinCapacity)
                .NotEmpty()
                    .WithMessage("La capacidad mínima es obligatoria.")
                .GreaterThan(0)
                    .WithMessage("La capacidad mínima debe ser mayor que 0.")
                .LessThanOrEqualTo(255)
                    .WithMessage("La capacidad mínima no puede ser mayor a 255.");

            RuleFor(p => p.MaxCapacity)
                .NotEmpty()
                    .WithMessage("La capacidad máxima es obligatoria.")
                .GreaterThan(0)
                    .WithMessage("La capacidad máxima debe ser mayor que 0.")
                .LessThanOrEqualTo(255)
                    .WithMessage("La capacidad máxima no puede ser mayor a 255.");

            RuleFor(p => p.ServiceAreaId)
                .NotEmpty()
                    .WithMessage("El área de servicio es obligatorio.")
                .NotEqual(0)
                    .WithMessage("El área de servicio es obligatorio.");
        }
    }
}
