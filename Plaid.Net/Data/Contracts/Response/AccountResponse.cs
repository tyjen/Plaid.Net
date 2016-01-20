namespace Plaid.Net.Contracts
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Plaid.Net.Models;

    /// <summary>
    /// The response contract for a bank account.
    /// </summary>
    internal class AccountResponse
    {
        /// <summary>
        /// Additional information pertaining to the account such as the limit, name, or last few digits of the account number.
        /// </summary>
        [JsonProperty("meta")]
        public IDictionary<string, string> Metadata;

        /// <summary>
        /// A more detailed classification of the account type. When unavailable, this field will not be returned.
        /// </summary>
        [JsonProperty("subtype")]
        public string AccountSubtype { get; set; }

        /// <summary>
        /// Gets or sets the type of account.
        /// </summary>
        [JsonProperty("type")]
        public string AccountType { get; set; }

        /// <summary>
        /// Gets or sets the account balance.
        /// </summary>
        /// <remarks>
        /// The Current Balance is the total amount of funds in the account. The Available Balance is the Current Balance 
        /// less any outstanding holds or debits that have not yet posted to the account. Note that not all institutions 
        /// calculate the Available Balance. In the case that Available Balance is unavailable from the institution, 
        /// Plaid will either return an Available Balance value of null or only return a Current Balance.
        /// </remarks>
        [JsonProperty("balance")]
        public BalanceResponse Balance { get; set; }

        /// <summary>
        /// Gets or sets the unique id of the account.
        /// </summary>
        [JsonProperty("_id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the type of financial institution associated with the account.
        /// </summary>
        [JsonProperty("institution_type")]
        public string InstitutionType { get; set; }

        /// <summary>
        /// Gets or sets the id unique to the accounts of a particular access token.
        /// </summary>
        [JsonProperty("_item")]
        public string Item { get; set; }

        /// <summary>
        /// Gets or sets the user id the account belongs to.
        /// </summary>
        [JsonProperty("_user")]
        public string User { get; set; }

        /// <summary>
        /// Converts this contract into its <see cref="Account"/> model.
        /// </summary>
        /// <returns>The <see cref="Account"/> model.</returns>
        public Account ToAccount()
        {
            Account account = new Account();
            account.AccountType = new AccountType(this.AccountType);
            account.AccountSubtype = this.AccountSubtype != null ? (new AccountSubType(this.AccountSubtype)) : null;
            account.AvailableBalance = this.Balance.Available;
            account.CurrentBalance = this.Balance.Current;
            account.Id = this.Id;
            account.InstitutionType = new InstitutionType(this.InstitutionType);
            account.ItemId = this.Item;
            account.UserId = this.User;
            account.Metadata = new Dictionary<string, string>(this.Metadata);

            return account;
        }
    }
}