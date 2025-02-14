using MediatR;
using Microsoft.EntityFrameworkCore;
using Resyaku.Application.Data;
using Resyaku.Application.Errors;
using Resyaku.Domain.Primitives;

namespace Resyaku.Application.Features.Tables.Commands.UpdateTable
{
    public sealed class UpdateTableCommandHandler(IApplicationDbContext dbContext)
        : IRequestHandler<UpdateTableCommand, Result>
    {
        public async Task<Result> Handle(UpdateTableCommand request, CancellationToken cancellationToken)
        {
            var table = await dbContext
                .Tables
                .FirstOrDefaultAsync(t => t.TableId == request.TableId, CancellationToken.None);

            if (table is null)
            {
                return Result.Failure(TableErrors.NotFound);
            }

            bool serviceAreaExists = await dbContext
                .ServiceAreas
                .AnyAsync(sa => sa.ServiceAreaId == request.ServiceAreaId, CancellationToken.None);

            if (!serviceAreaExists)
            {
                return Result.Failure(ServiceAreaErrors.NotFound);
            }

            table.Name = request.Name;
            table.MinCapacity = request.MinCapacity;
            table.MaxCapacity = request.MaxCapacity;
            table.IsActive = request.IsActive;
            table.ServiceAreaId = request.ServiceAreaId;
            table.ModifiedOnUtc = DateTime.UtcNow;

            await dbContext.SaveChangesAsync(CancellationToken.None);

            return Result.Success();
        }
    }
}
