namespace Plaid.Net.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Plaid.Net.Models;
    using Plaid.Net.Models.Results;
    using Tyjen.Net.Http;

    /// <summary>
    /// Tests for <see cref="HttpPlaidClient"/>
    /// </summary>
    [TestClass]
    public class HttpPlaidClientTests : BaseTestClass
    {
        [TestMethod]
        public async Task AddUserLoginOnlySuccess()
        {
            IHttpClientWrapper httpClient = this.GetMockHttpClient("AddUserSuccess.json", HttpStatusCode.OK, HttpMethod.Post);
            IPlaidClient testClient = this.GetPlaidClient(httpClient);
            AddUserResult result = await testClient.AddUserAsync(BaseTestClass.TestUsername, BaseTestClass.TestPassword, BaseTestClass.TestInstitution);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsError);
            Assert.IsFalse(result.IsMfaRequired);
            Assert.IsNotNull(result.AccessToken);

            // For some reason their test account returns this data no matter what
            // Assert.IsNull(result.Accounts);
            // Assert.IsNull(result.Transactions);
        }

        [TestMethod]
        public async Task AddUserWithPinSuccess()
        {
            IHttpClientWrapper httpClient = this.GetMockHttpClient("AddUserPinSuccess.json", HttpStatusCode.Created, HttpMethod.Post);
            IPlaidClient testClient = this.GetPlaidClient(httpClient);
            AddUserResult result = await testClient.AddUserAsync(BaseTestClass.TestUsername, BaseTestClass.TestPassword, InstitutionType.Usaa, null, "1234");

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsError);
            Assert.IsTrue(result.IsMfaRequired);
            Assert.IsNotNull(result.AccessToken);
        }

        [TestMethod]
        public async Task AddUserNoPin()
        {
            IHttpClientWrapper httpClient = this.GetMockHttpClient("MissingCredentials.json", HttpStatusCode.BadRequest, HttpMethod.Post);
            IPlaidClient testClient = this.GetPlaidClient(httpClient);
            AddUserResult result = await testClient.AddUserAsync(BaseTestClass.TestUsername, BaseTestClass.TestPassword, InstitutionType.Usaa);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsError);
            Assert.IsFalse(result.IsMfaRequired);

            Assert.AreEqual(400, result.Exception.HttpStatusCode);
            Assert.AreEqual(ErrorCode.CredentialsMissing, result.Exception.ErrorCode);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.Exception.Message));
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.Exception.Resolution));
        }

        [TestMethod]
        public async Task AddUserInvalidPin()
        {
            IHttpClientWrapper httpClient = this.GetMockHttpClient("InvalidPin.json", HttpStatusCode.PaymentRequired, HttpMethod.Post);
            IPlaidClient testClient = this.GetPlaidClient(httpClient);
            AddUserResult result = await testClient.AddUserAsync(BaseTestClass.TestUsername, BaseTestClass.TestPassword, InstitutionType.Usaa, null, "4567");

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsError);
            Assert.IsFalse(result.IsMfaRequired);

            Assert.AreEqual(402, result.Exception.HttpStatusCode);
            Assert.AreEqual(ErrorCode.InvalidPin, result.Exception.ErrorCode);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.Exception.Message));
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.Exception.Resolution));
        }

        [TestMethod]
        public async Task AddUserInvalidCreds()
        {
            IHttpClientWrapper httpClient = this.GetMockHttpClient("InvalidCredentials.json", HttpStatusCode.PaymentRequired, HttpMethod.Post);
            IPlaidClient testClient = this.GetPlaidClient(httpClient);
            AddUserResult result = await testClient.AddUserAsync("test_bad", BaseTestClass.TestPassword, BaseTestClass.TestInstitution);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsError);
            Assert.IsNotNull(result.Exception);
            Assert.AreEqual(402, result.Exception.HttpStatusCode);
            Assert.AreEqual(ErrorCode.InvalidCredentials, result.Exception.ErrorCode);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.Exception.Message));
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.Exception.Resolution));
        }

        [TestMethod]
        public async Task AddUserLocked()
        {
            IHttpClientWrapper httpClient = this.GetMockHttpClient("AccountLocked.json", HttpStatusCode.PaymentRequired, HttpMethod.Post);
            IPlaidClient testClient = this.GetPlaidClient(httpClient);
            AddUserResult result = await testClient.AddUserAsync(BaseTestClass.TestUsername, "plaid_locked", BaseTestClass.TestInstitution);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsError);
            Assert.IsNotNull(result.Exception);
            Assert.AreEqual(402, result.Exception.HttpStatusCode);
            Assert.AreEqual(ErrorCode.AccountLocked, result.Exception.ErrorCode);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.Exception.Message));
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.Exception.Resolution));
        }

        [TestMethod]
        public async Task AddUserQuestionAuth()
        {
            IHttpClientWrapper httpClient = this.GetMockHttpClient("QuestionMfa.json", HttpStatusCode.Created, HttpMethod.Post);
            IPlaidClient testClient = this.GetPlaidClient(httpClient);
            AddUserResult result = await testClient.AddUserAsync(BaseTestClass.TestUsername, BaseTestClass.TestPassword, InstitutionType.UsBank);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsError);
            Assert.IsTrue(result.IsMfaRequired);

            Assert.AreEqual(AuthType.Questions, result.AuthPrompt.AuthType);
            Assert.IsNotNull(result.AuthPrompt.Questions);
            Assert.AreEqual(1, result.AuthPrompt.Questions.Count);
        }

        [TestMethod]
        public async Task AddUserCodeAuth()
        {
            IHttpClientWrapper httpClient = this.GetMockHttpClient("DeviceCodeMfa.json", HttpStatusCode.Created, HttpMethod.Post);
            IPlaidClient testClient = this.GetPlaidClient(httpClient);
            AddUserResult result = await testClient.AddUserAsync(BaseTestClass.TestUsername, BaseTestClass.TestPassword, InstitutionType.Chase);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsError);
            Assert.IsTrue(result.IsMfaRequired);

            Assert.AreEqual(AuthType.Device, result.AuthPrompt.AuthType);
            Assert.IsNull(result.AuthPrompt.CodeDeliveryOptions);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.AuthPrompt.DeviceMessage));
        }

        [TestMethod]
        public async Task AddUserListCodeAuth()
        {
            IHttpClientWrapper httpClient = this.GetMockHttpClient("DeviceListMfa.json", HttpStatusCode.Created, HttpMethod.Post);
            IPlaidClient testClient = this.GetPlaidClient(httpClient);
            AddUserOptions options = new AddUserOptions { IncludeMfaList = true };
            AddUserResult result = await testClient.AddUserAsync(BaseTestClass.TestUsername, BaseTestClass.TestPassword, InstitutionType.Chase, options);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsError);
            Assert.IsTrue(result.IsMfaRequired);

            Assert.AreEqual(AuthType.Code, result.AuthPrompt.AuthType);
            Assert.IsNotNull(result.AuthPrompt.CodeDeliveryOptions);
            Assert.AreEqual(2, result.AuthPrompt.CodeDeliveryOptions.Count);

            Assert.IsFalse(string.IsNullOrWhiteSpace(result.AuthPrompt.CodeDeliveryOptions[0].Mask));
            Assert.AreEqual(DeliveryType.Phone, result.AuthPrompt.CodeDeliveryOptions[0].Type);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.AuthPrompt.CodeDeliveryOptions[1].Mask));
            Assert.AreEqual(DeliveryType.Email, result.AuthPrompt.CodeDeliveryOptions[1].Type);
        }

        [TestMethod]
        public async Task AddUserSelectionAuth()
        {
            IHttpClientWrapper httpClient = this.GetMockHttpClient("SelectionMfa.json", HttpStatusCode.Created, HttpMethod.Post);
            IPlaidClient testClient = this.GetPlaidClient(httpClient);
            AddUserResult result = await testClient.AddUserAsync("plaid_selections", BaseTestClass.TestPassword, InstitutionType.Citi);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsError);
            Assert.IsTrue(result.IsMfaRequired);

            Assert.AreEqual(AuthType.Selection, result.AuthPrompt.AuthType);
            Assert.IsNotNull(result.AuthPrompt.MultipleChoiceQuestions);
            Assert.AreEqual(2, result.AuthPrompt.MultipleChoiceQuestions.Count);

            Assert.IsFalse(string.IsNullOrWhiteSpace(result.AuthPrompt.MultipleChoiceQuestions[0].Question));
            Assert.AreEqual(2, result.AuthPrompt.MultipleChoiceQuestions[0].Options.Count);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.AuthPrompt.MultipleChoiceQuestions[1].Question));
            Assert.AreEqual(2, result.AuthPrompt.MultipleChoiceQuestions[1].Options.Count);
        }

        [TestMethod]
        public async Task AuthUserSingleQuestion()
        {
            IHttpClientWrapper httpClient = this.GetMockHttpClient("AuthUserUsBank.json", HttpStatusCode.OK, HttpMethod.Post, "connect/step");
            IPlaidClient testClient = this.GetPlaidClient(httpClient);
            AccessToken token = new AccessToken("test_us");
            AddUserResult result = await testClient.AuthenticateUserAsync(token, false, ApiType.Connect, "tomato");

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsError);
            Assert.IsFalse(result.IsMfaRequired);
            Assert.IsNotNull(result.AccessToken);
        }

        [TestMethod]
        public async Task AuthUserMultipleQuestions()
        {
            IHttpClientWrapper httpClient = this.GetMockHttpClient("QuestionMfa.json", HttpStatusCode.Created, HttpMethod.Post, "connect/step");
            IPlaidClient testClient = this.GetPlaidClient(httpClient);testClient = this.GetPlaidClient(httpClient);
            AccessToken token = new AccessToken("test_us");
            AddUserResult result = await testClient.AuthenticateUserAsync(token, false, ApiType.Connect, "again");

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsError);
            Assert.IsTrue(result.IsMfaRequired);
            Assert.IsNotNull(result.AuthPrompt);
            Assert.IsNotNull(result.AuthPrompt.Questions);
            Assert.IsNotNull(result.AccessToken);

            httpClient = this.GetMockHttpClient("AuthUserUsBank.json", HttpStatusCode.OK, HttpMethod.Post, "connect/step");
            testClient = this.GetPlaidClient(httpClient);
            result = await testClient.AuthenticateUserAsync(token, false, mfaValues: "tomato");

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsError);
            Assert.IsFalse(result.IsMfaRequired);
            Assert.IsNotNull(result.AccessToken);
        }

        [TestMethod]
        public async Task AuthUserCodeMask()
        {
            IHttpClientWrapper httpClient = this.GetMockHttpClient("DeviceCodeMfa.json", HttpStatusCode.Created, HttpMethod.Post, "connect/step");
            IPlaidClient testClient = this.GetPlaidClient(httpClient);
            AddUserResult result = await testClient.AuthenticateUserAsync(new AccessToken("test_chase"), "xxx-xxx-5309");

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsError);
            Assert.IsTrue(result.IsMfaRequired);

            Assert.AreEqual(AuthType.Device, result.AuthPrompt.AuthType);
            Assert.IsNull(result.AuthPrompt.CodeDeliveryOptions);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.AuthPrompt.DeviceMessage));
        }

        [TestMethod]
        public async Task AuthUserCodeDeviceType()
        {
            IHttpClientWrapper httpClient = this.GetMockHttpClient("DeviceCodeMfa.json", HttpStatusCode.Created, HttpMethod.Post, "connect/step");
            IPlaidClient testClient = this.GetPlaidClient(httpClient);
            AddUserResult result = await testClient.AuthenticateUserAsync(new AccessToken("test_chase"), DeliveryType.Phone);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsError);
            Assert.IsTrue(result.IsMfaRequired);

            Assert.AreEqual(AuthType.Device, result.AuthPrompt.AuthType);
            Assert.IsNull(result.AuthPrompt.CodeDeliveryOptions);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.AuthPrompt.DeviceMessage));
        }

        [TestMethod]
        public async Task AuthUserSelection()
        {
            IHttpClientWrapper httpClient = this.GetMockHttpClient("AuthUserCiti.json", HttpStatusCode.OK, HttpMethod.Post, "connect/step");
            IPlaidClient testClient = this.GetPlaidClient(httpClient);
            AddUserResult result = await testClient.AuthenticateUserAsync(new AccessToken("test_citi"), false, ApiType.Connect, "tomato", "ketchup");

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsError);
            Assert.IsFalse(result.IsMfaRequired);
            Assert.IsNull(result.AuthPrompt);
        }

        [TestMethod]
        public async Task AuthUserInvalidAnswer()
        {
            IHttpClientWrapper httpClient = this.GetMockHttpClient("InvalidMfa.json", HttpStatusCode.PaymentRequired, HttpMethod.Post, "connect/step");
            IPlaidClient testClient = this.GetPlaidClient(httpClient);
            AddUserResult result = await testClient.AuthenticateUserAsync(new AccessToken("test_citi"), false, ApiType.Connect, "tomato");

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsError);
            Assert.IsFalse(result.IsMfaRequired);

            Assert.IsNotNull(result.Exception);
            Assert.AreEqual(ErrorCode.InvalidMfa, result.Exception.ErrorCode);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.Exception.Message));
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.Exception.Resolution));
        }

        [TestMethod]
        public async Task DeleteUserSuccess()
        {
            Mock<IHttpClientWrapper> mockHttpClient = new Mock<IHttpClientWrapper>();
            mockHttpClient.Setup(h => h.DeleteAsJsonAsync("connect", It.IsNotNull<object>()))
                          .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK));

            IPlaidClient testClient = this.GetPlaidClient();
            var result = await testClient.DeleteUserAsync(new AccessToken("test_citi"));

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsError);
            Assert.IsTrue(result.Value);
        }

        [TestMethod]
        public async Task GetTransactionsSuccess()
        {
            IHttpClientWrapper httpClient = this.GetMockHttpClient("AuthUserUsBank.json", HttpStatusCode.OK, HttpMethod.Post, "connect/get");
            IPlaidClient testClient = this.GetPlaidClient(httpClient);

            TransactionResult result = await testClient.GetTransactionsAsync(new AccessToken("test_wells"));

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsError);
            Assert.IsNotNull(result.Accounts);
            Assert.IsNotNull(result.Transactions);

            Assert.AreEqual(4, result.Accounts.Count);
            Assert.AreEqual(16, result.Transactions.Count);

            Account account = result.Accounts[0];
            Assert.AreEqual("QPO8Jo8vdDHMepg41PBwckXm4KdK1yUdmXOwK", account.Id);
            Assert.AreEqual("KdDjmojBERUKx3JkDd9RuxA5EvejA4SENO4AA", account.ItemId);
            Assert.AreEqual("eJXpMzpR65FP4RYno6rzuA7OZjd9n3Hna0RYa", account.UserId);
            Assert.AreEqual(1203.42, account.AvailableBalance);
            Assert.AreEqual(1274.93, account.CurrentBalance);
            Assert.AreEqual(new InstitutionType("fake_institution"), account.InstitutionType);
            Assert.AreEqual(AccountType.Depository, account.AccountType);
            Assert.AreEqual(AccountSubType.Savings, account.AccountSubtype);
            Assert.IsNotNull(account.Metadata);
            Assert.AreEqual("Plaid Savings", account.Metadata["name"]);
            Assert.AreEqual("9606", account.Metadata["number"]);

            Transaction transaction = result.Transactions[0];
            Assert.AreEqual("XARE85EJqKsjxLp6XR8ocg8VakrkXpTXmRdOo", transaction.AccountId);
            Assert.AreEqual(200, transaction.Amount);
            Assert.AreEqual(new DateTimeOffset(2014, 7, 21, 0, 0, 0, TimeSpan.Zero), transaction.Date);
            Assert.AreEqual("ATM Withdrawal", transaction.Name);

            Assert.IsNotNull(transaction.Location);
            Assert.AreEqual("San Francisco", transaction.Location.City);
            Assert.AreEqual("CA", transaction.Location.State);
            Assert.IsNull(transaction.Location.Latitude);
            Assert.IsNull(transaction.Location.Longitude);

            Assert.AreEqual(false, transaction.IsPending);
            Assert.IsNotNull(transaction.Categories);
            Assert.AreEqual(3, transaction.Categories.Count);
            Assert.AreEqual("21012002", transaction.CategoryId);
        }

        [TestMethod]
        public async Task GetTransactionsError()
        {
            IHttpClientWrapper httpClient = this.GetMockHttpClient("BadAccessToken.json", HttpStatusCode.Unauthorized, HttpMethod.Post, "connect/get");
            IPlaidClient testClient = this.GetPlaidClient(httpClient);
            TransactionResult result = await testClient.GetTransactionsAsync(new AccessToken("test_bad"));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsError);

            Assert.IsNotNull(result.Exception);
            Assert.AreEqual((int)HttpStatusCode.Unauthorized, result.Exception.HttpStatusCode);
            Assert.AreEqual(ErrorCode.BadAccessToken, result.Exception.ErrorCode);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.Exception.Message));
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.Exception.Resolution));

            Assert.IsNull(result.Accounts);
            Assert.IsNull(result.Transactions);
        }

        [TestMethod]
        public async Task GetPendingTransactions()
        {
            IHttpClientWrapper httpClient = this.GetMockHttpClient("PendingTransactions.json", HttpStatusCode.OK, HttpMethod.Post, "connect/get");
            IPlaidClient testClient = this.GetPlaidClient(httpClient);
            TransactionResult result = await testClient.GetTransactionsAsync(new AccessToken("test_wells"), true);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsError);
            Assert.IsNotNull(result.Accounts);
            Assert.IsNotNull(result.Transactions);

            Assert.AreEqual(4, result.Accounts.Count);
            Assert.AreEqual(16, result.Transactions.Count);
        }

        [TestMethod]
        public async Task GetTransactionsOneAccount()
        {
            IHttpClientWrapper httpClient = this.GetMockHttpClient("TransactionsOneAccount.json", HttpStatusCode.OK, HttpMethod.Post, "connect/get");
            IPlaidClient testClient = this.GetPlaidClient(httpClient);
            TransactionResult result = await testClient.GetTransactionsAsync(new AccessToken("test_wells"), accountId: "QPO8Jo8vdDHMepg41PBwckXm4KdK1yUdmXOwK");

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsError);
            Assert.IsNotNull(result.Accounts);
            Assert.IsNotNull(result.Transactions);

            Assert.AreEqual(4, result.Accounts.Count);
            Assert.AreEqual(2, result.Transactions.Count);
        }

        [TestMethod]
        public async Task GetTransactionsInDateRange()
        {
            IHttpClientWrapper httpClient = this.GetMockHttpClient("DateRangeTransactions.json", HttpStatusCode.OK, HttpMethod.Post, "connect/get");
            IPlaidClient testClient = this.GetPlaidClient(httpClient);
            DateTimeOffset startDate = new DateTimeOffset(2014, 4, 1, 0, 0, 0, TimeSpan.Zero);
            DateTimeOffset endDate = startDate.AddMonths(1);

            TransactionResult result = await testClient.GetTransactionsAsync(new AccessToken("test_wells"), null, null, startDate, endDate);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsError);
            Assert.IsNotNull(result.Accounts);
            Assert.IsNotNull(result.Transactions);

            Assert.AreEqual(4, result.Accounts.Count);
            Assert.AreEqual(5, result.Transactions.Count);

            foreach (Transaction t in result.Transactions)
            {
                Assert.IsTrue(t.Date > startDate);
                Assert.IsTrue(t.Date < endDate);
            }
        }

        [TestMethod]
        public async Task GetTransactionsGreaterDate()
        {
            IHttpClientWrapper httpClient = this.GetMockHttpClient("TransactionsGreaterThan.json", HttpStatusCode.OK, HttpMethod.Post, "connect/get");
            IPlaidClient testClient = this.GetPlaidClient(httpClient);
            DateTimeOffset startDate = new DateTimeOffset(2014, 7, 1, 0, 0, 0, TimeSpan.Zero);

            TransactionResult result = await testClient.GetTransactionsAsync(new AccessToken("test_wells"), null, null, startDate);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsError);
            Assert.IsNotNull(result.Accounts);
            Assert.IsNotNull(result.Transactions);

            Assert.AreEqual(4, result.Accounts.Count);
            Assert.AreEqual(2, result.Transactions.Count);

            foreach (Transaction t in result.Transactions)
            {
                Assert.IsTrue(t.Date > startDate);
            }
        }

        [TestMethod]
        public async Task GetTransactionsEndDate()
        {
            IHttpClientWrapper httpClient = this.GetMockHttpClient("TransactionsEndDate.json", HttpStatusCode.OK, HttpMethod.Post, "connect/get");
            IPlaidClient testClient = this.GetPlaidClient(httpClient);
            DateTimeOffset endDate = new DateTimeOffset(2014, 4, 1, 0, 0, 0, TimeSpan.Zero);

            TransactionResult result = await testClient.GetTransactionsAsync(new AccessToken("test_wells"), null, null, null, endDate);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsError);
            Assert.IsNotNull(result.Accounts);
            Assert.IsNotNull(result.Transactions);

            Assert.AreEqual(4, result.Accounts.Count);
            Assert.AreEqual(1, result.Transactions.Count);

            foreach (Transaction t in result.Transactions)
            {
                Assert.IsTrue(t.Date < endDate);
            }
        }

        [TestMethod]
        public async Task UpdateUserSuccess()
        {
            IHttpClientWrapper httpClient = this.GetMockHttpClient("AddUserSuccess.json", HttpStatusCode.OK, new HttpMethod("PATCH"));
            IPlaidClient testClient = this.GetPlaidClient(httpClient);
            AddUserResult result = await testClient.UpdateUserAsync(new AccessToken("test_wells"), BaseTestClass.TestUsername, BaseTestClass.TestPassword);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsError);
            Assert.IsFalse(result.IsMfaRequired);
            Assert.IsNotNull(result.AccessToken);
        }

        [TestMethod]
        public async Task UpdateUserWithPin()
        {
            IHttpClientWrapper httpClient = this.GetMockHttpClient("QuestionMfa.json", HttpStatusCode.Created, new HttpMethod("PATCH"));
            IPlaidClient testClient = this.GetPlaidClient(httpClient);
            AddUserResult result = await testClient.UpdateUserAsync(new AccessToken("test_usaa"), BaseTestClass.TestUsername, BaseTestClass.TestPassword, "1234");

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsError);
            Assert.IsTrue(result.IsMfaRequired);
            Assert.IsNotNull(result.AuthPrompt);
            Assert.IsNotNull(result.AuthPrompt.Questions);
        }

        [TestMethod]
        public async Task UpdateUserAuth()
        {
            IHttpClientWrapper httpClient = this.GetMockHttpClient("QuestionMfa.json", HttpStatusCode.Created, new HttpMethod("PATCH"));
            IPlaidClient testClient = this.GetPlaidClient(httpClient);
            AddUserResult result = await testClient.UpdateUserAsync(new AccessToken("test_usaa"), BaseTestClass.TestUsername, BaseTestClass.TestPassword, "1234");

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsError);
            Assert.IsTrue(result.IsMfaRequired);
            Assert.IsNotNull(result.AuthPrompt);
            Assert.IsNotNull(result.AuthPrompt.Questions);

            httpClient = this.GetMockHttpClient("AddUserSuccess.json", HttpStatusCode.OK, new HttpMethod("PATCH"), "connect/step");
            testClient = this.GetPlaidClient(httpClient);

            result = await testClient.AuthenticateUserAsync(new AccessToken("test_usaa"), true, ApiType.Connect, "tomato");
            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsError);
            Assert.IsFalse(result.IsMfaRequired);
        }

        [TestMethod]
        public async Task UpdateUserWebhook()
        {
            IHttpClientWrapper httpClient = this.GetMockHttpClient("AddUserSuccess.json", HttpStatusCode.OK, new HttpMethod("PATCH"));
            IPlaidClient testClient = this.GetPlaidClient(httpClient);
            AddUserResult result = await testClient.UpdateUserAsync(new AccessToken("test_wells"), BaseTestClass.TestUsername, BaseTestClass.TestPassword, webhookUri: new Uri("http://dummy.test.com"));

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsError);
            Assert.IsFalse(result.IsMfaRequired);
            Assert.IsNotNull(result.AccessToken);
        }

        [TestMethod]
        public async Task GetCategoriesSuccess()
        {
            IHttpClientWrapper httpClient = this.GetMockHttpClient("Categories.json", HttpStatusCode.OK, HttpMethod.Get, "categories");
            IPlaidClient testClient = this.GetPlaidClient();
            var result = await testClient.GetCategoriesAsync();

            Assert.IsNotNull(result.Value);
            Assert.IsFalse(result.IsError);
            Assert.IsTrue(result.Value.Count > 0);
            Assert.IsNotNull(result.Value[0]);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.Value[0].Id));
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.Value[0].Type));
            Assert.IsNotNull(result.Value[0].Hierarchy);
            Assert.IsTrue(result.Value[0].Hierarchy.Count > 0);
        }

        [TestMethod]
        public async Task GetCategorySuccess()
        {
            string categoryId = "10001000";
            IHttpClientWrapper httpClient = this.GetMockHttpClient("Category.json", HttpStatusCode.OK, HttpMethod.Get, "categories/" + categoryId);
            IPlaidClient testClient = this.GetPlaidClient(httpClient);
            var result = await testClient.GetCategoryAsync(categoryId);

            Assert.IsNotNull(result.Value);
            Assert.IsFalse(result.IsError);

            Assert.AreEqual(categoryId, result.Value.Id);
            Assert.AreEqual("special", result.Value.Type);
            Assert.IsNotNull(result.Value.Hierarchy);
            Assert.IsTrue(result.Value.Hierarchy.Count > 0);
            Assert.AreEqual("Bank Fees", result.Value.Hierarchy[0]);
            Assert.AreEqual("Overdraft", result.Value.Hierarchy[1]);
        }

        [TestMethod]
        public async Task GetCategoryNotFound()
        {
            IHttpClientWrapper httpClient = this.GetMockHttpClient("CategoryNotFound.json", HttpStatusCode.NotFound, HttpMethod.Get, "categories/notexists");
            IPlaidClient testClient = this.GetPlaidClient(httpClient);
            var result = await testClient.GetCategoryAsync("notexists");

            Assert.IsNull(result.Value);
            Assert.IsTrue(result.IsError);

            Assert.IsNotNull(result.Exception);
            Assert.AreEqual(404, result.Exception.HttpStatusCode);
            Assert.AreEqual(ErrorCode.CategoryNotFound, result.Exception.ErrorCode);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.Exception.Message));
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.Exception.Resolution));
        }

        [TestMethod]
        public async Task GetInstitutionsSuccess()
        {
            IHttpClientWrapper httpClient = this.GetMockHttpClient("Institutions.json", HttpStatusCode.OK, HttpMethod.Get, "institutions");
            IPlaidClient testClient = this.GetPlaidClient(httpClient);
            var result = await testClient.GetInstitutionsAsync();

            Assert.IsNotNull(result.Value);
            Assert.IsFalse(result.IsError);
            Assert.IsTrue(result.Value.Count > 0);
            Assert.IsNotNull(result.Value[0]);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.Value[0].Id));
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.Value[0].Name));
            Assert.IsNotNull(result.Value[0].Type);
            Assert.IsTrue(result.Value[0].MfaDescriptions.Count > 0);
        }

        [TestMethod]
        public async Task GetInstitutionSuccess()
        {
            string instId = "531ea6327de8211c80000440";
            IHttpClientWrapper httpClient = this.GetMockHttpClient("Institution.json", HttpStatusCode.OK, HttpMethod.Get, "institutions/" + instId);
            IPlaidClient testClient = this.GetPlaidClient(httpClient);
            var result = await testClient.GetInstitutionAsync(instId);

            Assert.IsNotNull(result.Value);
            Assert.IsFalse(result.IsError);

            Assert.AreEqual(instId, result.Value.Id);
            Assert.AreEqual(InstitutionType.UsBank, result.Value.Type);
            Assert.IsNotNull(result.Value.MfaDescriptions);
            Assert.IsTrue(result.Value.MfaDescriptions.Count > 0);
            Assert.IsTrue(result.Value.HasMfa);
            Assert.AreEqual("Personal ID", result.Value.UsernameHint);
            Assert.AreEqual("Password", result.Value.PasswordHint);
        }

        [TestMethod]
        public async Task GetInstitutionNotFound()
        {
            IHttpClientWrapper httpClient = this.GetMockHttpClient("InstitutionNotFound.json", HttpStatusCode.NotFound, HttpMethod.Get, "institutions/notexists");
            IPlaidClient testClient = this.GetPlaidClient(httpClient);
            var result = await testClient.GetInstitutionAsync("notexists");

            Assert.IsNull(result.Value);
            Assert.IsTrue(result.IsError);

            Assert.IsNotNull(result.Exception);
            Assert.AreEqual(404, result.Exception.HttpStatusCode);
            Assert.AreEqual(ErrorCode.InstitutionNotFound, result.Exception.ErrorCode);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.Exception.Message));
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.Exception.Resolution));
        }

        [TestMethod]
        public async Task ExchangeTokenSuccess()
        {
            IHttpClientWrapper httpClient = this.GetMockHttpClient("TokenExchangeSuccess.json", HttpStatusCode.OK, HttpMethod.Post, "exchange_token");
            IPlaidClient testClient = this.GetPlaidClient(httpClient);
            var result = await testClient.ExchangeBankTokenAsync("test_public_token", "test_account_id");

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsError);

            Assert.IsNotNull(result.AccessToken);
            Assert.AreEqual("foobar_plaid_access_token", result.AccessToken.Value);
            Assert.AreEqual("foobar_stripe_bank_account_token", result.BankAccountToken);
        }

        [TestMethod]
        public async Task ExchangeTokenFailed()
        {
            IHttpClientWrapper httpClient = this.GetMockHttpClient("InvalidCredentials.json", HttpStatusCode.Unauthorized, HttpMethod.Post, "exchange_token");
            IPlaidClient testClient = this.GetPlaidClient(httpClient);
            TokenExchangeResult result = await testClient.ExchangeBankTokenAsync("test_public_token", "test_account_id");

            Assert.IsNull(result.AccessToken);
            Assert.IsNull(result.BankAccountToken);
            Assert.IsTrue(result.IsError);

            Assert.IsNotNull(result.Exception);
            Assert.AreEqual(401, result.Exception.HttpStatusCode);
            Assert.AreEqual(ErrorCode.InvalidCredentials, result.Exception.ErrorCode);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.Exception.Message));
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.Exception.Resolution));
        }

        [TestMethod]
        public async Task GetAuthAccountSuccess()
        {
            IHttpClientWrapper httpClient = this.GetMockHttpClient("AuthUserUsBank.json", HttpStatusCode.OK, HttpMethod.Get, "auth/get");
            IPlaidClient testClient = this.GetPlaidClient(httpClient);

            PlaidResult<IList<Account>> result = await testClient.GetAuthAccountDataAsync(new AccessToken("test_wells"));

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsError);
            Assert.IsNotNull(result.Value);

            Assert.AreEqual(4, result.Value.Count);

            Account account = result.Value[0];
            Assert.AreEqual("QPO8Jo8vdDHMepg41PBwckXm4KdK1yUdmXOwK", account.Id);
            Assert.AreEqual("KdDjmojBERUKx3JkDd9RuxA5EvejA4SENO4AA", account.ItemId);
            Assert.AreEqual("eJXpMzpR65FP4RYno6rzuA7OZjd9n3Hna0RYa", account.UserId);
            Assert.AreEqual(1203.42, account.AvailableBalance);
            Assert.AreEqual(1274.93, account.CurrentBalance);
            Assert.AreEqual(new InstitutionType("fake_institution"), account.InstitutionType);
            Assert.AreEqual(AccountType.Depository, account.AccountType);
            Assert.AreEqual(AccountSubType.Savings, account.AccountSubtype);
            Assert.IsNotNull(account.Metadata);
            Assert.AreEqual("Plaid Savings", account.Metadata["name"]);
            Assert.AreEqual("9606", account.Metadata["number"]);
        }

        [TestMethod]
        public async Task GetAuthAccountError()
        {
            IHttpClientWrapper httpClient = this.GetMockHttpClient("BadAccessToken.json", HttpStatusCode.Unauthorized, HttpMethod.Get, "auth/get");
            IPlaidClient testClient = this.GetPlaidClient(httpClient);
            PlaidResult<IList<Account>> result = await testClient.GetAuthAccountDataAsync(new AccessToken("test_bad"));

            Assert.IsNotNull(result);
            Assert.IsNull(result.Value);
            Assert.IsTrue(result.IsError);

            Assert.IsNotNull(result.Exception);
            Assert.AreEqual((int)HttpStatusCode.Unauthorized, result.Exception.HttpStatusCode);
            Assert.AreEqual(ErrorCode.BadAccessToken, result.Exception.ErrorCode);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.Exception.Message));
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.Exception.Resolution));
        }
    }
}
