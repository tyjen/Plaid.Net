namespace Plaid.Net.Models
{
    using System;

    /// <summary>
    /// Represents an address used in <see cref="Transaction"/>s.
    /// </summary>
    public class Address
    {
        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        public float? Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        public float? Longitude { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the street address.
        /// </summary>
        public string Street { get; set; }
    }
}