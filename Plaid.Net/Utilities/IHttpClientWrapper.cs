namespace Plaid.Net.Utilities
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for <see cref="HttpClient"/> so that it can be mocked.
    /// </summary>
    internal interface IHttpClientWrapper : IDisposable
    {
        /// <summary>
        /// Gets or sets the base address of Uniform Resource Identifier (URI) of the
        /// Internet resource used when sending requests.
        /// </summary>
        Uri BaseAddress { get; set; }

        /// <summary>
        /// Gets the headers which should be sent with each request.
        /// </summary>
        HttpRequestHeaders DefaultRequestHeaders { get; }

        /// <summary>
        /// Gets or sets the number of milliseconds to wait before the request times out.
        /// </summary>
        TimeSpan Timeout { get; set; }

        /// <summary>
        /// Wraps a PATCH request.
        /// </summary>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<HttpResponseMessage> PatchAsync(string requestUri);

        /// <summary>
        /// Wraps a PATCH request.
        /// </summary>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="value">Value for request body.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<HttpResponseMessage> PatchAsJsonAsync<T>(string requestUri, T value);

        /// <summary>
        /// Wraps <see cref="HttpClient.DeleteAsync"/>
        /// </summary>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<HttpResponseMessage> DeleteAsync(string requestUri);

        /// <summary>
        /// Wraps <see cref="HttpClient.DeleteAsync"/>
        /// </summary>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="value">Value for request body.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<HttpResponseMessage> DeleteAsJsonAsync<T>(string requestUri, T value);

        /// <summary>
        /// Wraps <see cref="HttpClient.DeleteAsync"/>
        /// </summary>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<HttpResponseMessage> DeleteAsync(Uri requestUri);

        /// <summary>
        /// Wraps <see cref="HttpClient.GetAsync"/>
        /// </summary>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<HttpResponseMessage> GetAsync(string requestUri);

        /// <summary>
        /// Wraps <see cref="HttpClient.GetAsync"/>
        /// </summary>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<HttpResponseMessage> GetAsync(Uri requestUri);

        /// <summary>
        /// Wraps <see cref="HttpClient.PostAsync"/>
        /// </summary>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="content">The HTTP request content sent to the server.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content);

        /// <summary>
        /// Wraps <see cref="HttpClient.PostAsync"/>
        /// </summary>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="content">The HTTP request content sent to the server.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<HttpResponseMessage> PostAsync(Uri requestUri, HttpContent content);

        /// <summary>
        /// Wraps <see cref="HttpClient.PostAsJsonAsync"/>
        /// </summary>
        /// <typeparam name="T">The type of value.</typeparam>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="value">The value that will be placed in the request's entity body.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<HttpResponseMessage> PostAsJsonAsync<T>(string requestUri, T value);

        /// <summary>
        /// Wraps <see cref="HttpClient.PostAsJsonAsync"/>
        /// </summary>
        /// <typeparam name="T">The type of value.</typeparam>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="value">The value that will be placed in the request's entity body.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<HttpResponseMessage> PostAsJsonAsync<T>(Uri requestUri, T value);

        /// <summary>
        /// Wraps <see cref="HttpClient.PutAsync"/>
        /// </summary>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="content">The HTTP request content sent to the server.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<HttpResponseMessage> PutAsync(string requestUri, HttpContent content);

        /// <summary>
        /// Wraps <see cref="HttpClient.PutAsync"/>
        /// </summary>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="content">The HTTP request content sent to the server.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<HttpResponseMessage> PutAsync(Uri requestUri, HttpContent content);

        /// <summary>
        /// Wraps <see cref="HttpClient.PutAsJsonAsync"/>
        /// </summary>
        /// <typeparam name="T">The type of value.</typeparam>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="value">The value that will be placed in the request's entity body.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<HttpResponseMessage> PutAsJsonAsync<T>(string requestUri, T value);

        /// <summary>
        /// Wraps <see cref="HttpClient.PutAsJsonAsync"/>
        /// </summary>
        /// <typeparam name="T">The type of value.</typeparam>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="value">The value that will be placed in the request's entity body.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<HttpResponseMessage> PutAsJsonAsync<T>(Uri requestUri, T value);

        /// <summary>
        /// Wraps <see cref="HttpClient.SendAsync"/>
        /// </summary>
        /// <param name="request">The HTTP request message to send.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);
    }
}