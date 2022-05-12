namespace Winium.StoreApps.Common
{
    /// <summary>
    /// Command info according to https://w3c.github.io/webdriver/#endpoints.
    /// </summary>
    public class CommandInfo
    {
        #region Constants

        /// <summary>
        /// DELETE HTTP protocol command.
        /// </summary>
        public const string DeleteCommand = "DELETE";

        /// <summary>
        /// GET HTTP protocol command.
        /// </summary>
        public const string GetCommand = "GET";

        /// <summary>
        /// POST HTTP protocol command.
        /// </summary>
        public const string PostCommand = "POST";

        #endregion

        #region Constructors

        /// <summary>
        /// Create command info with given method and uri template, that uniquely identify the command.
        /// </summary>
        /// <param name="method">Http protocol method.</param>
        /// <param name="resourcePath">Uri template, that uniquely identify the command.</param>
        public CommandInfo(string method, string resourcePath)
        {
            this.ResourcePath = resourcePath;
            this.Method = method;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Http protocol method.
        /// </summary>
        public string Method { get; private set; }

        /// <summary>
        /// Uri template, that uniquely identify the command.
        /// </summary>
        public string ResourcePath { get; private set; }

        #endregion
    }
}
