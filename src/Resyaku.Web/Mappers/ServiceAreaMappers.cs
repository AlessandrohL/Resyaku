using Resyaku.Application.Features.ServiceAreas.Commands.UpdateServiceArea;
using Resyaku.Application.Features.ServiceAreas.Queries.GetServiceAreaById;
using Resyaku.Web.ViewModels.ServiceAreas;

namespace Resyaku.Web.Mappers
{
    public static class ServiceAreaMappers
    {
        public static UpdateServiceAreaViewModel ToUpdateServiceAreaViewModel(this GetServiceAreaByIdDto serviceAreaDto)
        {
            return new UpdateServiceAreaViewModel(serviceAreaDto.Id, serviceAreaDto.Name);
        }

        public static UpdateServiceAreaCommand ToUpdateServiceAreaCommand(this UpdateServiceAreaViewModel viewModel)
        {
            return new UpdateServiceAreaCommand(viewModel.ServiceAreaId, viewModel.Name);
        }
    }
}
