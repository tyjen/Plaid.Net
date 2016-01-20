namespace Plaid.Net.Data.Models
{
    using System;
    using Tyjen.Net.Core.Identifiers;

    /// <summary>
    /// Identifier for the different delivery types for <see cref="CodeOption"/> multi-factor auth.
    /// </summary>
    public class DeliveryType : StringIdentifier
    {
        /// <summary>
        /// SafePass card.
        /// </summary>
        public static readonly DeliveryType Card = new DeliveryType("card");

        /// <summary>
        /// Email.
        /// </summary>
        public static readonly DeliveryType Email = new DeliveryType("email");

        /// <summary>
        /// Mobile phone.
        /// </summary>
        public static readonly DeliveryType Phone = new DeliveryType("phone");

        /// <summary>
        /// Initializes a new instance of the <see cref="DeliveryType"/> class.
        /// </summary>
        /// <param name="value">The string delivery type.</param>
        public DeliveryType(string value)
            : base(value)
        {
        }
    }
}