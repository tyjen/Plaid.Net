namespace Plaid.Net.Data.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Result from GetTransactionsAsync.
    /// </summary>
    public class TransactionResult
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

        /// <summary>
        /// Gets the transactions returned from the operation.
        /// </summary>
        public IList<Transaction> Transactions { get; internal set; }
    }
}