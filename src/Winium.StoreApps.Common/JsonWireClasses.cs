using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Winium.StoreApps.Common
{
    /// <summary>
    /// JSON representation of found element.
    /// </summary>
    public class JsonElementContent
    {
        private string element;

        /// <summary>
        /// Creates JSON element content with given element UID.
        /// </summary>
        /// <param name="element">Element UID.</param>
        public JsonElementContent(string element)
        {
            this.element = element;
        }

        /// <summary>
        /// Element UID.
        /// </summary>
        [JsonProperty("ELEMENT")]
        public string Element { get => this.element; set => this.element = value; }

        /// <summary>
        /// Element UID according to https://w3c.github.io/webdriver/#dfn-web-element-identifier.
        /// </summary>
        [JsonProperty("element-6066-11e4-a52e-4f735466cecf")]
        public string NewElement { get => this.element; set => this.element = value; }
    }

    /// <summary>
    /// JSON response class.
    /// </summary>
    public class JsonResponse
    {
        /// <summary>
        /// Creates instance of JSON content in Http responce.
        /// </summary>
        /// <param name="sessionId">Session Id.</param>
        /// <param name="responseCode">Response code.</param>
        /// <param name="value">Body value either <see cref="JsonElementContent"/> or <see cref="Exception"/>.</param>
        public JsonResponse(string sessionId, ResponseStatus responseCode, object value)
        {
            this.SessionId = sessionId;
            this.Status = responseCode;

            this.Value = responseCode == ResponseStatus.Success ? value : this.PrepareErrorResponse(value);
        }

        /// <summary>
        /// Prepare error response with "value" : { "error": ..., "message":..., "stacktrace":... }
        /// See https://w3c.github.io/webdriver/#errors.
        /// </summary>
        /// <param name="value">Exception or other object describing the error.</param>
        /// <returns>Error description: { "error": ..., "message":..., "stacktrace":... }</returns>
        private object PrepareErrorResponse(object value)
        {
            var result = new Dictionary<string, string> { { "error", JsonErrorCodes.Parse(this.Status) } };
            
            string message;
            if (value is Exception exception)
            {
                message = exception.Message;
                result.Add("stacktrace", exception.StackTrace);
            }
            else
            {
                message = value.ToString();
            }

            result.Add("message", message);
            return result;
        }

        /// <summary>
        /// Session id.
        /// </summary>
        [JsonProperty("sessionId")]
        public string SessionId { get; set; }

        /// <summary>
        /// Status code.
        /// </summary>
        [JsonProperty("status")]
        public ResponseStatus Status { get; set; }

        /// <summary>
        /// Value.
        /// </summary>
        [JsonProperty("value")]
        public object Value { get; set; }
    }
}
