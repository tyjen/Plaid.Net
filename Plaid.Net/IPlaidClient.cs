namespace Plaid.Net
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Plaid.Net.Models;

    /// <summary>
    /// The PlaidClient interface.
    /// </summary>
    public interface IPlaidClient
    {
        /// <summary>
        /// Adds a new user into Plaid's system.
        /// </summary>
        /// <param name="username">Username associated with the user's financial institution.</param>
        /// <param name="password">Password associated with the user's financial institution.</param>
        /// <param name="institution">The banking institution to auth with.</param>
        /// <param name="options">Additional options for the request.</param>
        /// <param name="api">Which API endpoint to use.</param>
        /// <param name="pin">(Usaa only) Pin number associated with the user's financial institution.</param>
        /// <returns>The <see cref="AddUserResult"/> containing the access token, multi-factor auth if required, or error details.</returns>
        Task<AddUserResult> AddUserAsync(string username, string password, InstitutionType institution, AddUserOptions options = null, string pin = null, ApiType api = ApiType.Connect);

        /// <summary>
        /// Two-factor authenticates a user based on previous calls to Add or Update user.
        /// </summary>
        /// <param name="deviceMask">The mask of the device to send the two-factor auth code to.</param>
        /// <param name="accessToken">The access token of the user to authenticate.</param>
        /// <param name="api">Which API endpoint to use.</param>
        /// <param name="isUpdate">True if this auth is in response to <see cref="UpdateUserAsync"/>.</param>
        /// <returns>The <see cref="AddUserResult"/>.</returns>
        Task<AddUserResult> AuthenticateUserAsync(AccessToken accessToken, string deviceMask, bool isUpdate = false, ApiType api = ApiType.Connect);

        /// <summary>
        /// Two-factor authenticates a user based on previous calls to Add or Update user.
        /// </summary>
        /// <param name="accessToken">The access token of the user to authenticate.</param>
        /// <param name="deliveryType">The type of device to send the two-factor auth code to.</param>
        /// <param name="api">Which API endpoint to use.</param>
        /// <param name="isUpdate">True if this auth is in response to <see cref="UpdateUserAsync"/>.</param>
        /// <returns>The <see cref="AddUserResult"/>.</returns>
        Task<AddUserResult> AuthenticateUserAsync(AccessToken accessToken, DeliveryType deliveryType, bool isUpdate = false, ApiType api = ApiType.Connect);

        /// <summary>
        /// Two-factor authenticates a user based on previous calls to Add or Update user.
        /// </summary>
        /// <param name="accessToken">The access token of the user to authenticate.</param>
        /// <param name="api">Which API endpoint to use.</param>
        /// <param name="isUpdate">True if this auth is in response to <see cref="UpdateUserAsync"/>.</param>
        /// <param name="mfaValues">The value (answer(s)) provided by the user for two-factor auth.</param>
        /// <returns>The <see cref="AddUserResult"/>.</returns>
        Task<AddUserResult> AuthenticateUserAsync(AccessToken accessToken, bool isUpdate = false, ApiType api = ApiType.Connect, params string[] mfaValues);

        /// <summary>
        /// Removes a user from Plaid's system.
        /// </summary>
        /// <param name="accessToken">The access token of the user to remove.</param>
        /// <param name="api">Which API endpoint to use.</param>
        /// <returns>True if the user was successfully removed, false otherwise.</returns>
        Task<PlaidResult<bool>> DeleteUserAsync(AccessToken accessToken, ApiType api = ApiType.Connect);

        /// <summary>
        /// Gets account balances for the given user.
        /// </summary>
        /// <param name="accessToken">The access token of the user to get balances for.</param>
        /// <returns>Async <see cref="Task"/>.</returns>
        Task<PlaidResult<IList<Account>>> GetAccountsAsync(AccessToken accessToken);

        /// <summary>
        /// Gets a list of detailed category information.
        /// </summary>
        /// <returns>List of all categories in plaid.</returns>
        Task<PlaidResult<IList<Category>>> GetCategoriesAsync();

        /// <summary>
        /// Gets details on a single category.
        /// </summary>
        /// <param name="id">Id of the category to get info for.</param>
        /// <returns>The associated <see cref="Category"/>.</returns>
        Task<PlaidResult<Category>> GetCategoryAsync(string id);

        /// <summary>
        /// Gets specific institution details.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns><see cref="Institution"/> info.</returns>
        Task<PlaidResult<Institution>> GetInstitutionAsync(string id);

        /// <summary>
        /// Gets a list of known financial institutions supported by Plaid.
        /// </summary>
        /// <returns>List of supported <see cref="Institution"/>s.</returns>
        Task<PlaidResult<IList<Institution>>> GetInstitutionsAsync();

        /// <summary>
        /// Gets a list of the users <see cref="Transaction"/>s.
        /// </summary>
        /// <param name="accessToken">The access token of the user to get transactions for.</param>
        /// <param name="includePending">If set to true, transactions from account activities that have not yet posted to the account will be returned.</param>
        /// <param name="accountId">Collect transactions for a specific account only, using an account id returned from the original AddUser operation.</param>
        /// <param name="greaterThanDate">Collect all recent transactions since and including the given date.</param>
        /// <param name="lessThanDate">Collect all transactions up to and including the given date. Can be used with greaterThanDate to define a range.</param>
        /// <returns>List of user's <see cref="Transaction"/>s.</returns>
        Task<TransactionResult> GetTransactionsAsync(AccessToken accessToken, bool? includePending = false, string accountId = null, DateTimeOffset? greaterThanDate = null, DateTimeOffset? lessThanDate = null);

        /// <summary>
        /// Updates an existing user in Plaid's system. This is required if a user's credentials have been updated.
        /// The webhook can also be updated without username/password updates.
        /// </summary>
        /// <param name="accessToken">The access token of the user being updated.</param>
        /// <param name="username">Username associated with the user's financial institution.</param>
        /// <param name="password">Password associated with the user's financial institution.</param>
        /// <param name="api">Which API endpoint to use.</param>
        /// <param name="pin">Pin number associated with the user's financial institution.</param>
        /// <param name="webhookUri">The updated webhook uri.</param>
        /// <returns>Async <see cref="Task"/>.</returns>
        Task<AddUserResult> UpdateUserAsync(AccessToken accessToken, string username, string password, string pin = null, Uri webhookUri = null, ApiType api = ApiType.Connect);

        /// <summary>
        /// Exchanges a public token and account id for an <see cref="AccessToken"/> and bank account token.
        /// </summary>
        /// <remarks>
        /// This API is used for Plaid/Stripe integration. More info here:
        /// https://plaid.com/docs/link/stripe
        /// </remarks>
        /// <param name="publicToken">The public token from Plaid Link.</param>
        /// <param name="accountId">The account identifier from Plaid Link.</param>
        /// <returns>The <see cref="TokenExchangeResult"/> from the operation.</returns>
        Task<TokenExchangeResult> ExchangeBankTokenAsync(string publicToken, string accountId);

        /// <summary>
        /// Gets the user's account and routing information.
        /// </summary>
        /// <param name="accessToken">The access token of the user to get data for.</param>
        Task<PlaidResult<IList<Account>>> GetAuthAccountDataAsync(AccessToken accessToken);
    }
}