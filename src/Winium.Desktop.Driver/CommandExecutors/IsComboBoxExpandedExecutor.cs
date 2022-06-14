using Winium.Cruciatus.Extensions;
using Winium.StoreApps.Common;

namespace Winium.Desktop.Driver.CommandExecutors
{
    internal class IsComboBoxExpandedExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            return this.JsonResponse(ResponseStatus.Success, element.ToComboBox().IsExpanded);
        }
    }
}
