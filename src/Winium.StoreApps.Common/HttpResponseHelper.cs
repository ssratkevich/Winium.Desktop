using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Winium.StoreApps.Common
{
    /// <summary>
    /// Helper for generating Http protocol responses.
    /// </summary>
    public static class HttpResponseHelper
    {
        #region Constants

        /// <summary>
        /// Content type of JSON response (part of http header).
        /// </summary>
        private const string JsonContentType = "application/json;charset=UTF-8";

        /// <summary>
        /// Content type of plain text response (part of http header).
        /// </summary>
        private const string PlainTextContentType = "text/plain";

        #endregion

        #region Static Fields

        /// <summary>
        /// Code to string map.
        /// </summary>
        private static Dictionary<int, string> statusCodeDescriptors;

        #endregion

        #region Public Properties

        /// <summary>
        /// Code to string map.
        /// See https://developer.mozilla.org/ru/docs/Web/HTTP/Status, https://datatracker.ietf.org/doc/html/rfc7231#section-6
        /// </summary>
        public static Dictionary<int, string> StatusCodeDescriptors =>
            statusCodeDescriptors ??=
            new Dictionary<int, string>
            {
                // 100
                { (int) HttpStatusCode.Continue, "Continue" },
                { (int) HttpStatusCode.SwitchingProtocols, "Switching Protocols" },
                { 102, "Processing" },
                { 103, "Early Hints" },

                // 200
                { (int) HttpStatusCode.OK, "OK" },
                { (int) HttpStatusCode.Created, "Created" },
                { (int) HttpStatusCode.Accepted, "Accepted" },
                { (int) HttpStatusCode.NonAuthoritativeInformation, "Non-Authoritative Information" },
                { (int) HttpStatusCode.NoContent, "No Content" },
                { (int) HttpStatusCode.ResetContent, "Reset Content" },
                { (int) HttpStatusCode.PartialContent, "Partial Content" },
                { 207, "Multi-Status" },

                // 300
                { (int) HttpStatusCode.Ambiguous, "Multiple Choices" },
                { (int) HttpStatusCode.Moved, "Moved Permanently" },
                { (int) HttpStatusCode.Found, "Found" },
                { (int) HttpStatusCode.SeeOther, "See Other" },
                { (int) HttpStatusCode.NotModified, "Not Modified" },
                { (int) HttpStatusCode.UseProxy, "Use Proxy" },
                { 306, "Switch Proxy" },
                { (int) HttpStatusCode.TemporaryRedirect, "Temporary Redirect" },
                { 308, "Permanent Redirect" },

                // 400
                { (int) HttpStatusCode.BadRequest, "Bad Request" },
                { (int) HttpStatusCode.Unauthorized, "Unauthorized" },
                { (int) HttpStatusCode.PaymentRequired, "Payment Required" },
                { (int) HttpStatusCode.Forbidden, "Forbidden" },
                { (int) HttpStatusCode.NotFound, "Not Found" },
                { (int) HttpStatusCode.MethodNotAllowed, "Method Not Allowed" },
                { (int) HttpStatusCode.NotAcceptable, "Not Acceptable" },
                { (int) HttpStatusCode.ProxyAuthenticationRequired, "Proxy Authentication Required" },
                { (int) HttpStatusCode.RequestTimeout, "Request Timeout" },
                { (int) HttpStatusCode.Conflict, "Conflict" },
                { (int) HttpStatusCode.Gone, "Gone" },
                { (int) HttpStatusCode.LengthRequired, "Length Required" },
                { (int) HttpStatusCode.PreconditionFailed, "Precondition Failed" },
                { (int) HttpStatusCode.RequestEntityTooLarge, "Request Entity Too Large" },
                { (int) HttpStatusCode.RequestUriTooLong, "Request-Uri Too Long" },
                { (int) HttpStatusCode.UnsupportedMediaType, "Unsupported Media Type" },
                { (int) HttpStatusCode.RequestedRangeNotSatisfiable, "Requested Range Not Satisfiable" },
                { (int) HttpStatusCode.ExpectationFailed, "Expectation Failed" },
                { 422, "Unprocessable Entity" },
                { 423, "Locked" },
                { 424, "Failed Dependency" },
                { (int) HttpStatusCode.UpgradeRequired, "Upgrade Required" },

                // 500
                { (int) HttpStatusCode.InternalServerError, "Internal Server Error" },
                { (int) HttpStatusCode.NotImplemented, "Not Implemented" },
                { (int) HttpStatusCode.BadGateway, "Bad Gateway" },
                { (int) HttpStatusCode.ServiceUnavailable, "Service Unavailable" },
                { (int) HttpStatusCode.GatewayTimeout, "Gateway Timeout" },
                { (int) HttpStatusCode.HttpVersionNotSupported, "Http Version Not Supported" },
                { 507, "Insufficient Storage" },
            };
            
        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Get description for code.
        /// </summary>
        /// <param name="code">Status code.</param>
        /// <returns>Description.</returns>
        public static string GetStatusCodeDescription(HttpStatusCode code) =>
            StatusCodeDescriptors.TryGetValue((int)code, out var description) ? description : string.Empty;

        /// <summary>
        /// Check code for user error.
        /// </summary>
        /// <param name="code">Response code to check.</param>
        /// <returns>true if it is user error.</returns>
        public static bool IsClientError(int code) =>
            code >= 400 && code < 500;

        /// <summary>
        /// Generates Http response string representation.
        /// </summary>
        /// <param name="statusCode">Http status code.</param>
        /// <param name="content">Response body.</param>
        /// <returns>Http response string representation.</returns>
        public static string ResponseString(HttpStatusCode statusCode, string content)
        {
            var contentType = IsClientError((int)statusCode) ? PlainTextContentType : JsonContentType;
            var statusDescription = GetStatusCodeDescription(statusCode);

            var responseString = new StringBuilder();
            responseString.AppendLine(string.Format("HTTP/1.1 {0} {1}", (int)statusCode, statusDescription));
            responseString.AppendLine(string.Format("Content-Type: {0}", contentType));
            responseString.AppendLine("Connection: close");
            responseString.AppendLine(string.Empty);
            responseString.AppendLine(content);

            return responseString.ToString();
        }

        #endregion
    }
}
