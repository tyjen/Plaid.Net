namespace Plaid.Net.Data.Contracts
{
    using System;
    using System.Security;
    using Newtonsoft.Json;
    using Tyjen.Net.Core.Extensions;

    /// <summary>
    /// Request object to add a new user.
    /// </summary>
    /// <remarks>
    /// To add a new end-user, submit their institution type and credentials to the /connect endpoint. 
    /// In certain cases, depending on the Financial Institution, you might be required to submit additional 
    /// information in the credentials object such as a PIN number. 
    /// </remarks>
    internal class AddUserRequest : PlaidRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddUserRequest"/> class.
        /// </summary>
        /// <param name="clientId">The client id provided on signup.</param>
        /// <param name="secret">The client secret provided on signup.</param>
        public AddUserRequest(string clientId, string secret)
            : base(clientId, secret)
        {
            // No-op
        }

        /// <summary>
        /// Gets or sets options for the request.
        /// </summary>
        [JsonProperty("options", NullValueHandling = NullValueHandling.Ignore)]
        public AddUserOptionsRequest Options { get; set; }

        /// <summary>
        /// Gets or sets the user's banking institution password.
        /// </summary>
        [JsonProperty("password")]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the user's banking institution pin.
        /// </summary>
        [JsonProperty("pin", NullValueHandling = NullValueHandling.Ignore)]
        public string Pin { get; set; }

        /// <summary>
        /// Gets or sets the institution type the request is intended for.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the user's banking institution username.
        /// </summary>
        [JsonProperty("username")]
        public string Username { get; set; }
    }
}