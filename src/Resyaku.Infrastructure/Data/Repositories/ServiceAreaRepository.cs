using Microsoft.EntityFrameworkCore;
using Resyaku.Application.Data.Repositories;
using Resyaku.Application.DTOs.ServiceAreas;
using Resyaku.Domain.Entities;

namespace Resyaku.Infrastructure.Data.Repositories
{
    public sealed class ServiceAreaRepository(ApplicationDbContext dbContext) : IServiceAreaRepository
    {
        public async Task<List<ServiceAreaSummaryDto>> GetAllServiceAreasAsync()
        {
            return await dbContext.ServiceAreas
                .AsNoTracking()
                .Select(sa => new ServiceAreaSummaryDto(sa.ServiceAreaId, sa.Name))
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
