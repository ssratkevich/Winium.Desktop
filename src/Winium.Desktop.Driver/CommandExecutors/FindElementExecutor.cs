using Winium.Cruciatus;
using Winium.Desktop.Driver.Extensions;
using Winium.StoreApps.Common;
using Winium.StoreApps.Common.Exceptions;

namespace Winium.Desktop.Driver.CommandExecutors
{
    internal class FindElementExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            var searchValue = this.ExecutedCommand.Parameters["value"].ToString();
            var searchStrategy = this.ExecutedCommand.Parameters["using"].ToString();

            var strategy = ByHelper.GetStrategy(searchStrategy, searchValue);
            var element = CruciatusFactory.Root.FindElement(strategy);
            if (element == null)
            {
                throw new AutomationException("Element cannot be found", ResponseStatus.NoSuchElement);
            }

            var registeredKey = this.Automator.ElementsRegistry.RegisterElement(element);
            var registeredObject = new JsonElementContent(registeredKey);
            return this.JsonResponse(ResponseStatus.Success, registeredObject);
        }
    }
}
