using Microsoft.EntityFrameworkCore;
using Resyaku.Application.Data.Repositories;
using Resyaku.Application.Features.ServiceAreas.Queries.GetAllServiceAreas;
using Resyaku.Application.Mapper;
using Resyaku.Domain.Entities;

namespace Resyaku.Infrastructure.Data.Repositories
{
    public sealed class ServiceAreaRepository(ApplicationDbContext dbContext) : IServiceAreaRepository
    {
        public async Task<List<GetAllServiceAreasDto>> GetAllServiceAreasAsync()
        {
            return await dbContext.ServiceAreas
                .AsNoTracking()
                .Select(sa => sa.ToAllServiceAreaDto())
                .ToListAsync();
        }

        public async Task<ServiceArea?> GetByIdAsync(int serviceAreaId)
        {
            return await dbContext.ServiceAreas.FindAsync(serviceAreaId);
        }
        
        public async Task<bool> ExistsByIdAsync(int serviceAreaId)
        {
            return await dbContext.ServiceAreas.AnyAsync(sa => sa.ServiceAreaId == serviceAreaId);
        }

        public void Add(ServiceArea serviceArea)
        {
            dbContext.Add(serviceArea);
        }
    }
}
