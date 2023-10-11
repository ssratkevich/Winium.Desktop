using System;

namespace Winium.StoreApps.Common.Exceptions
{
    /// <summary>
    /// Automation exception.
    /// </summary>
    public class AutomationException : Exception
    {
        #region Fields

        private ResponseStatus responseStatus = ResponseStatus.UnknownError;

        private ErrorCodes errorCodes = ErrorCodes.UnknownError;

        #endregion

        #region Constructors

        /// <summary>
        /// Create new empty instance.
        /// </summary>
        public AutomationException()
        {
        }

        /// <summary>
        /// Create new instance with given message and status.
        /// </summary>
        /// <param name="message">Exception message.</param>
        /// <param name="status">Response status.</param>
        public AutomationException(string message, ResponseStatus status)
            : base(message)
        {
            this.Status = status;
        }

        /// <summary>
        /// Create new instance with given message format string and its args.
        /// </summary>
        /// <param name="message">Exception message format string.</param>
        /// <param name="args">Optional args to insert in format string.</param>
        public AutomationException(string message, params object[] args)
            : base(string.Format(message, args))
        {
        }

        /// <summary>
        /// Create new instance with given message and inner exception.
        /// </summary>
        /// <param name="message">Exception message.</param>
        /// <param name="innerException">Inner exception.</param>
        public AutomationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Responce status.
        /// </summary>
        public ResponseStatus Status
        {
            get => this.responseStatus;
            set => this.responseStatus = value;
        }

        public ErrorCodes ErrorCode
        {
            get => this.errorCodes;
            set => this.errorCodes = value;
        }


        public AutomationException(string message, ErrorCodes errorCodes)
            : base(message)
        {
            this.ErrorCode = errorCodes;
        }

        #endregion
    }
}
