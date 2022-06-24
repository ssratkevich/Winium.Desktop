using System;
using Winium.Cruciatus;
using Winium.StoreApps.Common;
using Winium.StoreApps.Common.Exceptions;

namespace Winium.Desktop.Driver.CommandExecutors
{
    internal class SetTimeoutExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            var timeout = this.ExecutedCommand.Parameters["ms"];
            var type = this.ExecutedCommand.Parameters["type"];

            if (type.ToString() != "implicit")
            {
                throw new AutomationException($"Unsupported timeout type \"{type}\".", ResponseStatus.UnknownCommand);
            }

            CruciatusFactory.Settings.SearchTimeout = Convert.ToInt32(timeout);

            return this.JsonResponse();
        }
    }
}
