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
            // It is easier to reparse desired capabilities as JSON instead of re-mapping keys to attributes and calling type conversions, 
            // so we will take possible one time performance hit by serializing Dictionary and deserializing it as Capabilities object
            if (this.ExecutedCommand.Parameters.TryGetValue("desiredCapabilities", out var token))
            {
            }
            else if (this.ExecutedCommand.Parameters.TryGetValue("capabilities", out token))
            {
                token = token["firstMatch"][0];
            }
            var serializedCapability = JsonConvert.SerializeObject(token);
            this.Automator.ActualCapabilities = Capabilities.CapabilitiesFromJsonString(serializedCapability);

            this.InitializeApplication(this.Automator.ActualCapabilities.DebugConnectToRunningApp);
            this.InitializeKeyboardEmulator(this.Automator.ActualCapabilities.KeyboardSimulator);

            // Gives sometime to load visuals (needed only in case of slow emulation)
            Thread.Sleep(this.Automator.ActualCapabilities.LaunchDelay);

            return this.JsonResponse(ResponseStatus.Success, this.Automator.ActualCapabilities);
        }

        private void InitializeApplication(bool debugDoNotDeploy = false)
        {
            var appPath = this.Automator.ActualCapabilities.App;
            var appArguments = this.Automator.ActualCapabilities.Arguments;

            this.Automator.Application = new Application(appPath);
            if (!debugDoNotDeploy)
            {
                this.Automator.Application.Start(appArguments);
            }
        }

        private void InitializeKeyboardEmulator(KeyboardSimulatorType keyboardSimulatorType)
        {
            this.Automator.WiniumKeyboard = new WiniumKeyboard(keyboardSimulatorType);

            Logger.Debug("Current keyboard simulator: {0}", keyboardSimulatorType);
        }
    }
}
