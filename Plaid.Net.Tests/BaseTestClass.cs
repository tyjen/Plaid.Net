namespace Plaid.Net.Tests
{
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Reflection;
    using System.Text;
    using Moq;
    using Plaid.Net.Data.Models;
    using Tyjen.Net.Http;

    /// <summary>
    /// The base test class.
    /// </summary>
    public class BaseTestClass
    {
        /// <summary>
        /// The test client id.
        /// </summary>
        public const string TestClientId = "test_id";

        /// <summary>
        /// The test password.
        /// </summary>
        public const string TestPassword = "plaid_good";

        /// <summary>
        /// The test secret.
        /// </summary>
        public const string TestSecret = "test_secret";

        /// <summary>
        /// The test username.
        /// </summary>
        public const string TestUsername = "plaid_test";

        /// <summary>
        /// The test institution.
        /// </summary>
        public static readonly InstitutionType TestInstitution = InstitutionType.WellsFargo;

        /// <summary>
        /// The test server.
        /// </summary>
        public static readonly Uri TestServer = new Uri("https://tartan.plaid.com");

        /// <summary>
        /// Gets a mock response from the given file.
        /// </summary>
        /// <param name="responseFileName">Name of the file containing the response json.</param>
        /// <param name="statusCode">The status code of the response.</param>
        /// <returns>Response with the given content and status code.</returns>
        public HttpResponseMessage GetMockResponse(string responseFileName, HttpStatusCode statusCode)
        {
            string myDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string responseDir = Path.Combine(myDir, "Responses");
            FileInfo responseFile = new FileInfo(Path.Combine(responseDir, responseFileName));

            if (!responseFile.Exists) throw new FileNotFoundException("Couldn't find requested response file " + responseFileName, responseFile.FullName);

            string json = File.ReadAllText(responseFile.FullName);

            HttpResponseMessage response = new HttpResponseMessage(statusCode);
            response.Content = new StringContent(json, Encoding.UTF8, "application/json");

            return response;
        }

        public IHttpClientWrapper GetMockHttpClient(string responseFileName, HttpStatusCode statusCode, HttpMethod method, string path = "connect")
        {
            Mock<IHttpClientWrapper> mockHttpClient = new Mock<IHttpClientWrapper>();

            switch (method.Method)
            {
                case "POST":
                    mockHttpClient.Setup(h => h.PostAsJsonAsync(path, It.IsNotNull<object>()))
                                  .ReturnsAsync(this.GetMockResponse(responseFileName, statusCode));
                    break;
                case "GET":
                    mockHttpClient.Setup(h => h.GetAsJsonAsync(path, It.IsNotNull<object>()))
                                  .ReturnsAsync(this.GetMockResponse(responseFileName, statusCode));

                    mockHttpClient.Setup(h => h.GetAsync(path))
                                  .ReturnsAsync(this.GetMockResponse(responseFileName, statusCode));
                    break;
                case "PATCH":
                    mockHttpClient.Setup(h => h.PatchAsJsonAsync(path, It.IsNotNull<object>()))
                                  .ReturnsAsync(this.GetMockResponse(responseFileName, statusCode));
                    break;

            }

            return mockHttpClient.Object;
        }

        /// <summary>
        /// Gets the plaid client with test credentials and sandbox server url.
        /// </summary>
        public IPlaidClient GetPlaidClient()
        {
            return new HttpPlaidClient(BaseTestClass.TestServer, BaseTestClass.TestClientId, BaseTestClass.TestSecret);
        }

        /// <summary>
        /// Gets the plaid client with test credentials and sandbox server url.
        /// </summary>
        public IPlaidClient GetPlaidClient(IHttpClientWrapper httpClient)
        {
            return new HttpPlaidClient(BaseTestClass.TestServer, BaseTestClass.TestClientId, BaseTestClass.TestSecret, httpClient);
        }
    }
}