using Resyaku.Domain.Primitives;
using MediatR;
using Resyaku.Application.Data.Repositories;
using Resyaku.Application.Data.UnitOfWorks;
using Resyaku.Domain.Entities;

namespace Resyaku.Application.Features.ServiceAreas.Commands.CreateServiceArea
{
    public record CreateServiceAreaCommand(string Name) : IRequest<Result>;

    public sealed class CreateServiceAreaCommandHandler(
        IServiceAreaRepository serviceAreaRepository,
        IUnitOfWork unitOfWork)
        : IRequestHandler<CreateServiceAreaCommand, Result>
    {
        public async Task<Result> Handle(CreateServiceAreaCommand request, CancellationToken cancellationToken)
        {
            var newServiceArea = new ServiceArea(request.Name);

            serviceAreaRepository.Add(newServiceArea);

            await unitOfWork.SaveChangesAsync(CancellationToken.None);

            return Result.Success();
        }
    }
}
