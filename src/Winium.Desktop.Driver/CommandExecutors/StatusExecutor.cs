using System.Collections.Generic;
using Winium.Desktop.Driver.CommandHelpers;
using Winium.StoreApps.Common;

namespace Winium.Desktop.Driver.CommandExecutors
{
    internal class StatusExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            var response = new Dictionary<string, object> { { "build", new BuildInfo() }, { "os", new OSInfo() } };
            return this.JsonResponse(ResponseStatus.Success, response);
        }
    }
}
