namespace Plaid.Net.Data.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a plaid category.
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Gets or sets the list of hierarchy items.
        /// </summary>
        public IList<string> Hierarchy { get; set; }

        /// <summary>
        /// Gets or sets the category id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the category type.
        /// </summary>
        public string Type { get; set; }
    }
}