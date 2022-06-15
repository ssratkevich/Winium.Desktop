using Winium.Cruciatus.Extensions;
using Winium.StoreApps.Common;

namespace Winium.Desktop.Driver.CommandExecutors
{
    internal class GetDataGridRowCountExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var dataGrid = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey).ToDataGrid();

            return this.JsonResponse(ResponseStatus.Success, dataGrid.RowCount);
        }
    }
}
