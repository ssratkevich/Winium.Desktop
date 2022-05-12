using System.Net;

namespace Winium.StoreApps.Common
{
    /// <summary>
    /// Responce for command.
    /// </summary>
    public class CommandResponse
    {
        #region Public Properties

        /// <summary>
        /// Response body.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Http response status.
        /// </summary>
        public HttpStatusCode HttpStatusCode { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Create command response.
        /// </summary>
        /// <param name="code">Response code.</param>
        /// <param name="content">Response body.</param>
        /// <returns>Command response.</returns>
        public static CommandResponse Create(HttpStatusCode code, string content) =>
            new CommandResponse { HttpStatusCode = code, Content = content };

        /// <inheritdoc/>
        public override string ToString() =>
            $"{this.HttpStatusCode}: {this.Content}";
        
        #endregion
    }
}
