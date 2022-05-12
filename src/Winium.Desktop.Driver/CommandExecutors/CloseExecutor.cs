namespace Winium.Desktop.Driver.CommandExecutors
{
    internal class CloseExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            this.Automator.Close();
            return this.JsonResponse();
        }
    }
}
