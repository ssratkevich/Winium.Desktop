using Winium.Cruciatus.Extensions;

namespace Winium.Desktop.Driver.CommandExecutors
{
    internal class CollapseComboBoxExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey).ToComboBox().Collapse();

            return this.JsonResponse();
        }
    }
}
