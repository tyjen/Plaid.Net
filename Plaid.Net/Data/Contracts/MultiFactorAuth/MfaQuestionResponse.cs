namespace Plaid.Net.Data.Contracts
{
    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// Type of mfa response which specifies a security question.
    /// </summary>
    /// <remarks>
    /// Question-based authentication requires the user to answer a security question.
    /// With some financial institutions, multiple security questions may be required.
    /// </remarks>
    internal class MfaQuestionResponse
    {
        /// <summary>
        /// Gets or sets the security question.
        /// </summary>
        [JsonProperty("question")]
        public string Question { get; set; }
    }
}