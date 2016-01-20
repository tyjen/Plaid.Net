namespace Plaid.Net.Data.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Results from <see cref="IPlaidClient.GetAccountBalanceAsync"/>.
    /// </summary>
    public class BalanceResult
    {
        /// <summary>
        /// Gets the accounts returned from the operation.
        /// </summary>
        public IList<Account> Accounts { get; internal set; }

        /// <summary>
        /// Gets exception information if a request was not successful.
        /// </summary>
        public PlaidException Exception { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether the operation resulted in an error.
        /// </summary>
        public bool IsError => this.Exception != null;
    }
}