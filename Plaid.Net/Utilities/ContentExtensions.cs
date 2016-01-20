namespace Plaid.Net.Utilities
{
    using System;
    using System.Net.Http;
    using System.Text;
    using Newtonsoft.Json;

    /// <summary>
    /// Extensions for <see cref="HttpContent"/>.
    /// </summary>
    internal static class ContentExtensions
    {
        /// <summary>
        /// Converts an object to JSON <see cref="StringContent"/>.
        /// </summary>
        /// <param name="obj">The object to convert.</param>
        /// <returns>The <see cref="StringContent"/> object specifying the JSON formatted object.</returns>
        public static StringContent ToJsonContent(this object obj)
        {
            string json = JsonConvert.SerializeObject(obj);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        /// <summary>
        /// Converts an object to JSON <see cref="StringContent"/>.
        /// </summary>
        /// <param name="obj">The object to convert.</param>
        /// <param name="settings">Custon json serialization settings.</param>
        /// <returns>The <see cref="StringContent"/> object specifying the JSON formatted object.</returns>
        public static StringContent ToJsonContent(this object obj, JsonSerializerSettings settings)
        {
            string json = JsonConvert.SerializeObject(obj, settings);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}