namespace Plaid.Net.Utilities
{
    using System.Net;

    /// <summary>
    /// Wrapper for use with HTTP clients to return both what happened and the result object
    /// </summary>
    /// <typeparam name="TItem">response item associated with the request</typeparam>
    internal class HttpResult<TItem>
    {
        /// <summary>
        /// Create a result object with just a status code
        /// </summary>
        /// <param name="code">status code</param>
        public HttpResult(HttpStatusCode code)
            : this(code, default(TItem))
        {
        }

        /// <summary>
        /// Create a result object with status code and item
        /// </summary>
        /// <param name="code">HTTP status code</param>
        /// <param name="item">Response Item</param>
        public HttpResult(HttpStatusCode code, TItem item)
        {
            this.StatusCode = code;
            this.ResponseItem = item;
        }

        /// <summary>
        /// HTTP status code returned by the request
        /// </summary>
        public HttpStatusCode StatusCode { get; private set; }

        /// <summary>
        /// Deserialized item returned by the request (if any)
        /// </summary>
        public TItem ResponseItem { get; private set; }
    }
}
