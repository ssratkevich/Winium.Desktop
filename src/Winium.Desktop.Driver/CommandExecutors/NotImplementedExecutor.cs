using System;

namespace Winium.Desktop.Driver.CommandExecutors
{
    internal class NotImplementedExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            throw new NotImplementedException($"'{this.ExecutedCommand.Name}' is not valid or implemented command.");
        }
    }
}
