using Winium.Cruciatus.Extensions;
using Winium.StoreApps.Common;

namespace Winium.Desktop.Driver.CommandExecutors
{
    internal class GetDataGridColumnCountExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var dataGrid = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey).ToDataGrid();

            return this.JsonResponse(ErrorCodes.Success, dataGrid.ColumnCount);
        }
    }
}
