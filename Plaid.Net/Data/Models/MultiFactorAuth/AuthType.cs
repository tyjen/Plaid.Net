namespace Plaid.Net.Models
{
    using System;
    using Plaid.Net.Utilities;

    /// <summary>
    /// Identifier for the different types of two-factor auth.
    /// </summary>
    public class AuthType : StringIdentifier
    {
        /// <summary>
        /// Code-based two-factor auth.
        /// </summary>
        /// <remarks>
        /// Some institutions require an MFA code that is sent to your email or phone. 
        /// If there are multiple potential delivery methods, the user may select which they'd like to use.
        /// </remarks>
        public static readonly AuthType Code = new AuthType("list");

        /// <summary>
        /// Indicates a mfa code was sent to one of the user's devices.
        /// </summary>
        public static readonly AuthType Device = new AuthType("device");

        /// <summary>
        /// Question-based two-factor auth.
        /// </summary>
        /// <remarks>
        /// Question-based authentication (type=questions) requires the user to answer a security question.
        /// With some financial institutions, multiple security questions may be required
        /// </remarks>
        public static readonly AuthType Questions = new AuthType("questions");

        /// <summary>
        /// Multiple choice (selection) based two-factor auth.
        /// </summary>
        /// <remarks>
        /// Some institutions require the user to answer a question from a limited set of answers, i.e. multiple-choice.
        /// </remarks>
        public static readonly AuthType Selection = new AuthType("selections");

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthType"/> class.
        /// </summary>
        /// <param name="value">The string auth type.</param>
        public AuthType(string value)
            : base(value)
        {
            // No-op
        }
    }
}