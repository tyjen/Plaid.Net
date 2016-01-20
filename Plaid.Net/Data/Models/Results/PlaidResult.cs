namespace Plaid.Net.Data.Models
{
    using System;

    /// <summary>
    /// Generic result from Plaid which could possibly be an error.
    /// </summary>
    /// <typeparam name="T">The type of response value.</typeparam>
    public class PlaidResult<T> : PlaidResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlaidResult{T}"/> class.
        /// </summary>
        public PlaidResult()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlaidResult{T}"/> class.
        /// </summary>
        /// <param name="value">The return value.</param>
        public PlaidResult(T value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Gets or sets the result value.
        /// </summary>
        public T Value { get; set; }
    }

    /// <summary>
    /// Generic result from Plaid which could possibly be an error.
    /// </summary>
    public abstract class PlaidResult
    {
        /// <summary>
        /// Gets exception information if a request was not successful.
        /// </summary>
        public PlaidException Exception { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether the operation resulted in an error.
        /// </summary>
        public bool IsError => this.Exception != null;
    }
}