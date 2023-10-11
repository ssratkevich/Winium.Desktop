using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Winium.Cruciatus;
using Winium.Cruciatus.Settings;
using Winium.Desktop.Driver.Automation;
using Winium.Desktop.Driver.Input;
using Winium.StoreApps.Common;

namespace Winium.Desktop.Driver.CommandExecutors
{
    internal class NewSessionExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            var capabilitiesArray = JObject.Parse(this.ExecutedCommand.Parameters["capabilities"].ToString());

            var capabilityFirst = capabilitiesArray.First.First.Last;

            var serializedCapabilities =
                JsonConvert.SerializeObject(capabilityFirst);
            this.Automator.ActualCapabilities = Capabilities.CapabilitiesFromJsonString(serializedCapabilities);

            this.InitializeApplication(this.Automator.ActualCapabilities.DebugConnectToRunningApp);
            this.InitializeKeyboardEmulator(this.Automator.ActualCapabilities.KeyboardSimulator);

            // Gives sometime to load visuals (needed only in case of slow emulation)
            Thread.Sleep(this.Automator.ActualCapabilities.LaunchDelay);

            return this.JsonResponse(ErrorCodes.Success, this.Automator.ActualCapabilities);
        }

        private void InitializeApplication(bool debugDoNotDeploy = false)
        {
            string winDesk = JsonConvert.SerializeObject(this.Automator.ActualCapabilities.WinDesktopOptions);
            var winDesktopOptions = JsonConvert.DeserializeObject<Capabilities.DesktopOptions>(winDesk);
            var appPath = winDesktopOptions.App;
            var appArguments = winDesktopOptions.Args;

            this.Automator.Application = new Application(appPath);
            if (!debugDoNotDeploy)
            {
                this.Automator.Application.Start(appArguments.ToString());
            }
        }

        private void InitializeKeyboardEmulator(KeyboardSimulatorType keyboardSimulatorType)
        {
            this.Automator.WiniumKeyboard = new WiniumKeyboard(keyboardSimulatorType);

            Logger.Debug("Current keyboard simulator: {0}", keyboardSimulatorType);
        }
    }
}
