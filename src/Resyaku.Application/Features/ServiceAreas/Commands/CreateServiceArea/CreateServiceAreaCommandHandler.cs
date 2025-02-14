using Resyaku.Domain.Primitives;
using Resyaku.Application.Data;
using MediatR;

namespace Resyaku.Application.Features.ServiceAreas.Commands.CreateServiceArea
{
    public sealed class CreateServiceAreaCommandHandler(
        IApplicationDbContext dbContext)
        : IRequestHandler<CreateServiceAreaCommand, Result>
    {
        public async Task<Result> Handle(CreateServiceAreaCommand request, CancellationToken cancellationToken)
        {
            var newServiceArea = Domain.Entities.ServiceArea.Create(request.Name, Ulid.NewUlid().ToString());

            dbContext.ServiceAreas.Add(newServiceArea);

            await dbContext.SaveChangesAsync(CancellationToken.None);

            return Result.Success();
        }
    }
}
