namespace Plaid.Net.Utilities
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    /// <summary>
    /// Wraps <see cref="HttpClient"/> and adds custom validation.
    /// </summary>
    internal class HttpClientWrapper : IHttpClientWrapper
    {
        /// <summary>
        /// The underlying HttpClient instance
        /// </summary>
        private readonly HttpClient httpClient;

        /// <summary>
        /// The inner handler which is responsible for processing the HTTP response messages.
        /// </summary>
        private readonly HttpMessageHandler innerHandler;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpClientWrapper"/> class.
        /// </summary>
        public HttpClientWrapper() : this(new HttpClient(), null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpClientWrapper"/> class.
        /// </summary>
        /// <param name="httpClient">The underlying HttpClient instance</param>
        /// <param name="innerHandler">The inner handler which is responsible for processing the HTTP response messages.</param>
        public HttpClientWrapper(HttpClient httpClient, HttpMessageHandler innerHandler = null)
        {
            if (httpClient == null)
            {
                throw new ArgumentNullException(nameof(httpClient));
            }

            this.httpClient = httpClient;
            this.innerHandler = innerHandler;
        }

        /// <summary>
        /// Gets or sets the base address of Uniform Resource Identifier (URI) of the
        /// Internet resource used when sending requests.
        /// </summary>
        public Uri BaseAddress
        {
            get
            {
                return this.httpClient.BaseAddress;
            }

            set
            {
                this.httpClient.BaseAddress = value;
            }
        }

        /// <summary>
        /// Gets the headers which should be sent with each request.
        /// </summary>
        public HttpRequestHeaders DefaultRequestHeaders
        {
            get
            {
                return this.httpClient.DefaultRequestHeaders;
            }
        }

        /// <summary>
        /// Gets or sets the number of milliseconds to wait before the request times out.
        /// </summary>
        public TimeSpan Timeout
        {
            get
            {
                return this.httpClient.Timeout;
            }

            set
            {
                this.httpClient.Timeout = value;
            }
        }

        /// <summary>
        /// Wraps a PATCH request.
        /// </summary>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<HttpResponseMessage> PatchAsync(string requestUri)
        {
            HttpRequestMessage outgoingMessage = new HttpRequestMessage
            {
                RequestUri = this.GetUriFromString(requestUri),
                Method = new HttpMethod("PATCH")
            };

            return this.SendAsync(outgoingMessage);
        }

        /// <summary>
        /// Wraps a PATCH request.
        /// </summary>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="value">Value for request body.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<HttpResponseMessage> PatchAsJsonAsync<T>(string requestUri, T value)
        {
            HttpRequestMessage outgoingMessage = new HttpRequestMessage
            {
                RequestUri = this.GetUriFromString(requestUri),
                Method = new HttpMethod("PATCH"),
                Content = value?.ToJsonContent()
            };

            return this.SendAsync(outgoingMessage);
        }

        /// <summary>
        /// Wraps <see cref="HttpClient.DeleteAsync"/>
        /// </summary>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<HttpResponseMessage> DeleteAsync(string requestUri)
        {
            return this.DeleteAsync(this.GetUriFromString(requestUri));
        }

        /// <summary>
        /// Wraps <see cref="HttpClient.DeleteAsync"/>
        /// </summary>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="value">Value for request body.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<HttpResponseMessage> DeleteAsJsonAsync<T>(string requestUri, T value)
        {
            HttpRequestMessage outgoingMessage = new HttpRequestMessage
            {
                RequestUri = this.GetUriFromString(requestUri),
                Method = HttpMethod.Delete,
                Content = value?.ToJsonContent()
            };

            return this.SendAsync(outgoingMessage);
        }

        /// <summary>
        /// Wraps <see cref="HttpClient.DeleteAsync"/>
        /// </summary>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<HttpResponseMessage> DeleteAsync(Uri requestUri)
        {
            HttpRequestMessage outgoingMessage = new HttpRequestMessage
            {
                RequestUri = requestUri,
                Method = HttpMethod.Delete
            };

            return this.SendAsync(outgoingMessage);
        }

        /// <summary>
        /// Wraps <see cref="HttpClient.GetAsync"/>
        /// </summary>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<HttpResponseMessage> GetAsync(string requestUri)
        {
            return this.GetAsync(this.GetUriFromString(requestUri));
        }

        /// <summary>
        /// Wraps <see cref="HttpClient.GetAsync"/>
        /// </summary>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<HttpResponseMessage> GetAsJsonAsync<T>(string requestUri, T value)
        {
            HttpRequestMessage outgoingMessage = new HttpRequestMessage
            {
                RequestUri = this.GetUriFromString(requestUri),
                Method = HttpMethod.Get,
                Content = value?.ToJsonContent()
            };

            return this.SendAsync(outgoingMessage);
        }

        /// <summary>
        /// Wraps <see cref="HttpClient.GetAsync"/>
        /// </summary>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<HttpResponseMessage> GetAsync(Uri requestUri)
        {
            HttpRequestMessage outgoingMessage = new HttpRequestMessage
            {
                RequestUri = requestUri,
                Method = HttpMethod.Get
            };

            return this.SendAsync(outgoingMessage);
        }

        /// <summary>
        /// Wraps <see cref="HttpClient.PostAsync"/>
        /// </summary>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="content">The HTTP request content sent to the server.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content)
        {
            return this.PostAsync(this.GetUriFromString(requestUri), content);
        }

        /// <summary>
        /// Wraps <see cref="HttpClient.PostAsync"/>
        /// </summary>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="content">The HTTP request content sent to the server.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<HttpResponseMessage> PostAsync(Uri requestUri, HttpContent content)
        {
            HttpRequestMessage outgoingMessage = new HttpRequestMessage
            {
                RequestUri = requestUri,
                Method = HttpMethod.Post,
                Content = content
            };

            return this.SendAsync(outgoingMessage);
        }

        /// <summary>
        /// Wraps <see cref="HttpClient.PostAsJsonAsync"/>
        /// </summary>
        /// <typeparam name="T">The type of value.</typeparam>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="value">The value that will be placed in the request's entity body.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<HttpResponseMessage> PostAsJsonAsync<T>(string requestUri, T value)
        {
            return this.PostAsJsonAsync(this.GetUriFromString(requestUri), value);
        }

        /// <summary>
        /// Wraps <see cref="HttpClient.PostAsJsonAsync"/>
        /// </summary>
        /// <typeparam name="T">The type of value.</typeparam>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="value">The value that will be placed in the request's entity body.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<HttpResponseMessage> PostAsJsonAsync<T>(Uri requestUri, T value)
        {
            HttpRequestMessage outgoingMessage = new HttpRequestMessage
            {
                RequestUri = requestUri,
                Method = HttpMethod.Post,
                Content = value?.ToJsonContent()
            };

            return this.SendAsync(outgoingMessage);
        }

        /// <summary>
        /// Wraps <see cref="HttpClient.PutAsync"/>
        /// </summary>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="content">The HTTP request content sent to the server.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<HttpResponseMessage> PutAsync(string requestUri, HttpContent content)
        {
            return this.PutAsync(this.GetUriFromString(requestUri), content);
        }

        /// <summary>
        /// Wraps <see cref="HttpClient.PutAsync"/>
        /// </summary>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="content">The HTTP request content sent to the server.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<HttpResponseMessage> PutAsync(Uri requestUri, HttpContent content)
        {
            HttpRequestMessage outgoingMessage = new HttpRequestMessage
            {
                RequestUri = requestUri,
                Method = HttpMethod.Put,
                Content = content
            };

            return this.SendAsync(outgoingMessage);
        }

        /// <summary>
        /// Wraps <see cref="HttpClient.PutAsJsonAsync"/>
        /// </summary>
        /// <typeparam name="T">The type of value.</typeparam>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="value">The value that will be placed in the request's entity body.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<HttpResponseMessage> PutAsJsonAsync<T>(string requestUri, T value)
        {
            return this.PutAsJsonAsync(this.GetUriFromString(requestUri), value);
        }

        /// <summary>
        /// Wraps <see cref="HttpClient.PutAsJsonAsync"/>
        /// </summary>
        /// <typeparam name="T">The type of value.</typeparam>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="value">The value that will be placed in the request's entity body.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<HttpResponseMessage> PutAsJsonAsync<T>(Uri requestUri, T value)
        {
            HttpRequestMessage outgoingMessage = new HttpRequestMessage
            {
                RequestUri = requestUri,
                Method = HttpMethod.Put,
                Content = value?.ToJsonContent()
            };

            return this.SendAsync(outgoingMessage);
        }

        /// <summary>
        /// Wraps <see cref="HttpClient.SendAsync"/>
        /// </summary>
        /// <param name="request">The HTTP request message to send.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            // SetCorrelationIdHeader(request);
            return this.httpClient.SendAsync(request);
        }

        /// <summary>
        /// Disposes the current instance
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes the current instance
        /// </summary>
        /// <param name="disposing">Disposing</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.httpClient.Dispose();
                if (this.innerHandler != null)
                {
                    this.innerHandler.Dispose();
                }
            }
        }

        /// <summary>
        /// Constructs a Uri from the provided string
        /// </summary>
        /// <param name="requestUri">The Uri string</param>
        /// <returns>The constructed Uri if successful, null otherwise</returns>
        private Uri GetUriFromString(string requestUri)
        {
            Uri uri;
            return Uri.TryCreate(this.BaseAddress, requestUri, out uri) ? uri : null;
        }
    }
}
