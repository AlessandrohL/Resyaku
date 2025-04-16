using MediatR;
using Resyaku.Application.Data.Repositories;
using Resyaku.Application.Data.UnitOfWorks;
using Resyaku.Application.Errors;
using Resyaku.Domain.Entities;
using Resyaku.Domain.Primitives;

namespace Resyaku.Application.Features.Tables.Commands.CreateTable
{
    public sealed class CreateTableCommandHandler(
        IServiceAreaRepository serviceAreaRepository,
        ITableRepository tableRepository,
        IUnitOfWork unitOfWork)
        : IRequestHandler<CreateTableCommand, Result>
    {
        public async Task<Result> Handle(CreateTableCommand request, CancellationToken cancellationToken)
        {
            if (!await serviceAreaRepository.ExistsByIdAsync(request.ServiceAreaId))
            {
                return Result.Failure(TableErrors.NotFound);
            }

            var newTable = Table.Create(
                name: request.Name,
                minCapacity: request.MinCapacity,
                maxCapacity: request.MaxCapacity,
                serviceAreaId: request.ServiceAreaId,
                isActive: request.IsActive,
                rowUlid: Ulid.NewUlid().ToString());

            tableRepository.Add(newTable);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
