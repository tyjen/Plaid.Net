namespace Plaid.Net.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Plaid.Net.Data.Models;

    /// <summary>
    /// Contract for a transaction response from Plaid.
    /// </summary>
    internal class TransactionResponse
    {
        /// <summary>
        /// Gets or sets the id of the account in which this transaction occurred.
        /// </summary>
        [JsonProperty("_account")]
        public string AccountId { get; set; }

        /// <summary>
        /// Gets or sets the transaction amount.
        /// </summary>
        /// <remarks>
        /// The settled dollar value. Positive values when money moves out of the account; negative values when money moves in.
        /// </remarks>
        [JsonProperty("amount")]
        public double Amount { get; set; }

        /// <summary>
        /// An hierarchical array of the categories to which this transaction belongs.
        /// </summary>
        [JsonProperty("category")]
        public IList<string> Categories { get; set; }

        /// <summary>
        /// Gets or sets the id of the category to which this transaction belongs.
        /// </summary>
        [JsonProperty("category_id")]
        public string CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the date that the transaction was posted by the financial institution (in ISO 8601 format).
        /// </summary>
        [JsonProperty("date")]
        public DateTimeOffset? Date { get; set; }

        /// <summary>
        /// Gets or sets the metadata. Usually location or contact information.
        /// TODO: Explicitly parse out contact info.
        /// </summary>
        [JsonProperty("meta")]
        public dynamic Metadata { get; set; }

        /// <summary>
        /// Gets or sets the transaction name.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this transaction is pending.
        /// </summary>
        /// <remarks>
        /// When true, identifies the transaction as pending or unsettled. Pending transaction details (name, type, amount) may change before they are settled.
        /// </remarks>
        [JsonProperty("pending")]
        public bool Pending { get; set; }

        /// <summary>
        /// A numeric representation of our confidence in the meta data Plaid attached to the transaction.
        /// </summary>
        [JsonProperty("score")]
        public dynamic Score { get; set; }

        /// <summary>
        /// Gets or sets the transaction id.
        /// </summary>
        [JsonProperty("_id")]
        public string TransactionId { get; set; }

        /// <summary>
        /// Gets or sets the transaction type. Can be Place, Digital, or Special.
        /// TODO: Parse this explicitly.
        /// </summary>
        [JsonProperty("type")]
        public dynamic Type { get; set; }

        /// <summary>
        /// Converts this contract into its <see cref="Transaction"/> model.
        /// </summary>
        /// <returns>The <see cref="Transaction"/> model.</returns>
        public Transaction ToTransaction()
        {
            Transaction trans = new Transaction();
            trans.Metadata = this.Metadata;
            trans.AccountId = this.AccountId;
            trans.Amount = this.Amount;
            trans.CategoryId = this.CategoryId;
            trans.Name = this.Name;
            trans.Score = this.Score;
            trans.TransactionId = this.TransactionId;
            trans.Type = this.Type;
            trans.IsPending = this.Pending;

            if (this.Date.HasValue) trans.Date = new DateTimeOffset(this.Date.Value.DateTime, TimeSpan.Zero);

            if (this.Categories != null) trans.Categories = new List<string>(this.Categories);

            if (this.Metadata.location != null)
            {
                Address address = new Address();
                address.City = this.Metadata.location.city;
                address.State = this.Metadata.location.state;
                address.Street = this.Metadata.location.address;

                if (this.Metadata.location.coordinates != null)
                {
                    address.Latitude = this.Metadata.location.coordinates.lat;
                    address.Longitude = this.Metadata.location.coordinates.lon;
                }

                trans.Location = address;
            }

            return trans;
        }
    }
}