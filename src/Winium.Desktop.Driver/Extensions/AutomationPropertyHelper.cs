extern alias UIAComWrapper;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Winium.Cruciatus;
using Winium.Cruciatus.Exceptions;
using Automation = UIAComWrapper::System.Windows.Automation;

namespace Winium.Desktop.Driver.Extensions
{
    internal static class AutomationPropertyHelper
    {
        #region Static Fields

        private static readonly Dictionary<string, Automation::AutomationProperty> Properties = new();

        #endregion

        #region Constructors and Destructors

        static AutomationPropertyHelper()
        {
            var assembly = typeof(Automation::AutomationElementIdentifiers).Assembly;
            foreach(var type in assembly.GetTypes())
            {
                if (!type.Name.EndsWith("Identifiers"))
                {
                    continue;
                }
                foreach(var property in type
                    .GetFields(BindingFlags.Public | BindingFlags.Static)
                    .Where(f => f.FieldType == typeof(Automation::AutomationProperty)))
                {
                    Properties[property.Name] = (Automation::AutomationProperty)property.GetValue(null);
                }
            }
            //Properties =
            //    typeof(AutomationElementIdentifiers).GetFields(BindingFlags.Public | BindingFlags.Static)
            //        .Where(f => f.FieldType == typeof(AutomationProperty))
            //        .ToDictionary(f => f.Name, f => (AutomationProperty)f.GetValue(null));
        }

        #endregion

        #region Public Methods and Operators

        internal static Automation::AutomationProperty GetAutomationProperty(string propertyName)
        {
            const string Suffix = "Property";
            var fullPropertyName = propertyName.EndsWith(Suffix) ? propertyName : $"{propertyName}{Suffix}";
            if (Properties.ContainsKey(fullPropertyName))
            {
                return Properties[fullPropertyName];
            }

            CruciatusFactory.Logger.Error(string.Format("Property '{0}' is not UI Automation Property", propertyName));
            throw new CruciatusException("UNSUPPORTED PROPERTY");
        }

        #endregion
    }
}
