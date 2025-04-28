using Resyaku.Application.Features.ServiceAreas.Commands.CreateServiceArea;
using Resyaku.Application.Features.ServiceAreas.Commands.UpdateServiceArea;
using Resyaku.Web.ViewModels.ServiceAreas;

namespace Resyaku.Web.Mapper
{
    public static class ServiceAreaMappers
    {
        public static CreateServiceAreaCommand ToCreateServiceAreaCommand(
            this CreateServiceAreaViewModel viewModel)
        {
            return new CreateServiceAreaCommand(viewModel.Name);
        }

        public static UpdateServiceAreaCommand ToUpdateServiceAreaCommand(
            this UpdateServiceAreaViewModel viewModel,
            int serviceAreaId)
        {
            return new UpdateServiceAreaCommand(serviceAreaId, viewModel.Name);
        }
    }
}
