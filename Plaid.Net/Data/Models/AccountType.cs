namespace Plaid.Net.Data.Models
{
    using System;
    using Tyjen.Net.Core.Identifiers;

    /// <summary>
    /// Specifies which type an <see cref="Account"/> is.
    /// </summary>
    public class AccountType : StringIdentifier
    {
        /// <summary>
        /// Can be  <see cref="AccountSubType.Brokerage"/>, <see cref="AccountSubType.CashManagement"/>, <see cref="AccountSubType.Ira"/>.
        /// </summary>
        public static AccountType Brokerage = new AccountType("brokerage");

        /// <summary>
        /// Can be  <see cref="AccountSubType.Credit"/>, <see cref="AccountSubType.CreditCard"/>, <see cref="AccountSubType.LineOfCredit"/>.
        /// </summary>
        public static AccountType Credit = new AccountType("credit");

        /// <summary>
        /// Can be <see cref="AccountSubType.Checking"/>, <see cref="AccountSubType.Savings"/>, <see cref="AccountSubType.Prepaid"/>.
        /// </summary>
        public static AccountType Depository = new AccountType("depository");

        /// <summary>
        /// Can be  <see cref="AccountSubType.Auto"/>, <see cref="AccountSubType.Home"/>, <see cref="AccountSubType.Installment"/>, <see cref="AccountSubType.Mortgage"/>.
        /// </summary>
        public static AccountType Loan = new AccountType("loan");

        /// <summary>
        /// Can be  <see cref="AccountSubType.Home"/>.
        /// </summary>
        public static AccountType Mortgage = new AccountType("mortgage");

        /// <summary>
        /// Can be  <see cref="AccountSubType.Cd"/>, <see cref="AccountSubType.CertificateOfDeposit"/>, <see cref="AccountSubType.Ira"/>, <see cref="AccountSubType.MutualFund"/>.
        /// </summary>
        public static AccountType Other = new AccountType("other");

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountType"/> class.
        /// </summary>
        /// <param name="value">The string account type value.</param>
        public AccountType(string value)
            : base(value)
        {
        }
    }

    /// <summary>
    /// More specific <see cref="AccountType"/>.
    /// </summary>
    public class AccountSubType : StringIdentifier
    {
        /// <summary>
        /// Automobile load.
        /// </summary>
        public static AccountSubType Auto = new AccountSubType("auto");

        /// <summary>
        /// Brokerage account.
        /// </summary>
        public static AccountSubType Brokerage = new AccountSubType("brokerage");

        /// <summary>
        /// Cash management.
        /// </summary>
        public static AccountSubType CashManagement = new AccountSubType("cash management");

        /// <summary>
        /// Cd.
        /// </summary>
        public static AccountSubType Cd = new AccountSubType("cd");

        /// <summary>
        /// Certificate of deposit.
        /// </summary>
        public static AccountSubType CertificateOfDeposit = new AccountSubType("certificate of deposit");

        /// <summary>
        /// Checking account.
        /// </summary>
        public static AccountSubType Checking = new AccountSubType("checking");

        /// <summary>
        /// Credit account.
        /// </summary>
        public static AccountSubType Credit = new AccountSubType("credit");

        /// <summary>
        /// Credit card.
        /// </summary>
        public static AccountSubType CreditCard = new AccountSubType("credit card");

        /// <summary>
        /// Home loan or mortgage.
        /// </summary>
        public static AccountSubType Home = new AccountSubType("home");

        /// <summary>
        /// Paid installment account.
        /// </summary>
        public static AccountSubType Installment = new AccountSubType("installment");

        /// <summary>
        /// Ira account.
        /// </summary>
        public static AccountSubType Ira = new AccountSubType("ira");

        /// <summary>
        /// Line of credit.
        /// </summary>
        public static AccountSubType LineOfCredit = new AccountSubType("line of credit");

        /// <summary>
        /// Mortgage account.
        /// </summary>
        public static AccountSubType Mortgage = new AccountSubType("mortgage");

        /// <summary>
        /// Mutual fund.
        /// </summary>
        public static AccountSubType MutualFund = new AccountSubType("mutual_fund");

        /// <summary>
        /// Prepaid account.
        /// </summary>
        public static AccountSubType Prepaid = new AccountSubType("prepaid");

        /// <summary>
        /// Savings account.
        /// </summary>
        public static AccountSubType Savings = new AccountSubType("savings");

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountSubType"/> class.
        /// </summary>
        /// <param name="value">The string account subtype value.</param>
        public AccountSubType(string value)
            : base(value)
        {
        }
    }
}