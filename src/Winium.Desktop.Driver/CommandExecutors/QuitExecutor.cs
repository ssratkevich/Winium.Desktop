namespace Winium.Desktop.Driver.CommandExecutors
{
    internal class QuitExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            this.Automator.Close();
            return this.JsonResponse();
        }
    }
}
