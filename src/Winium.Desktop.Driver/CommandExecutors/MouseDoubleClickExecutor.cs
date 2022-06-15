using Winium.Cruciatus;
using Winium.Cruciatus.Core;

namespace Winium.Desktop.Driver.CommandExecutors
{
    internal class MouseDoubleClickExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            CruciatusFactory.Mouse.DoubleClick(MouseButton.Left);
            return this.JsonResponse();
        }
    }
}
