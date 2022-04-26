using System.Collections.Generic;
using System.Diagnostics;

namespace Winium.Desktop.Driver.CommandHelpers
{
    public static class TerminateApp
    {
        public static void TerminateExcecutor(Automator.Automator automator)
        {
            var automator = (Automator.Automator) automatorObject;
            if (!automator.ActualCapabilities.DebugConnectToRunningApp)
            {
                // If application had exited, find and terminate all children processes
                if (automator.Application.HasExited())
                {
                    List<Process> children = new List<Process>();
                    children = automator.Application.GetChildPrecesses(automator.Application.GetProcessId());
                    foreach (var child in children)
                    {
                        if (!child.HasExited && !automator.Application.Close(child))
                        {
                            automator.Application.Kill(child);
                        }
                    }
                }

                // If application is still running, terminate it as normal case
                else if (!automator.Application.Close())
                {
                    automator.Application.Kill();
                }

                automator.ElementsRegistry.Clear();
            }
        }
    }
}
