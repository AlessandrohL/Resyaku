using MediatR;
using Microsoft.EntityFrameworkCore;
using Resyaku.Application.Data;
using Resyaku.Application.Errors;
using Resyaku.Domain.Entities;
using Resyaku.Domain.Primitives;

namespace Resyaku.Application.Features.Tables.Commands.CreateTable
{
    public sealed class CreateTableCommandHandler(
        IApplicationDbContext dbContext)
        : IRequestHandler<CreateTableCommand, Result>
    {
        public async Task<Result> Handle(CreateTableCommand request, CancellationToken cancellationToken)
        {
            var serviceArea = await dbContext
                .ServiceAreas
                .FirstOrDefaultAsync(sa => sa.ServiceAreaId == request.ServiceAreaId, CancellationToken.None);

            if (serviceArea is null)
            {
                return Result.Failure(TableErrors.NotFound);
            }

            var newTable = Table.Create(
                name: request.Name,
                minCapacity: request.MinCapacity,
                maxCapacity: request.MaxCapacity,
                serviceArea: serviceArea,
                isActive: request.IsActive,
                rowUlid: Ulid.NewUlid().ToString());

            dbContext.Tables.Add(newTable);

            await dbContext.SaveChangesAsync(CancellationToken.None);

            return Result.Success();
        }
    }
}
