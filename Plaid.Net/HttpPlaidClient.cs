namespace Plaid.Net
{
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using CuttingEdge.Conditions;
    using Newtonsoft.Json;
    using Plaid.Net.Contracts;
    using Plaid.Net.Contracts.Response;
    using Plaid.Net.Models;
    using Plaid.Net.Utilities;

    /// <summary>
    /// The RESTFUL Http Plaid client.
    /// </summary>
    public class HttpPlaidClient : IPlaidClient
    {
        /// <summary>
        /// The client id provided by Plaid.
        /// </summary>
        private readonly string clientId;

        /// <summary>
        /// The client secret provided by Plaid.
        /// </summary>
        private readonly string clientSecret;

        /// <summary>
        /// The http client.
        /// </summary>
        private readonly IHttpClientWrapper httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpPlaidClient"/> class.
        /// </summary>
        /// <param name="serviceUri">The base uri to Plaid's service.</param>
        /// <param name="clientId">The client id provided by Plaid.</param>
        /// <param name="clientSecret">The client secret provided by Plaid.</param>
        /// <param name="httpClient">The http client to use for requests.</param>
        internal HttpPlaidClient(Uri serviceUri, string clientId, string clientSecret, IHttpClientWrapper httpClient)
        {
            Condition.Requires(clientId).IsNotNullOrWhiteSpace();
            Condition.Requires(clientSecret).IsNotNullOrWhiteSpace();
            Condition.Requires(serviceUri).IsNotNull();
            Condition.Requires(httpClient).IsNotNull();

            this.clientId = clientId;
            this.clientSecret = clientSecret;
            this.httpClient = httpClient;
            this.httpClient.BaseAddress = serviceUri;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpPlaidClient"/> class.
        /// </summary>
        /// <param name="serviceUri">The base uri to Plaid's service.</param>
        /// <param name="clientId">The client id provided by Plaid.</param>
        /// <param name="clientSecret">The client secret provided by Plaid.</param>
        public HttpPlaidClient(Uri serviceUri, string clientId, string clientSecret)
            : this(serviceUri, clientId, clientSecret, new HttpClientWrapper())
        {
        }

        /// <inheritdoc />
        public async Task<AddUserResult> AddUserAsync(string username,string password, InstitutionType institution, AddUserOptions options = null, string pin = null, ApiType api = ApiType.Connect)
        {
            Condition.Requires(username).IsNotNullOrWhiteSpace();
            Condition.Requires(password).IsNotNullOrWhiteSpace();
            Condition.Requires(institution).IsNotNull();

            AddUserRequest userRequest = new AddUserRequest(this.clientId, this.clientSecret)
                {
                    Username = username, 
                    Password = password, 
                    Type = institution.Value, 
                    Options = new AddUserOptionsRequest(options), 
                    Pin = pin
                };

            HttpResponseMessage response = await this.httpClient.PostAsJsonAsync(GetEndpoint(api), userRequest);
            return await this.ProcessAddOrAuthResponse(response);
        }

        /// <inheritdoc />
        public Task<AddUserResult> AuthenticateUserAsync(AccessToken accessToken, DeliveryType deliveryType, bool isUpdate = false, ApiType api = ApiType.Connect)
        {
            Condition.Requires(accessToken).IsNotNull();
            Condition.Requires(deliveryType).IsNotNull();

            AuthUserRequest authRequest = new AuthUserRequest(this.clientId, this.clientSecret, accessToken);
            authRequest.Options = new SendMethodRequest();
            authRequest.Options.SendMethod = new ExpandoObject();
            authRequest.Options.SendMethod.type = deliveryType.Value;

            return this.AuthUserInternal(authRequest, api, isUpdate);
        }

        /// <inheritdoc />
        public Task<AddUserResult> AuthenticateUserAsync(AccessToken accessToken, string deviceMask, bool isUpdate = false, ApiType api = ApiType.Connect)
        {
            Condition.Requires(accessToken).IsNotNull();
            Condition.Requires(deviceMask).IsNotNullOrWhiteSpace();

            AuthUserRequest authRequest = new AuthUserRequest(this.clientId, this.clientSecret, accessToken);
            authRequest.Options = new SendMethodRequest();
            authRequest.Options.SendMethod = new ExpandoObject();
            authRequest.Options.SendMethod.mask = deviceMask;

            return this.AuthUserInternal(authRequest, api, isUpdate);
        }

        /// <inheritdoc />
        public Task<AddUserResult> AuthenticateUserAsync(AccessToken accessToken, bool isUpdate = false, ApiType api = ApiType.Connect, params string[] mfaValues)
        {
            Condition.Requires(accessToken).IsNotNull();
            Condition.Requires(mfaValues).IsNotNull().IsNotEmpty();

            AuthUserRequest authRequest = new AuthUserRequest(this.clientId, this.clientSecret, accessToken);

            if (mfaValues.Length == 1) authRequest.MfaValue = mfaValues.FirstOrDefault();
            else authRequest.MfaValue = new List<string>(mfaValues);

            return this.AuthUserInternal(authRequest, api, isUpdate);
        }

        /// <inheritdoc />
        public async Task<PlaidResult<bool>> DeleteUserAsync(AccessToken accessToken, ApiType api = ApiType.Connect)
        {
            Condition.Requires(accessToken).IsNotNull();

            PlaidRequest deleteRequest = new PlaidRequest(this.clientId, this.clientSecret, accessToken.Value);

            HttpResponseMessage response = await this.httpClient.DeleteAsJsonAsync(GetEndpoint(api), deleteRequest);

            PlaidResult<bool> result = new PlaidResult<bool>(response.StatusCode == HttpStatusCode.OK);
            result.Exception = await this.ParseException(response);

            return result;
        }

        /// <inheritdoc />
        public async Task<PlaidResult<IList<Account>>> GetAccountBalanceAsync(AccessToken accessToken)
        {
            Condition.Requires(accessToken).IsNotNull();

            PlaidRequest balanceRequest = new PlaidRequest(this.clientId, this.clientSecret, accessToken.Value);
            HttpResponseMessage response = await this.httpClient.GetAsJsonAsync("balance", balanceRequest);

            // Re-use add user result since it contains the account objects of interest
            AddUserResult userResult = await this.ProcessAddOrAuthResponse(response);
            PlaidResult<IList<Account>> result = new PlaidResult<IList<Account>>();

            if (userResult.Accounts != null) result.Value = new List<Account>(userResult.Accounts);

            result.Exception = userResult.Exception;

            return result;
        }

        /// <inheritdoc />
        public async Task<PlaidResult<IList<Category>>>  GetCategoriesAsync()
        {
            HttpResponseMessage response = await this.httpClient.GetAsync("categories");
            string responseJson = await response.Content.ReadAsStringAsync();
            PlaidResult<IList<Category>> result = new PlaidResult<IList<Category>>();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                IList<CategoryResponse> categoryResponse = JsonConvert.DeserializeObject<IList<CategoryResponse>>(responseJson);
                result.Value = categoryResponse?.Select(x => x.ToCategory()).ToList();
            }

            result.Exception = await this.ParseException(response, responseJson);
            return result;
        }

        /// <inheritdoc />
        public async Task<PlaidResult<Category>> GetCategoryAsync(string id)
        {
            Condition.Requires(id).IsNotNullOrWhiteSpace();

            HttpResponseMessage response = await this.httpClient.GetAsync("categories/" + id);
            string responseJson = await response.Content.ReadAsStringAsync();
            PlaidResult<Category> result = new PlaidResult<Category>();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                CategoryResponse categoryResponse = JsonConvert.DeserializeObject<CategoryResponse>(responseJson);
                result.Value = categoryResponse?.ToCategory();
            }

            result.Exception = await this.ParseException(response, responseJson);
            return result;
        }

        /// <inheritdoc />
        public async Task<PlaidResult<Institution>> GetInstitutionAsync(string id)
        {
            Condition.Requires(id).IsNotNullOrWhiteSpace();

            HttpResponseMessage response = await this.httpClient.GetAsync("institutions/" + id);
            string responseJson = await response.Content.ReadAsStringAsync();
            PlaidResult<Institution> result = new PlaidResult<Institution>();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                InstitutionResponse instResponse = JsonConvert.DeserializeObject<InstitutionResponse>(responseJson);
                result.Value = instResponse?.ToInstitution();
            }

            result.Exception = await this.ParseException(response, responseJson);
            return result;
        }

        /// <inheritdoc />
        public async Task<PlaidResult<IList<Institution>>> GetInstitutionsAsync()
        {
            HttpResponseMessage response = await this.httpClient.GetAsync("institutions");
            string responseJson = await response.Content.ReadAsStringAsync();
            PlaidResult<IList<Institution>> result = new PlaidResult<IList<Institution>>();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                IList<InstitutionResponse> instResponse = JsonConvert.DeserializeObject<IList<InstitutionResponse>>(responseJson);
                result.Value = instResponse?.Select(x => x.ToInstitution()).ToList();
            }

            result.Exception = await this.ParseException(response, responseJson);
            return result;
        }

        /// <inheritdoc />
        public async Task<TransactionResult> GetTransactionsAsync(AccessToken accessToken, bool? includePending = null, string accountId = null, DateTimeOffset? greaterThanDate = null, DateTimeOffset? lessThanDate = null)
        {
            Condition.Requires(accessToken).IsNotNull();

            GetTransactionsRequest transactionRequest = new GetTransactionsRequest(this.clientId, this.clientSecret, accessToken.Value);

            if (includePending.HasValue || !string.IsNullOrWhiteSpace(accountId) || greaterThanDate.HasValue || lessThanDate.HasValue)
            {
                transactionRequest.Options = new TransactionOptionsRequest
                    {
                        Account = accountId, 
                        GreaterThanDate = greaterThanDate, 
                        LessThanDate = lessThanDate, 
                        Pending = includePending
                    };
            }

            HttpResponseMessage response = await this.httpClient.PostAsJsonAsync(GetEndpoint(ApiType.Connect, "get"), transactionRequest);
            string responseJson = await response.Content.ReadAsStringAsync();

            TransactionResult result = new TransactionResult();

            // 200 OK indicates success, response includes access token and accounts/transactions
            if (response.StatusCode == HttpStatusCode.OK)
            {
                // Can just re-use the AddUserResponse since it contains account and transaction objects
                AddUserResponse userResponse = JsonConvert.DeserializeObject<AddUserResponse>(responseJson);

                if (userResponse.Accounts != null) result.Accounts = userResponse.Accounts.Select(x => x.ToAccount()).ToList();
                if (userResponse.Transactions != null) result.Transactions = userResponse.Transactions.Select(x => x.ToTransaction()).ToList();

                return result;
            }

            result.Exception = await this.ParseException(response, responseJson);
            return result;
        }

        /// <inheritdoc />
        public async Task<AddUserResult> UpdateUserAsync(AccessToken accessToken, string username, string password, string pin = null, Uri webhookUri = null, ApiType api = ApiType.Connect)
        {
            Condition.Requires(accessToken).IsNotNull();
            if ((string.IsNullOrWhiteSpace(username) || password == null) && webhookUri == null) throw new ArgumentException("Username/password required unless only updating webhookUri");

            UpdateUserRequest updateRequest = new UpdateUserRequest(this.clientId, this.clientSecret, accessToken.Value);
            updateRequest.Username = username;
            updateRequest.Password = password;
            updateRequest.Pin = pin;

            if (webhookUri != null) updateRequest.Options = new UpdateUserRequestOptions { Webhook = webhookUri.ToString() };

            HttpResponseMessage response = await this.httpClient.PatchAsJsonAsync(GetEndpoint(api), updateRequest);
            return await this.ProcessAddOrAuthResponse(response);
        }

        /// <inheritdoc />
        public async Task<TokenExchangeResult> ExchangeBankTokenAsync(string publicToken, string accountId)
        {
            Condition.Requires(publicToken).IsNotNullOrWhiteSpace();
            Condition.Requires(accountId).IsNotNullOrWhiteSpace();

            ExchangeTokenRequest exchangeRequest = new ExchangeTokenRequest(this.clientId, this.clientSecret, publicToken, accountId);
            HttpResponseMessage response = await this.httpClient.PostAsJsonAsync("exchange_token", exchangeRequest);
            string responseJson = await response.Content.ReadAsStringAsync();

            TokenExchangeResult result = new TokenExchangeResult();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                ExchangeTokenResponse tokenResponse = JsonConvert.DeserializeObject<ExchangeTokenResponse>(responseJson);

                result.AccessToken = new AccessToken(tokenResponse.AccessToken);
                result.BankAccountToken = tokenResponse.BankToken;

                return result;
            }

            result.Exception = await this.ParseException(response, responseJson);
            return result;
        }

        /// <inheritdoc />
        public async Task<PlaidResult<IList<Account>>> GetAuthAccountDataAsync(AccessToken accessToken)
        {
            Condition.Requires(accessToken).IsNotNull();

            PlaidRequest dataRequest = new PlaidRequest(this.clientId, this.clientSecret, accessToken.Value);
            HttpResponseMessage response = await this.httpClient.PostAsJsonAsync(GetEndpoint(ApiType.Auth, "get"), dataRequest);
            string responseJson = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                // Can re-use AddUserResponse since it has accounts, just ignore transactions
                AddUserResponse dataResponse = JsonConvert.DeserializeObject<AddUserResponse>(responseJson);

                return new PlaidResult<IList<Account>>(dataResponse?.Accounts?.Select(a => a.ToAccount()).ToList());
            }

            PlaidResult<IList<Account>> errorResult = new PlaidResult<IList<Account>>();
            errorResult.Exception = await this.ParseException(response, responseJson);
            return errorResult;
        }

        /// <summary>
        /// Private helper to get the endpoint for an api.
        /// </summary>
        /// <example>
        /// GetEndpoint(ApiType.Connect, "get") => "connect/get"
        /// </example>
        private static string GetEndpoint(ApiType api, string path)
        {
            string endpoint = HttpPlaidClient.GetEndpoint(api);
            return $"{endpoint}/{path}";
        }

        /// <summary>
        /// Private helper to get the endpoint for an api.
        /// </summary>
        private static string GetEndpoint(ApiType api)
        {
            switch (api)
            {
                case ApiType.Connect:
                    return "connect";
                case ApiType.Auth:
                    return "auth";
            }

            throw new InvalidOperationException("Cannot get endpoint for unknown ApiType: " + api);
        }

        /// <summary>
        /// Internal helper for user auth.
        /// </summary>
        private async Task<AddUserResult> AuthUserInternal(AuthUserRequest request, ApiType api, bool isUpdate)
        {
            string endpoint = GetEndpoint(api, "step");

            HttpResponseMessage response = isUpdate
                                               ? await this.httpClient.PatchAsJsonAsync(endpoint, request)
                                               : await this.httpClient.PostAsJsonAsync(endpoint, request);

            return await this.ProcessAddOrAuthResponse(response);
        }

        /// <summary>
        /// Helper to parse exceptions from a plaid service response.
        /// </summary>
        private async Task<PlaidException> ParseException(HttpResponseMessage response, string responseJson = null)
        {
            if (response.IsSuccessStatusCode) return null;

            if (responseJson == null) responseJson = await response.Content.ReadAsStringAsync();

            ErrorResponse errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(responseJson);
            return errorResponse?.ToException(response.StatusCode);
        }

        /// <summary>
        /// Private helper to handle responses from Add or Auth user API calls.
        /// </summary>
        private async Task<AddUserResult> ProcessAddOrAuthResponse(HttpResponseMessage response)
        {
            string responseJson = await response.Content?.ReadAsStringAsync();
            AddUserResult result = new AddUserResult();

            // 201 CREATED indicates success but requires multi-factor authentication
            if (response.StatusCode == HttpStatusCode.Created)
            {
                MfaResponse mfaResponse = JsonConvert.DeserializeObject<MfaResponse>(responseJson);
                AuthenticationPrompt mfaPrompt = mfaResponse?.ToAuthenticationPrompt();

                result.AccessToken = new AccessToken(mfaResponse?.AccessToken);
                result.AuthPrompt = mfaPrompt;
                return result;
            }

            // 200 OK indicates success, response includes access token and optionally accounts/transactions
            if (response.StatusCode == HttpStatusCode.OK)
            {
                AddUserResponse userResponse = JsonConvert.DeserializeObject<AddUserResponse>(responseJson);
                result.AccessToken = new AccessToken(userResponse.AccessToken);

                if (userResponse.Accounts != null) result.Accounts = userResponse.Accounts.Select(x => x.ToAccount()).ToList();

                if (userResponse.Transactions != null) result.Transactions = userResponse.Transactions.Select(x => x.ToTransaction()).ToList();

                return result;
            }

            result.Exception = await this.ParseException(response, responseJson);
            return result;
        }
    }
}