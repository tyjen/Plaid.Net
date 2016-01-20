namespace Plaid.Net.Models
{
    using System;

    /// <summary>
    /// Result from an exchange token api call.
    /// </summary>
    public class TokenExchangeResult : PlaidResult
    {
        /// <summary>
        /// The Plaid access token for the user.
        /// </summary>
        public AccessToken AccessToken { get; set; }

        /// <summary>
        /// The bank account token used to make ACH payments.
        /// </summary>
        public string BankAccountToken { get; set; }
    }
}