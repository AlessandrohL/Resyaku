using Resyaku.Domain.Primitives;
using MediatR;
using Resyaku.Application.Data.Repositories;
using Resyaku.Application.Data.UnitOfWorks;
using Resyaku.Application.Errors;

namespace Resyaku.Application.Features.ServiceAreas.Commands.UpdateServiceArea
{
    public record UpdateServiceAreaCommand(int ServiceAreaId, string Name) : IRequest<Result>;

    public sealed class UpdateServiceAreaCommandHandler(
        IServiceAreaRepository serviceAreaRepository,
        IUnitOfWork unitOfWork)
        : IRequestHandler<UpdateServiceAreaCommand, Result>
    {
        public async Task<Result> Handle(UpdateServiceAreaCommand request, CancellationToken cancellationToken)
        {
            var existingServiceArea = await serviceAreaRepository.GetByIdAsync(request.ServiceAreaId);

            if (existingServiceArea is null)
            {
                return Result.Failure(ServiceAreaErrors.NotFound);
            }

            existingServiceArea.Name = request.Name;

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
