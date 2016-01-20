namespace Plaid.Net.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Result from GetTransactionsAsync.
    /// </summary>
    public class TransactionResult : PlaidResult
    {
        /// <summary>
        /// Gets the accounts returned from the operation.
        /// </summary>
        public IList<Account> Accounts { get; internal set; }

        /// <summary>
        /// Gets the transactions returned from the operation.
        /// </summary>
        public IList<Transaction> Transactions { get; internal set; }
    }
}