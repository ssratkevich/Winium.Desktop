using Winium.Cruciatus.Exceptions;
using Winium.Cruciatus.Extensions;
using Winium.StoreApps.Common;

namespace Winium.Desktop.Driver.CommandExecutors
{
    internal class SelectMenuItemExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            var dataGridKey = this.ExecutedCommand.Parameters["ID"].ToString();
            var headersPath = this.ExecutedCommand.Parameters["PATH"].ToString();

            var menu = this.Automator.ElementsRegistry.GetRegisteredElement(dataGridKey).ToMenu();

            try
            {
                menu.SelectItem(headersPath);
            }
            catch (CruciatusException exception)
            {
                return this.JsonResponse(ErrorCodes.NoSuchElement, exception);
            }

            return this.JsonResponse();
        }
    }
}
