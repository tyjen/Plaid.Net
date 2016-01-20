namespace Plaid.Net.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a financial transaction.
    /// </summary>
    public class Transaction
    {
        /// <summary>
        /// Gets or sets the id of the account in which this transaction occurred.
        /// </summary>
        public string AccountId { get; set; }

        /// <summary>
        /// Gets or sets the transaction amount.
        /// </summary>
        /// <remarks>
        /// The settled dollar value. Positive values when money moves out of the account; negative values when money moves in.
        /// </remarks>
        public double Amount { get; set; }

        /// <summary>
        /// An hierarchical array of the categories to which this transaction belongs.
        /// </summary>
        public IList<string> Categories { get; set; }

        /// <summary>
        /// Gets or sets the id of the category to which this transaction belongs.
        /// </summary>
        public string CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the date that the transaction was posted by the financial institution (in ISO 8601 format).
        /// </summary>
        public DateTimeOffset Date { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this transaction is pending.
        /// </summary>
        /// <remarks>
        /// When true, identifies the transaction as pending or unsettled. Pending transaction details (name, type, amount) may change before they are settled.
        /// </remarks>
        public bool IsPending { get; set; }

        /// <summary>
        /// Gets or sets the location associated with the transaction if available.
        /// </summary>
        public Address Location { get; set; }

        /// <summary>
        /// Gets or sets the metadata. Usually location or contact information.
        /// TODO: Explicitly parse out contact info.
        /// </summary>
        public dynamic Metadata { get; set; }

        /// <summary>
        /// Gets or sets the transaction name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A numeric representation of our confidence in the meta data Plaid attached to the transaction.
        /// </summary>
        public dynamic Score { get; set; }

        /// <summary>
        /// Gets or sets the transaction id.
        /// </summary>
        public string TransactionId { get; set; }

        /// <summary>
        /// Gets or sets the transaction type. Can be Place, Digital, or Special.
        /// TODO: Parse this explicitly.
        /// </summary>
        public dynamic Type { get; set; }
    }
}