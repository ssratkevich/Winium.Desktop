extern alias UIAComWrapper;
using System;
using Winium.Cruciatus.Core;
using Automation = UIAComWrapper::System.Windows.Automation;

namespace Winium.Desktop.Driver.Extensions
{
    /// <summary>
    /// Element search strategy helper.
    /// </summary>
    public static class ByHelper
    {
        #region Public Methods and Operators

        /// <summary>
        /// Get desired element search strategy by it's name.
        /// </summary>
        /// <param name="strategy">Strategy name.</param>
        /// <param name="value">Search value.</param>
        /// <returns>Search element strategy.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public static By GetStrategy(string strategy, string value)
        {
            switch (strategy)
            {
                case "id":
                    return By.Uid(value);
                case "name":
                    return By.Name(value);
                case "class name":
                    return By.AutomationProperty(Automation::AutomationElementIdentifiers.ClassNameProperty, value);
                case "xpath":
                    return By.XPath(value);
                default:
                    throw new NotImplementedException(
                        string.Format("'{0}' is not valid or implemented searching strategy.", strategy));
            }
        }

        #endregion
    }
}
