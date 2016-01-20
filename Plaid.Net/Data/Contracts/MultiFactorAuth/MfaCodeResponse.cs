namespace Plaid.Net.Contracts
{
    using System;
    using Newtonsoft.Json;
    using Plaid.Net.Models;

    /// <summary>
    /// A response which specifies a device to send an auth code to.
    /// Contract for <see cref="CodeDeliveryOption"/>
    /// </summary>
    /// <remarks>
    /// Some institutions require an MFA code that is sent to your email or phone.
    /// If there are multiple potential delivery methods, we allow you to specify 
    /// which you'd like to use.
    /// </remarks>
    internal class MfaCodeResponse
    {
        /// <summary>
        /// Gets or sets the masked display name.
        /// </summary>
        [JsonProperty("mask")]
        public string Mask { get; set; }

        /// <summary>
        /// Gets or sets the type of device.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// Converts the contract into it's <see cref="CodeDeliveryOption"/> model.
        /// </summary>
        /// <returns>The <see cref="CodeDeliveryOption"/> model.</returns>
        public CodeDeliveryOption ToCodeOption()
        {
            return new CodeDeliveryOption { Mask = this.Mask, Type = new DeliveryType(this.Type) };
        }
    }
}