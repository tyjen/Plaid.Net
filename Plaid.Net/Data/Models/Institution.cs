namespace Plaid.Net.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a financial institution.
    /// </summary>
    public class Institution
    {
        /// <summary>
        /// Gets or sets a value indicating whether institution requires mfa.
        /// </summary>
        public bool HasMfa { get; set; }

        /// <summary>
        /// Gets or sets the institution id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets a list of mfa descriptions if the institution requires mfa.
        /// </summary>
        public IList<string> MfaDescriptions { get; set; }

        /// <summary>
        /// Gets or sets the institution name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the institution's password hint.
        /// </summary>
        public string PasswordHint { get; set; }

        /// <summary>
        /// Gets or sets the institution's type.
        /// </summary>
        public InstitutionType Type { get; set; }

        /// <summary>
        /// Gets or sets the institution's username hint.
        /// </summary>
        public string UsernameHint { get; set; }
    }
}