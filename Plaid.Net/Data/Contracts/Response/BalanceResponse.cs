namespace Plaid.Net.Contracts
{
    using System;

    /// <summary>
    /// The balance of an <see cref="AccountResponse"/>.
    /// </summary>
    /// <remarks>
    /// The Current Balance is the total amount of funds in the account. The Available Balance is the Current Balance 
    /// less any outstanding holds or debits that have not yet posted to the account. Note that not all institutions 
    /// calculate the Available Balance. In the case that Available Balance is unavailable from the institution, 
    /// Plaid will either return an Available Balance value of null or only return a Current Balance.
    /// </remarks>
    internal class BalanceResponse
    {
        /// <summary>
        /// Gets or sets the available balance.
        /// </summary>
        public double Available { get; set; }

        /// <summary>
        /// Gets or sets the current balance.
        /// </summary>
        public double Current { get; set; }
    }
}