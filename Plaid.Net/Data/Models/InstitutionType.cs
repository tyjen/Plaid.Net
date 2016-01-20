namespace Plaid.Net.Models
{
    using System;
    using Plaid.Net.Utilities;

    /// <summary>
    /// Identifier for banking institutions.
    /// </summary>
    public class InstitutionType : StringIdentifier
    {
        /// <summary>
        /// American Express. No MFA.
        /// </summary>
        public static InstitutionType AmericanExpress = new InstitutionType("amex");

        /// <summary>
        /// Bank of America. Question-based MFA (3 questions) or code-based MFA (SafePass).
        /// </summary>
        public static InstitutionType BankOfAmerica = new InstitutionType("bofa");

        /// <summary>
        /// Capital One 360. Question-based MFA (1 to 3 questions).
        /// </summary>
        public static InstitutionType CapitalOne = new InstitutionType("capone360");

        /// <summary>
        /// Charles Schwab. No MFA.
        /// </summary>
        public static InstitutionType CharlesSchwab = new InstitutionType("schwab");

        /// <summary>
        /// Chase. Code-based MFA.
        /// </summary>
        public static InstitutionType Chase = new InstitutionType("chase");

        /// <summary>
        /// Citi. Question-based MFA and selection-based MFA (Auth only)
        /// </summary>
        public static InstitutionType Citi = new InstitutionType("citi");

        /// <summary>
        /// Fidelity. No MFA.
        /// </summary>
        public static InstitutionType Fidelity = new InstitutionType("fidelity");

        /// <summary>
        /// Navy Federal Credit Union. No MFA.
        /// </summary>
        public static InstitutionType NavyFederalCreditUnion = new InstitutionType("nfcu");

        /// <summary>
        /// PNC. Question-based MFA (3 questions).
        /// </summary>
        public static InstitutionType Pnc = new InstitutionType("pnc");

        /// <summary>
        /// Silicon Valley Bank. No MFA.
        /// </summary>
        public static InstitutionType SiliconValleyBank = new InstitutionType("svb");

        /// <summary>
        /// Sun Trust. No MFA.
        /// </summary>
        public static InstitutionType SunTrust = new InstitutionType("suntrust");

        /// <summary>
        /// TD Bank. Question-based MFA (1 to 3 questions).
        /// </summary>
        public static InstitutionType TdBank = new InstitutionType("td");

        /// <summary>
        /// USAA. Question-based MFA (3 questions). Requires PIN.
        /// </summary>
        public static InstitutionType Usaa = new InstitutionType("usaa");

        /// <summary>
        /// US bank. Question-based MFA (1 to 3 questions).
        /// </summary>
        public static InstitutionType UsBank = new InstitutionType("us");

        /// <summary>
        /// Wells Fargo. No MFA.
        /// </summary>
        public static InstitutionType WellsFargo = new InstitutionType("wells");

        /// <summary>
        /// Initializes a new instance of the <see cref="InstitutionType"/> class.
        /// </summary>
        /// <param name="institution">The string institution type.</param>
        public InstitutionType(string institution)
            : base(institution)
        {
            // No-op
        }
    }
}