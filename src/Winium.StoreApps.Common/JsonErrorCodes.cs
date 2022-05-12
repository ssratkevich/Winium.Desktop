using System.Collections.Generic;
using System.Net;

namespace Winium.StoreApps.Common
{
    /// <summary>
    /// Winium error codes.
    /// </summary>
    public static class JsonErrorCodes
    {
        // TODO: in the future ResponseStatus will be removed in favor of HTTPStatus (see https://w3c.github.io/webdriver/webdriver-spec.html#handling-errors)
        #region Static Fields

        private static readonly Dictionary<ResponseStatus, string> ErrorMap = new Dictionary<ResponseStatus, string>();

        private static readonly Dictionary<ErrorCodes, (string description, HttpStatusCode code)> ErrorCodesMap = new()
        {
            { ErrorCodes.Success, (string.Empty, HttpStatusCode.OK) },
            { ErrorCodes.ElementClickIntercepted, ("element click intercepted", HttpStatusCode.BadRequest) },
            { ErrorCodes.ElementNotInteractable, ("element not interactable", HttpStatusCode.BadRequest) },
            { ErrorCodes.InsecureCertificate, ("insecure certificate", HttpStatusCode.BadRequest) },
            { ErrorCodes.InvalidArgument, ("invalid argument", HttpStatusCode.BadRequest) },
            { ErrorCodes.InvalidCookieDomain, ("invalid cookie domain", HttpStatusCode.BadRequest) },
            { ErrorCodes.InvalidElementState, ("invalid element state", HttpStatusCode.BadRequest) },
            { ErrorCodes.InvalidSelector , ("invalid selector", HttpStatusCode.BadRequest) },
            { ErrorCodes.InvalidSessionId, ("invalid session id", HttpStatusCode.NotFound) },
            { ErrorCodes.JavascriptError , ("javascript error", HttpStatusCode.InternalServerError) },
            { ErrorCodes.MoveTargetOutOfBounds, ("move target out of bounds", HttpStatusCode.InternalServerError) },
            { ErrorCodes.NoSuchAlert, ("no such alert", HttpStatusCode.NotFound) },
            { ErrorCodes.NoSuchCookie, ("no such cookie", HttpStatusCode.NotFound) },
            { ErrorCodes.NoSuchElement, ("no such element", HttpStatusCode.NotFound) },
            { ErrorCodes.NoSuchFrame, ("no such frame", HttpStatusCode.NotFound) },
            { ErrorCodes.NoSuchWindow, ("no such window", HttpStatusCode.NotFound) },
            { ErrorCodes.NoSuchShadowRoot, ("no such shadow root", HttpStatusCode.NotFound) },
            { ErrorCodes.ScriptTimeoutError, ("script timeout", HttpStatusCode.InternalServerError) },
            { ErrorCodes.SessionNotCreated, ("session not created", HttpStatusCode.InternalServerError) },
            { ErrorCodes.StaleElementReference, ("stale element reference", HttpStatusCode.NotFound) },
            { ErrorCodes.DetachedShadowRoot, ("detached shadow root", HttpStatusCode.NotFound) },
            { ErrorCodes.Timeout, ("timeout", HttpStatusCode.InternalServerError) },
            { ErrorCodes.UnableToSetCookie, ("unable to set cookie", HttpStatusCode.InternalServerError) },
            { ErrorCodes.UnableToCaptureScreen, ("unable to capture screen", HttpStatusCode.InternalServerError) },
            { ErrorCodes.UnexpectedAlertOpen, ("unexpected alert open", HttpStatusCode.InternalServerError) },
            { ErrorCodes.UnknownCommand, ("unknown command", HttpStatusCode.NotFound) },
            { ErrorCodes.UnknownError, ("unknown error", HttpStatusCode.InternalServerError) },
            { ErrorCodes.UnknownMethod, ("unknown method", HttpStatusCode.MethodNotAllowed) },
            { ErrorCodes.UnsupportedOperation, ("unsupported operation", HttpStatusCode.InternalServerError) },
        };

        #endregion

        #region Constructors and Destructors

        static JsonErrorCodes()
        {
            ErrorMap.Add(ResponseStatus.NoSuchElement, "no such element");
            ErrorMap.Add(ResponseStatus.NoSuchFrame, "no such frame");
            ErrorMap.Add(ResponseStatus.UnknownCommand, "unknown command");
            ErrorMap.Add(ResponseStatus.StaleElementReference, "stale element reference");
            ErrorMap.Add(ResponseStatus.ElementNotVisible, "element not visible");
            ErrorMap.Add(ResponseStatus.InvalidElementState, "invalid element state");
            ErrorMap.Add(ResponseStatus.UnknownError, "unknown error");
            ErrorMap.Add(ResponseStatus.ElementIsNotSelectable, "element not selectable");
            ErrorMap.Add(ResponseStatus.JavaScriptError, "javascript error");
            ErrorMap.Add(ResponseStatus.Timeout, "timeout");
            ErrorMap.Add(ResponseStatus.NoSuchWindow, "no such window");
            ErrorMap.Add(ResponseStatus.InvalidCookieDomain, "invalid cookie domain");
            ErrorMap.Add(ResponseStatus.UnableToSetCookie, "unable to set cookie");
            ErrorMap.Add(ResponseStatus.UnexpectedAlertOpen, "unexpected alert open");
            ErrorMap.Add(ResponseStatus.NoAlertOpenError, "no such alert");
            ErrorMap.Add(ResponseStatus.ScriptTimeout, "script timeout");
            ErrorMap.Add(ResponseStatus.InvalidElementCoordinates, "invalid element coordinates");
            ErrorMap.Add(ResponseStatus.InvalidSelector, "invalid selector");
            ErrorMap.Add(ResponseStatus.SessionNotCreatedException, "session not created");
            ErrorMap.Add(ResponseStatus.MoveTargetOutOfBounds, "move target out of bounds");

            // TODO: No match in ResponseStatus
            /*ErrorMap.Add(400, "invalid argument");
            ErrorMap.Add(404, "invalid session id"); 
            ErrorMap.Add(405, "unknown method"); 
            ErrorMap.Add(500, "unsupported operation");*/
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Gets string description from response status.
        /// </summary>
        /// <param name="status">Response status code.</param>
        /// <returns>Description.</returns>
        public static string Parse(ResponseStatus status) =>
            ErrorMap.ContainsKey(status) ? ErrorMap[status] : status.ToString();

        /// <summary>
        /// Gets string description from error code.
        /// </summary>
        /// <param name="code">Error code.</param>
        /// <returns>Description.</returns>
        public static string GetErrorDescription(ErrorCodes code) =>
            ErrorCodesMap.TryGetValue(code, out var description)
            ? description.description : code.ToString();

        /// <summary>
        /// Gets <see cref="HttpStatusCode"/> from error code.
        /// </summary>
        /// <param name="code">Error code.</param>
        /// <returns>HttpStatusCode for error.</returns>
        public static HttpStatusCode GetErrorStatusCode(ErrorCodes code) =>
            ErrorCodesMap.TryGetValue(code, out var description)
            ? description.code : HttpStatusCode.InternalServerError;

        #endregion
    }
}
