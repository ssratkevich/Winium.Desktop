﻿using Winium.StoreApps.Common;

namespace Winium.Desktop.Driver.CommandExecutors
{
    internal class IsElementDisplayedExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            return this.JsonResponse(ResponseStatus.Success, !element.Properties.IsOffscreen);
        }
    }
}
