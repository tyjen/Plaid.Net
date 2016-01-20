namespace Plaid.Net.Data.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Contains information needed for the user to perform two-factor authentication.
    /// Only one two-factor auth method will be non-null if auth is required.
    /// </summary>
    public class AuthenticationPrompt
    {
        /// <summary>
        /// Gets or sets the type of two-factor auth.
        /// </summary>
        public AuthType AuthType { get; set; }

        /// <summary>
        /// Gets or sets the list of options to deliver the two-factor auth code.
        /// </summary>
        /// <remarks>
        /// Some institutions require an MFA code that is sent to your email or phone. 
        /// If there are multiple potential delivery methods, the user may select which they'd like to use.
        /// </remarks>
        public IList<CodeOption> CodeDeliveryOptions { get; set; }

        /// <summary>
        /// Gets or sets the device message for two-factor auth.
        /// </summary>
        /// <remarks>
        /// This indicates a multi-factor auth code was sent to a device.
        /// </remarks>
        public string DeviceMessage { get; set; }

        /// <summary>
        /// Gets or sets the list of multiple choice options for two-factor auth.
        /// </summary>
        /// <remarks>
        /// Some institutions require the user to answer a question from a limited set of answers, i.e. multiple-choice.
        /// </remarks>
        public IList<SelectionItem> SelectionOptions { get; set; }

        /// <summary>
        /// Gets or sets the security questions to answer for two-factor auth.
        /// </summary>
        /// <remarks>
        /// Question-based authentication (type=questions) requires the user to answer a security question.
        /// With some financial institutions, multiple security questions may be required
        /// </remarks>
        public IList<string> Questions { get; set; }
    }
}