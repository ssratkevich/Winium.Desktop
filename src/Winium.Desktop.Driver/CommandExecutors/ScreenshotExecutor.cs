using Winium.Cruciatus;
using Winium.StoreApps.Common;

namespace Winium.Desktop.Driver.CommandExecutors
{
    internal class ScreenshotExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            var screenshot = CruciatusFactory.Screenshoter.GetScreenshot();
            var screenshotSource = screenshot.AsBase64String();

            return this.JsonResponse(ErrorCodes.Success, screenshotSource);
        }
    }
}
