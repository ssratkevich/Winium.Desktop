using Winium.Cruciatus.Extensions;
using Winium.StoreApps.Common;
using Winium.StoreApps.Common.Exceptions;

namespace Winium.Desktop.Driver.CommandExecutors
{
    internal class FindMenuItemExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            var dataGridKey = this.ExecutedCommand.Parameters["ID"].ToString();
            var headersPath = this.ExecutedCommand.Parameters["PATH"].ToString();

            var munu = this.Automator.ElementsRegistry.GetRegisteredElement(dataGridKey).ToMenu();

            var element = munu.GetItem(headersPath);
            if (element == null)
            {
                throw new AutomationException("No menu item was found", ResponseStatus.NoSuchElement);
            }

            var elementKey = this.Automator.ElementsRegistry.RegisterElement(element);

            return this.JsonResponse(ErrorCodes.Success, new JsonElementContent(elementKey));
        }
    }
}
