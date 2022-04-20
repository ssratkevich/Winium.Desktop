using System;
using System.Collections.Generic;
using System.Linq;

namespace Winium.Desktop.Driver.CommandExecutors
{
    internal class SendKeysToActiveElementExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var chars = this.ExecutedCommand.Parameters["value"]
                .SelectMany(x => x.ToString().ToCharArray());

            this.Automator.WiniumKeyboard.SendKeys(chars.ToArray());

            return this.JsonResponse();
        }

        #endregion
    }
}
