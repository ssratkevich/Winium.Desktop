extern alias UIAComWrapper;
using System.Globalization;
using Winium.StoreApps.Common;
using Automation = UIAComWrapper::System.Windows.Automation;

namespace Winium.Desktop.Driver.CommandExecutors
{
    internal class GetCurrentWindowHandleExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            var node = Automation::AutomationElement.FocusedElement;
            var rootElement = Automation::AutomationElement.RootElement;
            var treeWalker = Automation::TreeWalker.ControlViewWalker;
            while (node != rootElement && !node.Current.ControlType.Equals(Automation::ControlType.Window))
            {
                node = treeWalker.GetParent(node);
            }

            var result = (node == rootElement)
                             ? string.Empty
                             : node.Current.NativeWindowHandle.ToString(CultureInfo.InvariantCulture);
            return this.JsonResponse(ErrorCodes.Success, result);
        }
    }
}