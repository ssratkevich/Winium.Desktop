using System;
using System.Collections.Generic;
using System.Reflection;
using Winium.StoreApps.Common;

namespace Winium.Desktop.Driver
{
    /// <summary>
    /// Uri template tables helper.
    /// Tables separated by HTTP method.
    /// </summary>
    internal class UriDispatchTables
    {
        #region Fields

        private readonly Dictionary<string, CommandInfo> commandDictionary = new Dictionary<string, CommandInfo>();

        private UriTemplateTable deleteDispatcherTable;

        private UriTemplateTable getDispatcherTable;

        private UriTemplateTable postDispatcherTable;

        #endregion

        #region Constructors and Destructors

        public UriDispatchTables(Uri prefix)
        {
            this.InitializeSeleniumCommandDictionary();
            this.InitializeWiniumCommandDictionary();
            this.ConstructDispatcherTables(prefix);
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Try to get command by given URI and http method.
        /// </summary>
        /// <param name="httpMethod">Http protocol method.</param>
        /// <param name="uriToMatch">Uri to match with.</param>
        /// <returns>Matched pattern or null.</returns>
        public UriTemplateMatch Match(string httpMethod, Uri uriToMatch)
        {
            var table = this.FindDispatcherTable(httpMethod);
            return table != null ? table.MatchSingle(uriToMatch) : null;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Select table to search in by http protocol method.
        /// </summary>
        /// <param name="httpMethod">Http protocol method.</param>
        /// <returns>Table to search in or null.</returns>
        private UriTemplateTable FindDispatcherTable(string httpMethod) =>
            httpMethod switch
            {
                CommandInfo.GetCommand => this.getDispatcherTable,
                CommandInfo.PostCommand => this.postDispatcherTable,
                CommandInfo.DeleteCommand => this.deleteDispatcherTable,
                _ => null,
            };

        private void ConstructDispatcherTables(Uri prefix)
        {
            this.getDispatcherTable = new UriTemplateTable(prefix);
            this.postDispatcherTable = new UriTemplateTable(prefix);
            this.deleteDispatcherTable = new UriTemplateTable(prefix);

            var fields = typeof(DriverCommand).GetFields(BindingFlags.Public | BindingFlags.Static);
            foreach (var field in fields)
            {
                var commandName = field.GetValue(null).ToString();
                var commandInformation = this.commandDictionary[commandName];
                var commandUriTemplate = new UriTemplate(commandInformation.ResourcePath);
                var templateTable = this.FindDispatcherTable(commandInformation.Method);
                templateTable.KeyValuePairs.Add(new KeyValuePair<UriTemplate, object>(commandUriTemplate, commandName));
            }

            this.getDispatcherTable.MakeReadOnly(false);
            this.postDispatcherTable.MakeReadOnly(false);
            this.deleteDispatcherTable.MakeReadOnly(false);
        }

        /// <summary>
        /// See: https://w3c.github.io/webdriver/#endpoints
        /// </summary>
        private void InitializeSeleniumCommandDictionary()
        {
            this.commandDictionary.Add(DriverCommand.NewSession, new CommandInfo("POST", "/session"));
            // Wrong command name. Must be "Delete Session"
            this.commandDictionary.Add(DriverCommand.Quit, new CommandInfo("DELETE", "/session/{sessionId}"));
            this.commandDictionary.Add(DriverCommand.Status, new CommandInfo("GET", "/status"));
            // Absent GET	/session/{session id}/timeouts	Get Timeouts
            this.commandDictionary.Add(
                DriverCommand.SetTimeout,
                new CommandInfo("POST", "/session/{sessionId}/timeouts"));
            // Not described command
            this.commandDictionary.Add(
                DriverCommand.ImplicitlyWait,
                new CommandInfo("POST", "/session/{sessionId}/timeouts/implicit_wait"));
            // Not described command
            this.commandDictionary.Add(
                DriverCommand.SetAsyncScriptTimeout,
                new CommandInfo("POST", "/session/{sessionId}/timeouts/async_script"));
            // Wrong name. Must be "Navigate To"
            this.commandDictionary.Add(DriverCommand.Get, new CommandInfo("POST", "/session/{sessionId}/url"));
            this.commandDictionary.Add(DriverCommand.GetCurrentUrl, new CommandInfo("GET", "/session/{sessionId}/url"));

            // Wrong name. Must be "Back"
            this.commandDictionary.Add(DriverCommand.GoBack, new CommandInfo("POST", "/session/{sessionId}/back"));
            // Wrong name. Must be "Forward"
            this.commandDictionary.Add(DriverCommand.GoForward, new CommandInfo("POST", "/session/{sessionId}/forward"));

            this.commandDictionary.Add(DriverCommand.Refresh, new CommandInfo("POST", "/session/{sessionId}/refresh"));

            this.commandDictionary.Add(DriverCommand.GetTitle, new CommandInfo("GET", "/session/{sessionId}/title"));

            // Absent GET	/session/{session id}/window	Get Window Handle

            // Wrong name. Must be "Close Window"
            this.commandDictionary.Add(DriverCommand.Close, new CommandInfo("DELETE", "/session/{sessionId}/window"));

            this.commandDictionary.Add(
                DriverCommand.SwitchToWindow,
                new CommandInfo("POST", "/session/{sessionId}/window"));

            // Not described command
            this.commandDictionary.Add(
                DriverCommand.GetCurrentWindowHandle,
                new CommandInfo("GET", "/session/{sessionId}/window_handle"));

            // Wrong address. Must be "/session/{sessionId}/window/handles"
            this.commandDictionary.Add(
                DriverCommand.GetWindowHandles,
                new CommandInfo("GET", "/session/{sessionId}/window_handles"));

            // Absent POST	/session/{session id}/window/new	New Window

            this.commandDictionary.Add(
                DriverCommand.SwitchToFrame,
                new CommandInfo("POST", "/session/{sessionId}/frame"));
            this.commandDictionary.Add(
                DriverCommand.SwitchToParentFrame,
                new CommandInfo("POST", "/session/{sessionId}/frame/parent"));

            // Absent GET	/session/{session id}/window/rect	Get Window Rect
            // Absent POST / session /{ session id}/ window / rect   Set Window Rect

            // Not described command
            this.commandDictionary.Add(
                DriverCommand.GetWindowSize,
                new CommandInfo("GET", "/session/{sessionId}/window/{windowHandle}/size"));
            // Not described command
            this.commandDictionary.Add(
                DriverCommand.SetWindowSize,
                new CommandInfo("POST", "/session/{sessionId}/window/{windowHandle}/size"));
            // Not described command
            this.commandDictionary.Add(
                DriverCommand.GetWindowPosition,
                new CommandInfo("GET", "/session/{sessionId}/window/{windowHandle}/position"));
            // Not described command
            this.commandDictionary.Add(
                DriverCommand.SetWindowPosition,
                new CommandInfo("POST", "/session/{sessionId}/window/{windowHandle}/position"));
            // Not described command
            this.commandDictionary.Add(
                DriverCommand.MaximizeWindow,
                new CommandInfo("POST", "/session/{sessionId}/window/{windowHandle}/maximize"));

            // Absent GET  /session/{sessionId}/window/rect       Get Window Rect
            // Absent POST /session/{sessionId}/window/rect       Set Window Rect
            // Absent POST /session/{sessionId}/window/maximize   Maximize Window
            // Absent POST /session/{sessionId}/window/minimize   Minimize Window
            // Absent POST /session/{sessionId}/window/fullscreen Fullscreen Window

            this.commandDictionary.Add(
                DriverCommand.GetActiveElement,
                new CommandInfo("POST", "/session/{sessionId}/element/active"));

            // Absent GET	/session/{session id}/element/{element id}/shadow	Get Element Shadow Root

            this.commandDictionary.Add(
                DriverCommand.FindElement,
                new CommandInfo("POST", "/session/{sessionId}/element"));

            this.commandDictionary.Add(
                DriverCommand.FindElements,
                new CommandInfo("POST", "/session/{sessionId}/elements"));

            // Wrong name. Must be "Find Element From Element"
            this.commandDictionary.Add(
                DriverCommand.FindChildElement,
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/element"));

            // Wrong name. Must be "Find Elements From Element"
            this.commandDictionary.Add(
                DriverCommand.FindChildElements,
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/elements"));

            // Absent POST	/session/{session id}/shadow/{shadow id}/element	Find Element From Shadow Root
            // Absent POST	/session/{session id}/shadow/{shadow id}/elements	Find Elements From Shadow Root

            this.commandDictionary.Add(
                DriverCommand.IsElementSelected,
                new CommandInfo("GET", "/session/{sessionId}/element/{id}/selected"));

            this.commandDictionary.Add(
                DriverCommand.GetElementAttribute,
                new CommandInfo("GET", "/session/{sessionId}/element/{id}/attribute/{name}"));

            // Absent GET	/session/{session id}/element/{element id}/property/{name}	Get Element Property

            // Wrong name. Must be "Get Element CSS Value"
            this.commandDictionary.Add(
               DriverCommand.GetElementValueOfCssProperty,
               new CommandInfo("GET", "/session/{sessionId}/element/{id}/css/{propertyName}"));

            this.commandDictionary.Add(
                DriverCommand.GetElementText,
                new CommandInfo("GET", "/session/{sessionId}/element/{id}/text"));

            this.commandDictionary.Add(
                DriverCommand.GetElementTagName,
                new CommandInfo("GET", "/session/{sessionId}/element/{id}/name"));

            // Absent GET	/session/{session id}/element/{element id}/rect	Get Element Rect

            this.commandDictionary.Add(
                DriverCommand.IsElementEnabled,
                new CommandInfo("GET", "/session/{sessionId}/element/{id}/enabled"));

            // Absent GET	/session/{session id}/element/{element id}/computedrole	    Get Computed Role
            // Absent GET	/session/{session id}/element/{element id}/computedlabel	Get Computed Label

            // Wrong name. Must be "Element Click"
            this.commandDictionary.Add(
                DriverCommand.ClickElement,
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/click"));

            // Wrong name. Must be "Element Clear"
            this.commandDictionary.Add(
                DriverCommand.ClearElement,
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/clear"));

            // Wrong name. Must be "Element Send Keys"
            this.commandDictionary.Add(
                DriverCommand.SendKeysToElement,
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/value"));

            this.commandDictionary.Add(
                DriverCommand.GetPageSource,
                new CommandInfo("GET", "/session/{sessionId}/source"));

            // Wrong adress. Must be "/session/{session id}/execute/sync"
            this.commandDictionary.Add(
                DriverCommand.ExecuteScript,
                new CommandInfo("POST", "/session/{sessionId}/execute"));

            // Wrong adress. Must be "/session/{session id}/execute/async"
            this.commandDictionary.Add(
                DriverCommand.ExecuteAsyncScript,
                new CommandInfo("POST", "/session/{sessionId}/execute_async"));

            this.commandDictionary.Add(
                DriverCommand.GetAllCookies,
                new CommandInfo("GET", "/session/{sessionId}/cookie"));

            // Absent GET	/session/{session id}/cookie/{name}	Get Named Cookie

            this.commandDictionary.Add(
                DriverCommand.AddCookie,
                new CommandInfo("POST", "/session/{sessionId}/cookie"));

            this.commandDictionary.Add(
                DriverCommand.DeleteCookie,
                new CommandInfo("DELETE", "/session/{sessionId}/cookie/{name}"));

            this.commandDictionary.Add(
                DriverCommand.DeleteAllCookies,
                new CommandInfo("DELETE", "/session/{sessionId}/cookie"));

            // Absent POST	    /session/{session id}/actions	Perform Actions
            // Absent DELETE	/session/{session id}/actions	Release Actions

            // Wrong address. Must be "/session/{session id}/alert/dismiss"
            this.commandDictionary.Add(
                DriverCommand.DismissAlert,
                new CommandInfo("POST", "/session/{sessionId}/dismiss_alert"));

            // Wrong address. Must be "/session/{session id}/alert/accept"
            this.commandDictionary.Add(
                DriverCommand.AcceptAlert,
                new CommandInfo("POST", "/session/{sessionId}/accept_alert"));

            // Wrong address. Must be "/session/{session id}/alert/text"
            this.commandDictionary.Add(
                DriverCommand.GetAlertText,
                new CommandInfo("GET", "/session/{sessionId}/alert_text"));

            // Wrong address. Must be "/session/{session id}/alert/text"
            this.commandDictionary.Add(
                DriverCommand.SetAlertValue,
                new CommandInfo("POST", "/session/{sessionId}/alert_text"));

            // Wrong name. Must be "Take Screenshot"
            this.commandDictionary.Add(
                DriverCommand.Screenshot,
                new CommandInfo("GET", "/session/{sessionId}/screenshot"));

            // Absent GET	/session/{session id}/element/{element id}/screenshot	Take Element Screenshot

            // Absent POST	/session/{session id}/print	Print Page

            // All other commands is not described.

            this.commandDictionary.Add(DriverCommand.DefineDriverMapping, new CommandInfo("POST", "/config/drivers"));
            
            this.commandDictionary.Add(DriverCommand.GetSessionList, new CommandInfo("GET", "/sessions"));
            
            this.commandDictionary.Add(
                DriverCommand.GetSessionCapabilities,
                new CommandInfo("GET", "/session/{sessionId}"));

            this.commandDictionary.Add(
                DriverCommand.DescribeElement,
                new CommandInfo("GET", "/session/{sessionId}/element/{id}"));
            
            
            this.commandDictionary.Add(
                DriverCommand.SubmitElement,
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/submit"));
            
            
            this.commandDictionary.Add(
                DriverCommand.IsElementDisplayed,
                new CommandInfo("GET", "/session/{sessionId}/element/{id}/displayed"));
            this.commandDictionary.Add(
                DriverCommand.GetElementLocation,
                new CommandInfo("GET", "/session/{sessionId}/element/{id}/location"));
            this.commandDictionary.Add(
                DriverCommand.GetElementLocationOnceScrolledIntoView,
                new CommandInfo("GET", "/session/{sessionId}/element/{id}/location_in_view"));
            this.commandDictionary.Add(
                DriverCommand.GetElementSize,
                new CommandInfo("GET", "/session/{sessionId}/element/{id}/size"));
           
            
            this.commandDictionary.Add(
                DriverCommand.ElementEquals,
                new CommandInfo("GET", "/session/{sessionId}/element/{id}/equals/{other}"));
            
            
            this.commandDictionary.Add(
                DriverCommand.GetOrientation,
                new CommandInfo("GET", "/session/{sessionId}/orientation"));
            this.commandDictionary.Add(
                DriverCommand.SetOrientation,
                new CommandInfo("POST", "/session/{sessionId}/orientation"));
            
            
            this.commandDictionary.Add(DriverCommand.MouseClick, new CommandInfo("POST", "/session/{sessionId}/click"));
            this.commandDictionary.Add(
                DriverCommand.MouseDoubleClick,
                new CommandInfo("POST", "/session/{sessionId}/doubleclick"));
            this.commandDictionary.Add(
                DriverCommand.MouseDown,
                new CommandInfo("POST", "/session/{sessionId}/buttondown"));
            this.commandDictionary.Add(DriverCommand.MouseUp, new CommandInfo("POST", "/session/{sessionId}/buttonup"));
            this.commandDictionary.Add(
                DriverCommand.MouseMoveTo,
                new CommandInfo("POST", "/session/{sessionId}/moveto"));
            this.commandDictionary.Add(
                DriverCommand.SendKeysToActiveElement,
                new CommandInfo("POST", "/session/{sessionId}/keys"));
            this.commandDictionary.Add(
                DriverCommand.TouchSingleTap,
                new CommandInfo("POST", "/session/{sessionId}/touch/click"));
            this.commandDictionary.Add(
                DriverCommand.TouchPress,
                new CommandInfo("POST", "/session/{sessionId}/touch/down"));
            this.commandDictionary.Add(
                DriverCommand.TouchRelease,
                new CommandInfo("POST", "/session/{sessionId}/touch/up"));
            this.commandDictionary.Add(
                DriverCommand.TouchMove,
                new CommandInfo("POST", "/session/{sessionId}/touch/move"));
            this.commandDictionary.Add(
                DriverCommand.TouchScroll,
                new CommandInfo("POST", "/session/{sessionId}/touch/scroll"));
            this.commandDictionary.Add(
                DriverCommand.TouchDoubleTap,
                new CommandInfo("POST", "/session/{sessionId}/touch/doubleclick"));
            this.commandDictionary.Add(
                DriverCommand.TouchLongPress,
                new CommandInfo("POST", "/session/{sessionId}/touch/longclick"));
            this.commandDictionary.Add(
                DriverCommand.TouchFlick,
                new CommandInfo("POST", "/session/{sessionId}/touch/flick"));
            this.commandDictionary.Add(DriverCommand.UploadFile, new CommandInfo("POST", "/session/{sessionId}/file"));
        }

        private void InitializeWiniumCommandDictionary()
        {
            this.commandDictionary.Add(
                DriverCommand.FindDataGridCell,
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/datagrid/cell/{row}/{column}"));

            this.commandDictionary.Add(
                DriverCommand.GetDataGridColumnCount,
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/datagrid/column/count"));

            this.commandDictionary.Add(
                DriverCommand.GetDataGridRowCount,
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/datagrid/row/count"));

            this.commandDictionary.Add(
                DriverCommand.ScrollToDataGridCell,
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/datagrid/scroll/{row}/{column}"));

            this.commandDictionary.Add(
                DriverCommand.SelectDataGridCell,
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/datagrid/select/{row}/{column}"));

            this.commandDictionary.Add(
                DriverCommand.IsComboBoxExpanded,
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/combobox/expanded"));

            this.commandDictionary.Add(
                DriverCommand.ExpandComboBox,
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/combobox/expand"));

            this.commandDictionary.Add(
                DriverCommand.CollapseComboBox,
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/combobox/collapse"));

            this.commandDictionary.Add(
                DriverCommand.FindComboBoxSelectedItem,
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/combobox/items/selected"));

            this.commandDictionary.Add(
                DriverCommand.ScrollToComboBoxItem,
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/combobox/scroll"));

            this.commandDictionary.Add(
                DriverCommand.ScrollToListBoxItem,
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/listbox/scroll"));

            this.commandDictionary.Add(
                DriverCommand.FindMenuItem,
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/menu/item/{path}"));

            this.commandDictionary.Add(
                DriverCommand.SelectMenuItem,
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/menu/select/{path}"));
        }

        #endregion
    }
}
