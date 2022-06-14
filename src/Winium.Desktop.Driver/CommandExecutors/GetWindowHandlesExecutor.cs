extern alias UIAComWrapper;
using System.Linq;
using Winium.Cruciatus;
using Winium.Cruciatus.Core;
using Winium.Cruciatus.Extensions;
using Winium.StoreApps.Common;
using Automation = UIAComWrapper::System.Windows.Automation;

namespace Winium.Desktop.Driver.CommandExecutors
{
    internal class GetWindowHandlesExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            var windows = 
                CruciatusFactory.Root
                    .FindElements(
                        By.AutomationProperty(
                            Automation::AutomationElement.ControlTypeProperty,
                            Automation::ControlType.Window));

            var handles =
                windows.Select(element => 
                    element.GetAutomationPropertyValue<int>(
                        Automation::AutomationElement.NativeWindowHandleProperty));

            return this.JsonResponse(ResponseStatus.Success, handles);
        }
    }
}
