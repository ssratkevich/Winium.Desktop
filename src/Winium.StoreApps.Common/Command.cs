using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Winium.StoreApps.Common
{
    /// <summary>
    /// WebDriver command.
    /// </summary>
    public class Command
    {
        #region Fields

        /// <summary>
        /// Command parameters according to https://w3c.github.io/webdriver/#endpoints.
        /// </summary>
        private IDictionary<string, JToken> commandParameters;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Creates command with given name and parameters.
        /// </summary>
        /// <param name="name">Command name.</param>
        /// <param name="jsonParameters">JSON object - command parameters.</param>
        public Command(string name, string jsonParameters)
            : this(name, string.IsNullOrEmpty(jsonParameters) ? null : JObject.Parse(jsonParameters))
        {
        }

        /// <summary>
        /// Creates command with given name and JSON object parameters string representation.
        /// </summary>
        /// <param name="name">Command name.</param>
        /// <param name="parameters">JSON object parameters string representation</param>
        public Command(string name, IDictionary<string, JToken> parameters)
        {
            this.Name = name;
            this.commandParameters = parameters ?? new JObject();
        }

        /// <summary>
        /// Creates command with given name and empty parameters.
        /// </summary>
        /// <param name="name">Command name.</param>
        public Command(string name) : this(name, string.Empty)
        { }

        /// <summary>
        /// Creates empty command.
        /// </summary>
        public Command() : this(string.Empty)
        { }

        #endregion

        #region Public Properties

        /// <summary>
        /// Command name.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Command parameters.
        /// </summary>
        [JsonProperty("parameters")]
        public IDictionary<string, JToken> Parameters
        {
            get => this.commandParameters;
            set => this.commandParameters = value;
        }

        /// <summary>
        /// Command SessionID.
        /// </summary>
        [JsonProperty("sessionId")]
        public string SessionId { get; set; }

        #endregion
    }
}
