using System.Collections.Generic;
using System.Diagnostics;
using Winium.Cruciatus;
using Winium.Desktop.Driver.Input;

namespace Winium.Desktop.Driver.Automation
{
    internal class Automator
    {
        #region Static Fields

        private static readonly object LockObject = new object();

        private static volatile Automator instance;

        #endregion

        #region Constructors and Destructors

        public Automator(string session)
        {
            this.Session = session;
            this.ElementsRegistry = new ElementsRegistry();
        }

        #endregion

        #region Public Properties

        public Capabilities ActualCapabilities { get; set; }

        public Application Application { get; set; }

        public ElementsRegistry ElementsRegistry { get; private set; }

        public string Session { get; private set; }

        public WiniumKeyboard WiniumKeyboard { get; set; }

        #endregion

        #region Public Methods and Operators

        public static T GetValue<T>(IReadOnlyDictionary<string, object> parameters, string key) where T : class
        {
            object valueObject;
            parameters.TryGetValue(key, out valueObject);

            return valueObject as T;
        }

        public static Automator InstanceForSession(string sessionId)
        {
            if (instance == null)
            {
                lock (LockObject)
                {
                    if (instance == null)
                    {
                        if (sessionId == null)
                        {
                            sessionId = "AwesomeSession";
                        }

                        // TODO: Add actual support for sessions. Temporary return single Automator for any season
                        instance = new Automator(sessionId);
                    }
                }
            }

            return instance;
        }

        /// <summary>
        /// Close application.
        /// </summary>
        public void CloseApplication()
        {
            if (this.ActualCapabilities.DebugConnectToRunningApp)
            {
                return;
            }

            // If application had exited, find and terminate all children processes
            if (this.Application.HasExited())
            {
                List<Process> children = new List<Process>();
                children = this.Application.GetChildPrecesses(this.Application.GetProcessId());
                foreach (var child in children)
                {
                    if (!child.HasExited && !this.Application.Close(child))
                    {
                        this.Application.Kill(child);
                    }
                }
            }

            // If application is still running, terminate it as normal case
            else if (!this.Application.Close())
            {
                this.Application.Kill();
            }
        }

        /// <summary>
        /// Close session.
        /// </summary>
        public void Close()
        {
            this.CloseApplication();
            this.ElementsRegistry.Clear();
        }

        #endregion
    }
}
