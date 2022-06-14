using Winium.Cruciatus.Core;
using Winium.StoreApps.Common;

namespace Winium.Desktop.Driver.CommandExecutors
{
    internal class GetOrientationExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            var orientation = RotationManager.GetCurrentOrientation();

            return this.JsonResponse(ResponseStatus.Success, orientation.ToString());
        }
    }
}