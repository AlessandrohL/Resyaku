using MediatR;
using Microsoft.EntityFrameworkCore;
using Resyaku.Application.Data;
using Resyaku.Application.Errors;
using Resyaku.Domain.Primitives;

namespace Resyaku.Application.Features.ServiceAreas.Commands.UpdateServiceArea
{
    public sealed class UpdateServiceAreaCommandHandler(IApplicationDbContext dbContext)
        : IRequestHandler<UpdateServiceAreaCommand, Result>
    {
        public async Task<Result> Handle(UpdateServiceAreaCommand request, CancellationToken cancellationToken)
        {
            var serviceArea = await dbContext
                .ServiceAreas
                .FirstOrDefaultAsync(sa => sa.ServiceAreaId == request.ServiceAreaId, CancellationToken.None);

            if (serviceArea is null)
            {
                return Result.Failure(ServiceAreaErrors.NotFound);
            }

            serviceArea.Name = request.Name;
            serviceArea.ModifiedOnUtc = DateTime.UtcNow;

            await dbContext.SaveChangesAsync(CancellationToken.None);

            return Result.Success();
        }
    }
}
