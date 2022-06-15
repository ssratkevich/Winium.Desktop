using Winium.Cruciatus;
using Winium.StoreApps.Common;

namespace Winium.Desktop.Driver.CommandExecutors
{
    internal class GetActiveElementExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            var registeredKey = this.Automator.ElementsRegistry.RegisterElement(CruciatusFactory.FocusedElement);
            var registeredObject = new JsonElementContent(registeredKey);
            return this.JsonResponse(ResponseStatus.Success, registeredObject);
        }
    }
}
