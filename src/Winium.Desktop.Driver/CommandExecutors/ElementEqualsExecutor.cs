using Winium.StoreApps.Common;

namespace Winium.Desktop.Driver.CommandExecutors
{
    internal class ElementEqualsExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();
            var otherRegisteredKey = this.ExecutedCommand.Parameters["other"].ToString();

            return this.JsonResponse(ErrorCodes.Success, registeredKey == otherRegisteredKey);
        }
    }
}
