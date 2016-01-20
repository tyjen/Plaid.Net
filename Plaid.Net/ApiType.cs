namespace Plaid.Net
{
    using System;

    /// <summary>
    /// Enum used to indicate the connect or auth api endpoints.
    /// </summary>
    public enum ApiType
    {
        /// <summary>
        /// The /connect api.
        /// </summary>
        Connect = 0,

        /// <summary>
        /// The /auth api.
        /// </summary>
        Auth = 1
    }
}