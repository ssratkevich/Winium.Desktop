using System;
using Winium.Cruciatus;
using Winium.Cruciatus.Core;
using Winium.StoreApps.Common;

namespace Winium.Desktop.Driver.CommandExecutors
{
    internal class MouseClickExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            var buttonId = Convert.ToInt32(this.ExecutedCommand.Parameters["button"]);

            switch ((MouseButton)buttonId)
            {
                case MouseButton.Left:
                    CruciatusFactory.Mouse.LeftButtonClick();
                    break;

                case MouseButton.Right:
                    CruciatusFactory.Mouse.RightButtonClick();
                    break;

                default:
                    return this.JsonResponse(ErrorCodes.UnknownCommand, "Mouse button behavior is not implemented");
            }

            return this.JsonResponse();
        }
    }
}
