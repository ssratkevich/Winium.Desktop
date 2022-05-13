using System.Collections.Generic;
using System.Linq;
using System.Windows.Automation;
using Newtonsoft.Json.Linq;
using Winium.Cruciatus.Core;
using Winium.Cruciatus.Elements;
using Winium.Cruciatus.Extensions;
using Winium.StoreApps.Common;
using Winium.StoreApps.Common.Exceptions;

namespace Winium.Desktop.Driver.CommandExecutors
{
    internal class ExecuteScriptExecutor : CommandExecutorBase
    {
        #region Constants

        internal const string HelpArgumentsErrorMsg = "Arguments error. See {0} for more information.";

        internal const string HelpUnknownScriptMsg = "Unknown script command '{0} {1}'. See {2} for supported commands.";

        internal const string HelpUrlAutomationScript =
            "https://github.com/ssratkevich/Winium.Desktop/wiki/Command-Execute-Script#use-ui-automation-patterns-on-element";

        internal const string HelpUrlInputScript =
            "https://github.com/ssratkevich/Winium.Desktop/wiki/Command-Execute-Script#simulate-input";

        internal const string HelpUrlScript = "https://github.com/ssratkevich/Winium.Desktop/wiki/Command-Execute-Script";

        #endregion

        #region Methods

        protected override string DoImpl()
        {
            var script = this.ExecutedCommand.Parameters["script"].ToString();

            var prefix = string.Empty;
            string command;

            var index = script.IndexOf(':');
            if (index == -1)
            {
                command = script;
            }
            else
            {
                prefix = script.Substring(0, index);
                command = script.Substring(++index).Trim();
            }

            switch (prefix)
            {
                case "input":
                    this.ExecuteInputScript(command);
                    break;
                case "automation":
                    this.ExecuteAutomationScript(command);
                    break;
                default:
                    var msg = string.Format(HelpUnknownScriptMsg, prefix, command, HelpUrlScript);
                    throw new AutomationException(msg, ResponseStatus.JavaScriptError);
            }

            return this.JsonResponse();
        }

        private void ExecuteInputScript(string command)
        {
            var args = (JArray) this.ExecutedCommand.Parameters["args"];
            var element = this.TryGetElement(args[0]);

            switch (command)
            {
                case "ctrl_click":
                    element.ClickWithPressedCtrl();
                    return;
                case "brc_click":
                    element.Click(MouseButton.Left, ClickStrategies.BoundingRectangleCenter);
                    return;
                default:
                    var msg = string.Format(HelpUnknownScriptMsg, "input:", command, HelpUrlInputScript);
                    throw new AutomationException(msg, ResponseStatus.JavaScriptError);
            }
        }

        private void ExecuteAutomationScript(string command)
        {
            var args = (JArray)this.ExecutedCommand.Parameters["args"];
            var element = this.TryGetElement(args[0]);

            switch (command)
            {
                case "ValuePattern.SetValue":
                    this.ValuePatternSetValue(element, args);
                    break;
                case "ScrollItemPattern.ScrollIntoView":
                case "ScrollIntoView":
                    element.ScrollIntoView(this.TryGetElement(args.ElementAtOrDefault(1)));
                    break;
                case "SelectionItemPattern.Select":
                    element.GetPattern<SelectionItemPattern>(SelectionItemPattern.Pattern).Select();
                    break;
                case "ExpandCollapsePattern.Expand":
                    ExpandCollapse(element, args);
                    break;
                case "SetFocus":
                    element.SetFocus();
                    break;
                default:
                    var msg = string.Format(HelpUnknownScriptMsg, "automation:", command, HelpUrlAutomationScript);
                    throw new AutomationException(msg, ResponseStatus.JavaScriptError);
            }
        }

        private void ValuePatternSetValue(CruciatusElement element, IEnumerable<JToken> args)
        {
            var value = args.ElementAtOrDefault(1);
            if (value == null)
            {
                var msg = string.Format(HelpArgumentsErrorMsg, HelpUrlAutomationScript);
                throw new AutomationException(msg, ResponseStatus.JavaScriptError);
            }

            element.GetPattern<ValuePattern>(ValuePattern.Pattern).SetValue(value.ToString());
        }

        private void ExpandCollapse(CruciatusElement element, IEnumerable<JToken> args)
        {
            bool expand = true;
            var value = args.ElementAtOrDefault(1);
            if (value != null)
            {
                bool.TryParse(value.ToString(), out expand);
            }
            var pattern = element.GetPattern<ExpandCollapsePattern>(ExpandCollapsePattern.Pattern);
            if (expand)
            {
                pattern.Expand();
            }
            else
            {
                pattern.Collapse();
            }
        }

        #endregion
    }
}
