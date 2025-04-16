using Resyaku.Application.Features.ServiceAreas.Queries.GetAllServiceAreas;
using Resyaku.Application.Features.ServiceAreas.Queries.GetServiceAreaById;
using Resyaku.Domain.Entities;

namespace Resyaku.Application.Mapper
{
    public static class ServiceAreaMapper
    {
        public static GetAllServiceAreasDto ToAllServiceAreaDto(this ServiceArea serviceArea)
        {
            return new GetAllServiceAreasDto
            {
                Id = serviceArea.ServiceAreaId,
                Name = serviceArea.Name
            };
        }

        public static GetServiceAreaByIdDto ToServiceAreaByIdDto(this ServiceArea serviceArea)
        {
            return new GetServiceAreaByIdDto
            {
                Id = serviceArea.ServiceAreaId,
                Name = serviceArea.Name
            };
        }
    }
}
