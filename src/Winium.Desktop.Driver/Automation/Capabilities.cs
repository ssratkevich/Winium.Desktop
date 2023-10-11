using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using Winium.Cruciatus.Settings;
using Winium.Desktop.Driver.CommandHelpers;

namespace Winium.Desktop.Driver.Automation
{
    internal class Capabilities
    {

        #region Constructors and Destructors

        internal Capabilities()
        {
            this.BrowserName = string.Empty;
            this.BrowserVersion = "11";
            this.App = string.Empty;
            this.PlatformName = "windows";
            this.AcceptInsecureCerts = false;
            this.PageLoadStrategy = string.Empty;
            this.SetWindowRect = string.Empty;
            this.TimeOutsValue = new TimeOuts(30000, 300000, 0);
            this.StrictFileInteractability = false;
            this.UnhandledPromptBehavior = string.Empty;
            this.WinDesktopOptions = new DesktopOptions(new object(), string.Empty, new object());
            //this.Arguments = string.Empty;
            this.LaunchDelay = 0;
            this.DebugConnectToRunningApp = false;
            this.InnerPort = 9998;
            this.KeyboardSimulator = KeyboardSimulatorType.BasedOnInputSimulatorLib;
        }

        #endregion

        #region Public Properties

        [JsonProperty("browserName")]
        public string BrowserName { get; set; }

        [JsonProperty("browserVersion")]
        public string BrowserVersion { get; set; }

        [JsonProperty("platformName")]
        public string PlatformName { get; set; }

        [JsonProperty("acceptInsecureCerts")]
        public bool AcceptInsecureCerts { get; set; }

        [JsonProperty("pageLoadStrategy")]
        public string PageLoadStrategy { get; set; }

        [JsonProperty("proxy")]
        public object ProxyValue { get; set; }
        public class Proxy
        {

            public Proxy()
            {


            }
            [JsonProperty("proxyType")]
            public string ProxyType { get; set; }



            [JsonProperty("proxyAutoconfigUrl")]
            public string ProxyAutoconfigUrl { get; set; }



            [JsonProperty("ftpProxy")]
            public string FtpProxy { get; set; }



            [JsonProperty("httpProxy")]
            public string HttpProxy { get; set; }



            [JsonProperty("noProxy")]
            public IList<string> NoProxy { get; set; }

            [JsonProperty("sslProxy")]
            public string SslProxy { get; set; }



            [JsonProperty("socksProxy")]
            public string SocksProxy { get; set; }



            [JsonProperty("socksVersion")]
            public int SocksVersion { get; set; }

        }

        [JsonProperty("setWindowRect")]
        public string SetWindowRect { get; set; }

        [JsonProperty("timeouts")]
        public object TimeOutsValue { get; set; }
        public class TimeOuts
        {
            public TimeOuts(int? script, int pageload, int implicity)
            {
                Script = script ?? 0;
                PageLoad = pageload;
                Implicit = implicity;
            }

            [JsonProperty("script")]
            public int Script { get; set; }

            [JsonProperty("pageload")]
            public int PageLoad { get; set; }

            [JsonProperty("implicit")]
            public int Implicit { get; set; }
        }

        [JsonProperty("strictFileInteractability")]
        public bool StrictFileInteractability { get; set; }

        [JsonProperty("unhandledPromptBehavior")]
        public string UnhandledPromptBehavior { get; set; }

        
        [JsonProperty("win:desktopOptions")]
        public object WinDesktopOptions { get; set; }

        public class DesktopOptions
        {
            public DesktopOptions(object args, string app, object extensions)
            {
                Args = args;
                App = app;
                Extensions = extensions;
            }

            [JsonProperty("args")]
            public object Args { get; set; }

            [JsonProperty("desktop:app")]
            public string App { get; set; }

            [JsonProperty("extensions")]
            public object Extensions { get; set; }

        }

        [JsonProperty("desktop:app")]
        public string App { get; set; }

        [JsonProperty("debugConnectToRunningApp")]
        public bool DebugConnectToRunningApp { get; set; }

        [JsonProperty("innerPort")]
        public int InnerPort { get; set; }

        [JsonProperty("keyboardSimulator")]
        public KeyboardSimulatorType KeyboardSimulator { get; set; }

        [JsonProperty("launchDelay")]
        public int LaunchDelay { get; set; }

        #endregion

        #region Public Methods and Operators

        public static Capabilities CapabilitiesFromJsonString(string jsonString)
        {
            var capabilities = JsonConvert.DeserializeObject<Capabilities>(
                jsonString,
                new JsonSerializerSettings
                {
                    Error =
                            delegate (object sender, ErrorEventArgs args)
                            {
                                args.ErrorContext.Handled = true;
                            }
                });

            return capabilities;
        }

        public string CapabilitiesToJsonString() => JsonConvert.SerializeObject(this);

        #endregion
    }
}
