using Resyaku.Application.DTOs.ServiceAreas;
using Resyaku.Domain.Entities;

namespace Resyaku.Application.Mapper;

public static class ServiceAreaMapper
{
    public static ServiceAreaSummaryDto ToServiceAreaSummary(this ServiceArea serviceArea)
    {
        return new ServiceAreaSummaryDto(serviceArea.ServiceAreaId, serviceArea.Name);
    }
}
