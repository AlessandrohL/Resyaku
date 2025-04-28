using Resyaku.Application.Features.Tables.Commands.CreateTable;
using Resyaku.Application.Features.Tables.Commands.UpdateTable;
using Resyaku.Web.ViewModels.Tables;

namespace Resyaku.Web.Mapper
{
    public static class TableMappers
    {
        public static CreateTableCommand ToCreateTableCommand(this CreateTableViewModel viewModel)
        {
            return new CreateTableCommand(
                viewModel.Name,
                viewModel.MinCapacity,
                viewModel.MaxCapacity,
                viewModel.ServiceAreaId,
                viewModel.IsActive);
        }

        public static UpdateTableCommand ToUpdateTableCommand(
            this UpdateTableViewModel viewModel,
            int tableId)
        {
            return new UpdateTableCommand(
                tableId,
                viewModel.Name,
                viewModel.MinCapacity,
                viewModel.MaxCapacity,
                viewModel.SelectedServiceAreaId,
                viewModel.IsActive);
        }
    }
}
