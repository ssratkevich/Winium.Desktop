extern alias UIAComWrapper;
using Winium.Cruciatus.Exceptions;
using Winium.Cruciatus.Extensions;
using Interop.UIAutomationClient;
using Winium.StoreApps.Common;
using Automation = UIAComWrapper::System.Windows.Automation;

namespace Winium.Desktop.Driver.CommandExecutors
{
    internal class IsElementSelectedExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.TryGetElement(registeredKey);

            bool isSelected;

            try
            {
                isSelected = element.GetAutomationPropertyValue<bool>(Automation::SelectionItemPattern.IsSelectedProperty);
            }
            catch (CruciatusException)
            {
                var toggleState = element.GetAutomationPropertyValue<ToggleState>(Automation::TogglePattern.ToggleStateProperty);

                isSelected = toggleState == ToggleState.ToggleState_On;
            }

            return this.JsonResponse(ResponseStatus.Success, isSelected);
        }
    }
}
