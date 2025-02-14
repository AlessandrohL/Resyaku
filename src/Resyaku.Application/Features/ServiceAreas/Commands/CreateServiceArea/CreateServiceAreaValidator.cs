using FluentValidation;

namespace Resyaku.Application.Features.ServiceAreas.Commands.CreateServiceArea
{
    public sealed class CreateServiceAreaValidator : AbstractValidator<CreateServiceAreaCommand>
    {
        public CreateServiceAreaValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .WithMessage("El nombre no puede estar vacío.")
                .MaximumLength(50)
                .WithMessage("El nombre no puede tener más de 50 caracteres.");
        }
    }
}
