namespace Plaid.Net.Data.Models
{
    using System;

    /// <summary>
    /// The add user options.
    /// </summary>
    public sealed class AddUserOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddUserOptions"/> class.
        /// </summary>
        public AddUserOptions()
        {
            this.LoginOnly = true;
        }

        /// <summary>
        /// Gets or sets the end date for which transactions will be collected.
        /// </summary>
        public DateTimeOffset? EndDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to include the code-based mfa list.
        /// </summary>
        /// <remarks>
        /// If the institution requires a code-based MFA credential, this option will list the available send methods.
        /// </remarks>
        public bool IncludeMfaList { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether include pending transactions.
        /// </summary>
        /// <remarks>
        /// If set to true, pending transactions will be returned. Pending transactions will generally show up as posted 
        /// within one to three business days, depending on the type of transaction - though some transactions may never post.
        /// Currently, new transaction IDs will be generated for all posted transactions.
        /// </remarks>
        public bool IncludePending { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the request is login only.
        /// </summary>
        /// <remarks>
        /// This option is valid for the initial authentication only. If set to false, 
        /// the initial /connect request will return transaction data based on the start_date and end_date specified.
        /// </remarks>
        public bool LoginOnly { get; set; }

        /// <summary>
        /// Gets or sets the start date from which to return transactions.
        /// </summary>
        public DateTimeOffset? StartDate { get; set; }

        /// <summary>
        /// Gets or sets the webhook. This webhook will be hit when the users' transactions have finished processing.
        /// </summary>
        public Uri WebhookUri { get; set; }
    }
}