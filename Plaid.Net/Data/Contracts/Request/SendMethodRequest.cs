namespace Plaid.Net.Contracts
{
    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// The send method request.
    /// </summary>
    internal class SendMethodRequest
    {
        /// <summary>
        /// Gets or sets the send method for code-based twofactor auth.
        /// </summary>
        [JsonProperty("send_method")]
        public dynamic SendMethod { get; set; }
    }
}