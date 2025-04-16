using Resyaku.Application.Features.ServiceAreas.Queries.GetAllServiceAreas;
using Resyaku.Domain.Entities;

namespace Resyaku.Application.Data.Repositories
{
    public interface IServiceAreaRepository
    {
        Task<List<GetAllServiceAreasDto>> GetAllServiceAreasAsync();
        Task<ServiceArea?> GetByIdAsync(int serviceAreaId);
        Task<bool> ExistsByIdAsync(int serviceAreaId);
        void Add(ServiceArea serviceArea);
    }
}
