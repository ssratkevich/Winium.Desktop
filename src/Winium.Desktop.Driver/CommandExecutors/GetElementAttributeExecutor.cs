extern alias UIAComWrapper;
using System;
using Winium.Cruciatus.Extensions;
using Winium.Desktop.Driver.Extensions;
using Winium.StoreApps.Common;
using Automation = UIAComWrapper::System.Windows.Automation;

namespace Winium.Desktop.Driver.CommandExecutors
{
    internal class GetElementAttributeExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();
            var propertyName = this.ExecutedCommand.Parameters["NAME"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            try
            {
                var property = AutomationPropertyHelper.GetAutomationProperty(propertyName);
                var propertyObject = element.GetAutomationPropertyValue<object>(property);

                return this.JsonResponse(ResponseStatus.Success, PrepareValueToSerialize(propertyObject));
            }
            catch (Exception)
            {
                return this.JsonResponse();
            }
        }

        /* Known types:
         * string, bool, int - should be as plain text
         * System.Windows.Automation.ControlType - should be used `ProgrammaticName` property
         * System.Window.Rect, System.Window.Point - overrides `ToString()` method, can serialize
         */
        private static object PrepareValueToSerialize(object obj)
        {
            if (obj is null)
            {
                return null;
            }

            if (obj.GetType().IsPrimitive)
            {
                return obj.ToString();
            }

            var controlType = obj as Automation::ControlType;
            if (controlType is not null)
            {
                return controlType.ProgrammaticName;
            }

            return obj;
        }
    }
}
