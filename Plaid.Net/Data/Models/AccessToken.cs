namespace Plaid.Net.Data.Models
{
    using System;
    using Tyjen.Net.Core.Identifiers;

    /// <summary>
    /// Identifier for an access token which is used to identify a user.
    /// </summary>
    /// <remarks>
    /// Once a user is authenticated with a banking institution, an access token
    /// is return which should be used for subsequent calls to Plaid.
    /// </remarks>
    public class AccessToken : StringIdentifier
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccessToken"/> class.
        /// </summary>
        /// <param name="token">The string access token.</param>
        public AccessToken(string token)
            : base(token)
        {
            // No-op
        }
    }
}