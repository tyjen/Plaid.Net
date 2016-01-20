namespace Plaid.Net.Models
{
    using System;

    /// <summary>
    /// Represents an option for the user to select to send an auth code to.
    /// </summary>
    /// <remarks>
    /// Some institutions require an MFA code that is sent to your email or phone. 
    /// If there are multiple potential delivery methods, the user may select which they'd like to use.
    /// </remarks>
    public class CodeDeliveryOption
    {
        /// <summary>
        /// Gets or sets the display mask.
        /// </summary>
        /// <example>ty***@outlook.com</example>
        public string Mask { get; set; }

        /// <summary>
        /// Gets or sets the code delivery type.
        /// </summary>
        /// <example>email</example>
        public DeliveryType Type { get; set; }
    }
}