using Resyaku.Application.Data.UnitOfWorks;

namespace Resyaku.Infrastructure.Data.UnitOfWorks
{
    public sealed class UnitOfWork(ApplicationDbContext dbContext) : IUnitOfWork
    {
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
