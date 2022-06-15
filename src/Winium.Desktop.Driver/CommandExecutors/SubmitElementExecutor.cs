using Winium.Cruciatus;

namespace Winium.Desktop.Driver.CommandExecutors
{
    internal class SubmitElementExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            CruciatusFactory.Keyboard.SendEnter();
            return this.JsonResponse();
        }
    }
}
