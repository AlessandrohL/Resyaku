using MediatR;
using Resyaku.Application.Data.Repositories;
using Resyaku.Application.Data.UnitOfWorks;
using Resyaku.Application.Errors;
using Resyaku.Domain.Primitives;

namespace Resyaku.Application.Features.ServiceAreas.Commands.UpdateServiceArea
{
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
            existingServiceArea.ModifiedOnUtc = DateTime.UtcNow;

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
