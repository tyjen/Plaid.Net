namespace Plaid.Net.Utilities
{
    using System;
    using System.ComponentModel;
    using System.Globalization;

    /// <summary>
    /// Identifier type to allow for strong typed identifiers. Class can provide 
    /// a reasonable alternative to enums in many scenarios. 
    /// </summary>
    /// <typeparam name="T">Type of identifier</typeparam>
    [Serializable]
    [ImmutableObject(true)]
    public abstract class Identifier<T> : IEquatable<Identifier<T>>
    {
        /// <summary>
        /// Holder for the wrapped Value
        /// </summary>
        private readonly T value;

        /// <summary>
        /// Construct the identifier with the given Value
        /// </summary>
        /// <param name="value">Value to wrap</param>
        protected Identifier(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            this.value = value;
        }

        /// <summary>
        /// The wrapped Value
        /// </summary>
        public T Value => this.value;

        /// <summary>
        /// The Type of the Value that is wrapped
        /// </summary>
        public Type ValueType => this.Value.GetType();

        /// <summary>
        /// Test if a and b are equal
        /// </summary>
        /// <param name="a">first object to test</param>
        /// <param name="b">second object to test</param>
        /// <returns>if a and b are equal</returns>
        public static bool operator ==(Identifier<T> a, Identifier<T> b)
        {
            // If both are null, or both are same instance, return true.
            if (object.ReferenceEquals(a, b))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }

            // Return true if the fields match:
            return a.Equals(b);
        }

        /// <summary>
        /// Returns the negation of the == operator
        /// </summary>
        /// <param name="a">first object</param>
        /// <param name="b">second object</param>
        /// <returns>negation of == operator</returns>
        public static bool operator !=(Identifier<T> a, Identifier<T> b)
        {
            return !(a == b);
        }

        /// <inheritdoc/>
        public bool Equals(Identifier<T> other)
        {
            if (other == null || this.GetType() != other.GetType())
            {
                return false;
            }
            return this.value.Equals(other.value);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "(Value:'{0}')", this.value);
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj == null || this.GetType() != obj.GetType())
            {
                return false;
            }

            Identifier<T> other = (Identifier<T>)obj;
            return this.Equals(other);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return this.value.GetHashCode();
        }
    }
}
