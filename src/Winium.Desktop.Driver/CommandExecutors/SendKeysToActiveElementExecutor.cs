namespace Winium.Desktop.Driver.CommandExecutors
{
    #region using

    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    internal class SendKeysToActiveElementExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var chars = new List<char>();
            foreach (var token in this.ExecutedCommand.Parameters["value"])
            {
                var str = token.ToString();
                if (string.IsNullOrEmpty(str))
                {
                    continue;
                }
                if (str.Length == 1)
                {
                    chars.Add(Convert.ToChar(str));
                }
                else
                {
                    chars.AddRange(str.ToCharArray());
                }
            }
            //var chars = this.ExecutedCommand.Parameters["value"].Select(x => Convert.ToChar(x.ToString()));

            this.Automator.WiniumKeyboard.SendKeys(chars.ToArray());

            return this.JsonResponse();
        }

        #endregion
    }
}
