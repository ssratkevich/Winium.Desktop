using System;
using System.Net;

namespace Winium.StoreApps.Common.Exceptions
{
    /// <summary>
    /// Inner driver exception.
    /// </summary>
    public class InnerDriverRequestException : Exception
    {
        #region Constructors
        
        /// <summary>
        /// Creates new exception.
        /// </summary>
        public InnerDriverRequestException()
        {
        }

        /// <summary>
        /// Creates new exception with given message and status code.
        /// </summary>
        /// <param name="message">Exception message.</param>
        /// <param name="statusCode">Status code.</param>
        public InnerDriverRequestException(string message, HttpStatusCode statusCode)
            : base(message)
        {
            this.StatusCode = statusCode;
        }

        /// <summary>
        /// Create new instance with given message format string and its args.
        /// </summary>
        /// <param name="message">Exception message format string.</param>
        /// <param name="args">Optional args to insert in format string.</param>
        public InnerDriverRequestException(string message, params object[] args)
            : base(string.Format(message, args))
        {
        }

        /// <summary>
        /// Create new instance with given message and inner exception.
        /// </summary>
        /// <param name="message">Exception message.</param>
        /// <param name="innerException">Inner exception.</param>
        public InnerDriverRequestException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Status code.
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }

        #endregion
    }
}
