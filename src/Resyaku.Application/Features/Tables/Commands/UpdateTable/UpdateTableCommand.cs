using MediatR;
using Resyaku.Application.Data.Repositories;
using Resyaku.Application.Data.UnitOfWorks;
using Resyaku.Application.Errors;
using Resyaku.Domain.Primitives;

namespace Resyaku.Application.Features.Tables.Commands.UpdateTable
{
    public record UpdateTableCommand(
        int TableId,
        string Name,
        int MinCapacity,
        int MaxCapacity,
        int ServiceAreaId,
        bool IsActive) : IRequest<Result>;

    public sealed class UpdateTableCommandHandler(
        ITableRepository tableRepository,
        IServiceAreaRepository serviceAreaRepository,
        IUnitOfWork unitOfWork)
        : IRequestHandler<UpdateTableCommand, Result>
    {
        public async Task<Result> Handle(UpdateTableCommand request, CancellationToken cancellationToken)
        {
            var existingTable = await tableRepository.GetByIdAsync(request.TableId);

            if (existingTable is null)
            {
                return Result.Failure(TableErrors.NotFound);
            }

            if (!await serviceAreaRepository.ExistsByIdAsync(request.ServiceAreaId))
            {
                return Result.Failure(ServiceAreaErrors.NotFound);
            }

            existingTable.Name = request.Name;
            existingTable.MinCapacity = request.MinCapacity;
            existingTable.MaxCapacity = request.MaxCapacity;
            existingTable.IsActive = request.IsActive;
            existingTable.ServiceAreaId = request.ServiceAreaId;

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
