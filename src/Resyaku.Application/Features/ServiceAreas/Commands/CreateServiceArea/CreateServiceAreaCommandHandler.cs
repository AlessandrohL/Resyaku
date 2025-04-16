using MediatR;
using Resyaku.Application.Data.Repositories;
using Resyaku.Application.Data.UnitOfWorks;
using Resyaku.Domain.Entities;
using Resyaku.Domain.Primitives;

namespace Resyaku.Application.Features.ServiceAreas.Commands.CreateServiceArea
{
    public sealed class CreateServiceAreaCommandHandler(
        IServiceAreaRepository serviceAreaRepository,
        IUnitOfWork unitOfWork)
        : IRequestHandler<CreateServiceAreaCommand, Result>
    {
        public async Task<Result> Handle(CreateServiceAreaCommand request, CancellationToken cancellationToken)
        {
            var newServiceArea = ServiceArea.Create(request.Name, Ulid.NewUlid().ToString());

            serviceAreaRepository.Add(newServiceArea);
            await unitOfWork.SaveChangesAsync(CancellationToken.None);

            return Result.Success();
        }
    }
}
