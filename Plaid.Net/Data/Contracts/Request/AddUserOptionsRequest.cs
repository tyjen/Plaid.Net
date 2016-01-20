namespace Plaid.Net.Contracts
{
    using System;
    using Newtonsoft.Json;
    using Plaid.Net.Models;

    /// <summary>
    /// Optional parameters for an <see cref="AddUserRequest"/>.
    /// </summary>
    internal sealed class AddUserOptionsRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddUserOptionsRequest"/> class.
        /// </summary>
        public AddUserOptionsRequest()
        {
            this.LoginOnly = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AddUserOptionsRequest"/> class.
        /// </summary>
        /// <param name="options">The options model to make a contract out of.</param>
        public AddUserOptionsRequest(AddUserOptions options)
            : this()
        {
            if (options == null) return;

            this.EndDate = options.EndDate;
            this.IncludeMfaList = options.IncludeMfaList;
            this.IncludePending = options.IncludePending;
            this.LoginOnly = options.LoginOnly;
            this.StartDate = options.StartDate;
            this.Webhook = options.WebhookUri?.ToString();
        }

        /// <summary>
        /// Gets or sets the end date for which transactions will be collected.
        /// </summary>
        [JsonProperty("end_date", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? EndDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to include the code-based mfa list.
        /// </summary>
        /// <remarks>
        /// If the institution requires a code-based MFA credential, this option will list the available send methods.
        /// </remarks>
        [JsonProperty("list", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IncludeMfaList { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether include pending transactions.
        /// </summary>
        /// <remarks>
        /// If set to true, pending transactions will be returned. Pending transactions will generally show up as posted 
        /// within one to three business days, depending on the type of transaction - though some transactions may never post.
        /// Currently, new transaction IDs will be generated for all posted transactions.
        /// </remarks>
        [JsonProperty("pending", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IncludePending { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the request is login only.
        /// </summary>
        /// <remarks>
        /// This option is valid for the initial authentication only. If set to false, 
        /// the initial /connect request will return transaction data based on the start_date and end_date specified.
        /// </remarks>
        [JsonProperty("login_only", NullValueHandling = NullValueHandling.Ignore)]
        public bool? LoginOnly { get; set; }

        /// <summary>
        /// Gets or sets the start date from which to return transactions.
        /// </summary>
        [JsonProperty("start_date", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? StartDate { get; set; }

        /// <summary>
        /// Gets or sets the webhook. This webhook will be hit when the users' transactions have finished processing.
        /// </summary>
        [JsonProperty("webhook", NullValueHandling = NullValueHandling.Ignore)]
        public string Webhook { get; set; }
    }
}