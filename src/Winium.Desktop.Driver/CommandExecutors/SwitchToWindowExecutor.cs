extern alias UIAComWrapper;
using Winium.Cruciatus;
using Winium.Cruciatus.Core;
using Winium.StoreApps.Common;
using Winium.StoreApps.Common.Exceptions;
using Automation = UIAComWrapper::System.Windows.Automation;

namespace Winium.Desktop.Driver.CommandExecutors
{
    internal class SwitchToWindowExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            var windowHandle = int.Parse(this.ExecutedCommand.Parameters["name"].ToString());

            var window =
                CruciatusFactory.Root
                    .FindElement(
                        By.AutomationProperty(
                            Automation::AutomationElement.NativeWindowHandleProperty,
                            windowHandle));
            if (window is null)
            {
                throw new AutomationException("Window cannot be found", ResponseStatus.NoSuchElement);
            }

            window.SetFocus();

            return this.JsonResponse();
        }
    }
}
