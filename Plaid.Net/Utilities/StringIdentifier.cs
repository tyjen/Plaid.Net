namespace Plaid.Net.Utilities
{
    using System;

    /// <summary>
    /// An Identifier type that is a string
    /// </summary>
    public abstract class StringIdentifier : Identifier<string>
    {
        /// <summary>
        /// Creates a String Identifier, validating that the value is not empty
        /// or white space
        /// </summary>
        /// <param name="value">wrapped value. Must not be null, empty or whitespace</param>
        protected StringIdentifier(string value) : base(value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(nameof(value));
            }
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return this.Value;
        }
    }
}

