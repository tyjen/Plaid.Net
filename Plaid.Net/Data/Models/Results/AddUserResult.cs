// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AddUserResponse.cs" company="">
// </copyright>
// <summary>
//   The add user response.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Plaid.Net.Data.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The result of an AddUser operation against Plaid.
    /// </summary>
    public class AddUserResult : PlaidResult
    {
        /// <summary>
        /// Gets the access token returned on success.
        /// </summary>
        public AccessToken AccessToken { get; internal set; }

        /// <summary>
        /// Gets the accounts returned from the operation.
        /// If LoginOnly is specified, Accounts will be null.
        /// </summary>
        public IList<Account> Accounts { get; internal set; }

        /// <summary>
        /// Gets the two-factor auth prompt if required. If this is not null,
        /// the user will need to provide two-factor auth.
        /// </summary>
        public AuthenticationPrompt AuthPrompt { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether is two-factor auth is required.
        /// </summary>
        public bool IsMfaRequired => this.AuthPrompt != null;

        /// <summary>
        /// Gets the transactions returned from the operation.
        /// If LoginOnly is specified, Transactions will be null.
        /// </summary>
        public IList<Transaction> Transactions { get; internal set; }
    }
}