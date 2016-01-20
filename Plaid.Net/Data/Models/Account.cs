namespace Plaid.Net.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a financial account.
    /// </summary>
    public class Account
    {
        /// <summary>
        /// Additional information pertaining to the account such as the limit, name, or last few digits of the account number.
        /// </summary>
        public IDictionary<string, string> Metadata;

        /// <summary>
        /// A more detailed classification of the account type. When unavailable, this field will be null.
        /// </summary>
        public AccountSubType AccountSubtype { get; set; }

        /// <summary>
        /// Gets or sets the account type.
        /// </summary>
        public AccountType AccountType { get; set; }

        /// <summary>
        /// Gets or sets the available account balance.
        /// </summary>
        public double AvailableBalance { get; set; }

        /// <summary>
        /// Gets or sets the current account balance.
        /// </summary>
        public double CurrentBalance { get; set; }

        /// <summary>
        /// Gets or sets the unique id of the account.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets which financial institution is associated with the account.
        /// </summary>
        public InstitutionType InstitutionType { get; set; }

        /// <summary>
        /// Gets or sets the id unique to the accounts of a particular access token.
        /// </summary>
        public string ItemId { get; set; }

        /// <summary>
        /// Gets or sets the user id the account belongs to.
        /// </summary>
        public string UserId { get; set; }
    }
}