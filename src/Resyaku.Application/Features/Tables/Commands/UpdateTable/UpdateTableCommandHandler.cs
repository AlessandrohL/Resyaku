using MediatR;
using Resyaku.Application.Data.Repositories;
using Resyaku.Application.Data.UnitOfWorks;
using Resyaku.Application.Errors;
using Resyaku.Domain.Primitives;

namespace Resyaku.Application.Features.Tables.Commands.UpdateTable
{
    public sealed class UpdateTableCommandHandler(
        ITableRepository tableRepository,
        IServiceAreaRepository serviceAreaRepository,
        IUnitOfWork unitOfWork)
        : IRequestHandler<UpdateTableCommand, Result>
    {
        public async Task<Result> Handle(UpdateTableCommand request, CancellationToken cancellationToken)
        {
            var table = await tableRepository.GetByIdAsync(request.TableId);

            if (table is null)
            {
                return Result.Failure(TableErrors.NotFound);
            }

            if (!await serviceAreaRepository.ExistsByIdAsync(request.ServiceAreaId))
            {
                return Result.Failure(ServiceAreaErrors.NotFound);
            }

            table.Name = request.Name;
            table.MinCapacity = request.MinCapacity;
            table.MaxCapacity = request.MaxCapacity;
            table.IsActive = request.IsActive;
            table.ServiceAreaId = request.ServiceAreaId;
            table.ModifiedOnUtc = DateTime.UtcNow;

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
