namespace Winium.StoreApps.Common
{
    /// <summary>
    /// Error codes according to https://w3c.github.io/webdriver/#errors 
    /// </summary>
    public enum ErrorCodes
    {
        /// <summary>
        /// No error.
        /// </summary>
        Success = 0,

        /// <summary>
        /// The Element Click command could not be completed because
        /// the element receiving the events is obscuring the element
        /// that was requested clicked.
        /// </summary>
        ElementClickIntercepted,

        /// <summary>
        /// A command could not be completed because the element
        /// is not pointer- or keyboard interactable.
        /// </summary>
        ElementNotInteractable,

        /// <summary>
        /// Navigation caused the user agent to hit a certificate warning,
        /// which is usually the result of an expired or invalid TLS certificate.
        /// (Not used in winium)
        /// </summary>
        InsecureCertificate,

        /// <summary>
        /// The arguments passed to a command are either invalid or malformed.
        /// </summary>
        InvalidArgument,

        /// <summary>
        /// An illegal attempt was made to set a cookie
        /// under a different domain than the current page.
        /// (Not used in winium)
        /// </summary>
        InvalidCookieDomain,

        /// <summary>
        /// A command could not be completed because the element
        /// is in an invalid state, e.g. attempting to clear
        /// an element that isn’t both editable and resettable.
        /// </summary>
        InvalidElementState,

        /// <summary>
        /// Argument was an invalid selector.
        /// </summary>
        InvalidSelector,

        /// <summary>
        /// Occurs if the given session id is not in
        /// the list of active sessions, meaning
        /// the session either does not exist or that it’s not active.
        /// </summary>
        InvalidSessionId,

        /// <summary>
        /// An error occurred while executing JavaScript supplied by the user.
        /// </summary>
        JavascriptError,

        /// <summary>
        /// The target for mouse interaction is not in the browser’s
        /// viewport and cannot be brought into that viewport.
        /// </summary>
        MoveTargetOutOfBounds,

        /// <summary>
        /// An attempt was made to operate on a modal dialog when one was not open.
        /// </summary>
        NoSuchAlert,

        /// <summary>
        /// No cookie matching the given path name was found
        /// amongst the associated cookies of the current
        /// browsing context’s active document.
        /// </summary>
        NoSuchCookie,

        /// <summary>
        /// An element could not be located on
        /// the page using the given search parameters.
        /// </summary>
        NoSuchElement,

        /// <summary>
        /// A command to switch to a frame could
        /// not be satisfied because the frame could not be found.
        /// </summary>
        NoSuchFrame,

        /// <summary>
        /// A command to switch to a window could
        /// not be satisfied because the window could not be found.
        /// </summary>
        NoSuchWindow,

        /// <summary>
        /// The element does not have a shadow root.
        /// </summary>
        NoSuchShadowRoot,

        /// <summary>
        /// A script did not complete before its timeout expired.
        /// </summary>
        ScriptTimeoutError,

        /// <summary>
        /// A new session could not be created.
        /// </summary>
        SessionNotCreated,

        /// <summary>
        /// A command failed because the referenced
        /// element is no longer attached to the DOM.
        /// </summary>
        StaleElementReference,

        /// <summary>
        /// A command failed because the referenced
        /// shadow root is no longer attached to the DOM.
        /// </summary>
        DetachedShadowRoot,

        /// <summary>
        /// An operation did not complete before its timeout expired.
        /// </summary>
        Timeout,

        /// <summary>
        /// A command to set a cookie’s value could not be satisfied.
        /// </summary>
        UnableToSetCookie,

        /// <summary>
        /// A screen capture was made impossible.
        /// </summary>
        UnableToCaptureScreen,

        /// <summary>
        /// A modal dialog was open, blocking this operation.
        /// </summary>
        UnexpectedAlertOpen,

        /// <summary>
        /// A command could not be executed because
        /// the remote end is not aware of it.
        /// </summary>
        UnknownCommand,

        /// <summary>
        /// An unknown error occurred in the
        /// remote end while processing the command.
        /// </summary>
        UnknownError,

        /// <summary>
        /// The requested command matched a known URL
        /// but did not match any method for that URL.
        /// </summary>
        UnknownMethod,

        /// <summary>
        /// Indicates that a command that should have
        /// executed properly cannot be supported for some reason.
        /// </summary>
        UnsupportedOperation,
    }
}
