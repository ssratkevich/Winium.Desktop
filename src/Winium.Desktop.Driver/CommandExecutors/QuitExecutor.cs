using Winium.Desktop.Driver.CommandHelpers;

namespace Winium.Desktop.Driver.CommandExecutors
{
    internal class QuitExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            TerminateApp.TerminateExcecutor(this.Automator);
            return this.JsonResponse();
        }
    }
}
