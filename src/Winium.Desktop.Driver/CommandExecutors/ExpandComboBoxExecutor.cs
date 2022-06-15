using Winium.Cruciatus.Extensions;

namespace Winium.Desktop.Driver.CommandExecutors
{
    internal class ExpandComboBoxExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey).ToComboBox().Expand();

            return this.JsonResponse();
        }
    }
}
