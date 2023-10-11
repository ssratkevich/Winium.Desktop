using Winium.Cruciatus.Extensions;
using Winium.StoreApps.Common;
using Winium.StoreApps.Common.Exceptions;

namespace Winium.Desktop.Driver.CommandExecutors
{
    internal class FindComboBoxSelectedItemExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var comboBox = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey).ToComboBox();

            var selectedItem = comboBox.SelectedItem();
            if (selectedItem == null)
            {
                throw new AutomationException("No items is selected", ResponseStatus.NoSuchElement);
            }

            var selectedItemKey = this.Automator.ElementsRegistry.RegisterElement(selectedItem);
            var registeredObject = new JsonElementContent(selectedItemKey);

            return this.JsonResponse(ErrorCodes.Success, registeredObject);
        }
    }
}
