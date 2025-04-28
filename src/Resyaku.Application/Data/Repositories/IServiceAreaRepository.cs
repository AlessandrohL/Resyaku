using Resyaku.Application.DTOs.ServiceAreas;
using Resyaku.Domain.Entities;

namespace Resyaku.Application.Data.Repositories
{
    public interface IServiceAreaRepository
    {
        Task<List<ServiceAreaSummaryDto>> GetAllServiceAreasAsync();
        Task<ServiceArea?> GetByIdAsync(int serviceAreaId);
        Task<bool> ExistsByIdAsync(int serviceAreaId);
        void Add(ServiceArea serviceArea);
    }
}
