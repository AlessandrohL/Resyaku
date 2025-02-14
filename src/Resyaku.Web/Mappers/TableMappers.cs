using Resyaku.Application.Features.Tables.Commands.UpdateTable;
using Resyaku.Application.Features.Tables.Queries.GetTableById;
using Resyaku.Web.ViewModels.Tables;

namespace Resyaku.Web.Mappers
{
    public static class TableMappers
    {
        public static UpdateTableViewModel ToUpdateTableViewModel(this GetTableByIdDto tableDto)
        {
            return new UpdateTableViewModel(
                tableId: tableDto.TableId,
                name: tableDto.Name,
                minCapacity: tableDto.MinCapacity,
                maxCapacity: tableDto.MaxCapacity,
                serviceAreaId: tableDto.ServiceAreaId,
                isActive: tableDto.IsActive);
        }

        public static UpdateTableCommand ToUpdateTableCommand(this UpdateTableViewModel viewModel)
        {
            return new UpdateTableCommand(
                viewModel.TableId,
                viewModel.Name,
                viewModel.MinCapacity,
                viewModel.MaxCapacity,
                viewModel.ServiceAreaId,
                viewModel.IsActive);
        }
    }
}
