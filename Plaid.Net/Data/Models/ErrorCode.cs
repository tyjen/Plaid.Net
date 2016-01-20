namespace Plaid.Net.Models
{
    using System;

    /// <summary>
    /// Error codes returned from Plaid.
    /// See: https://github.com/plaid/Support/blob/master/errors.md
    /// </summary>
    public enum ErrorCode
    {
        /// <summary>
        /// Default unknown error.
        /// </summary>
        Unknown = 0, 

        /// <summary>
        /// You need to include the access_token that you received from the original submit call.
        /// </summary>
        AccessTokenMissing = 1000, 

        /// <summary>
        /// You need to include a type parameter. Ex. bofa, wells, amex, chase, citi, etc.
        /// </summary>
        InstitutionTypeMissing = 1001, 

        /// <summary>
        /// You included an access_token on a submit call - this is only allowed on step and get routes.
        /// </summary>
        AccessTokenDisallowed = 1003, 

        /// <summary>
        /// This access token format is no longer supported. Contact support to resolve.
        /// </summary>
        UnsupportedAccessToken = 1008, 

        /// <summary>
        /// Options need to be JSON or stringified JSON.
        /// </summary>
        InvalidOptionsFormat = 1004, 

        /// <summary>
        /// Provide username, password, and pin if appropriate.
        /// </summary>
        CredentialsMissing = 1005, 

        /// <summary>
        /// Credentials need to be JSON or stringified JSON.
        /// </summary>
        InvalidCredentialsFormat = 1006, 

        /// <summary>
        /// In order to upgrade an account, an upgrade_to field is required , ex. connect
        /// </summary>
        UpgradeToRequired = 1007, 

        /// <summary>
        /// Valid 'Content-Type' headers are 'application/json' and 'application/x-www-form-urlencoded' with an optional 'UTF-8' charset.
        /// </summary>
        InvalidContentType = 1009, 

        /// <summary>
        /// Include your Client ID so we know who you are.
        /// </summary>
        ClientIdMissing = 1100, 

        /// <summary>
        /// Include your Secret so we can verify your identity.
        /// </summary>
        SecretMissing = 1101, 

        /// <summary>
        /// The Client ID does not exist or the Secret does not match the Client ID you provided.
        /// </summary>
        SecretOrClientIdInvalid = 1102, 

        /// <summary>
        /// Your Client ID does not have access to this product. Contact us to purchase this product.
        /// </summary>
        UnauthorizedProduct = 1104, 

        /// <summary>
        /// This access_token appears to be corrupted.
        /// </summary>
        BadAccessToken = 1105, 

        /// <summary>
        /// This public_token is corrupt or does not exist in our database.
        /// </summary>
        BadPublicToken = 1106, 

        /// <summary>
        /// Include the public_token received from the Plaid Link module.
        /// </summary>
        MissingPublicToken = 1107, 

        /// <summary>
        /// This institution is not currently supported.
        /// </summary>
        InvalidInstitutionType = 1108, 

        /// <summary>
        /// This product is not enabled for this item. Use the upgrade route to add it.
        /// </summary>
        ProductNotEnabled = 1110, 

        /// <summary>
        /// Specify a valid product to upgrade this item to.
        /// </summary>
        InvalidUpgrade = 1111, 

        /// <summary>
        /// You have reached the maximum number of additions. Contact us to raise your limit.
        /// </summary>
        AdditionLimitExceeded = 1112, 

        /// <summary>
        /// You have exceeded your request rate limit for this product. Try again soon.
        /// </summary>
        RateLimitExceeded = 1113, 

        /// <summary>
        /// The username or password provided were not correct.
        /// </summary>
        InvalidCredentials = 1200, 

        /// <summary>
        /// The username provided was not correct.
        /// </summary>
        InvalidUsername = 1201, 

        /// <summary>
        /// The password provided was not correct.
        /// </summary>
        InvalidPassword = 1202, 

        /// <summary>
        /// The MFA response provided was not correct.
        /// </summary>
        InvalidMfa = 1203, 

        /// <summary>
        /// The MFA send_method provided was invalid. Consult the documentation for the proper format.
        /// </summary>
        InvalidSendMethod = 1204, 

        /// <summary>
        /// The account is locked. Prompt the user to visit the issuing institution's site and unlock their account.
        /// </summary>
        AccountLocked = 1205, 

        /// <summary>
        /// The account has not been fully set up. Prompt the user to visit the issuing institution's site and finish the setup process.
        /// </summary>
        AccountNotSetup = 1206, 

        /// <summary>
        /// United States-only at this point!
        /// </summary>
        CountryNotSupported = 1207, 

        /// <summary>
        /// This account requires MFA to access - we're currently not supporting MFA through this institution.
        /// </summary>
        MfaNotSupported = 1208, 

        /// <summary>
        /// The pin provided was not correct.
        /// </summary>
        InvalidPin = 1209, 

        /// <summary>
        /// This account is currently not supported.
        /// </summary>
        AccountNotSupported = 1210, 

        /// <summary>
        /// The security rules for this account restrict access. Disable 'Extra Security at Sign-In' in your Bank of America settings.
        /// </summary>
        BankOfAmericaRestricted = 1211, 

        /// <summary>
        /// No valid accounts exist for this user.
        /// </summary>
        NoAccounts = 1212, 

        /// <summary>
        /// MFA access has changed or this application's access has been revoked. Submit a PATCH call to resolve.
        /// </summary>
        MfaReset = 1215, 

        /// <summary>
        /// This item does not require the MFA process at this time.
        /// </summary>
        MfaNotRequired = 1218, 

        /// <summary>
        /// This institution is not yet available in this environment.
        /// </summary>
        InstitutionNotAvailable = 1300, 

        /// <summary>
        /// Double-check the provided institution ID.
        /// </summary>
        InstitutionNotFound = 1301, 

        /// <summary>
        /// The institution is failing to respond to our request, if you resubmit the query the request may go through.
        /// </summary>
        InstitutionNotResponding = 1302, 

        /// <summary>
        /// The institution is down for an indeterminate amount of time, if you resubmit in a couple hours it may go through.
        /// </summary>
        InstitutionDown = 1303, 

        /// <summary>
        /// Double-check the provided category ID.
        /// </summary>
        CategoryNotFound = 1501, 

        /// <summary>
        /// You must include a type parameter.
        /// </summary>
        TypeRequired = 1502, 

        /// <summary>
        /// The specified type is not supported.
        /// </summary>
        InvalidType = 1503, 

        /// <summary>
        /// Consult the documentation for valid date formats.
        /// </summary>
        InvalidDate = 1507, 

        /// <summary>
        /// This product doesn't exist yet.
        /// </summary>
        ProductNotFound = 1600, 

        /// <summary>
        /// This product is not yet available for this institution.
        /// </summary>
        ProductNotAvailable = 1601, 

        /// <summary>
        /// User was previously deleted from our system.
        /// </summary>
        UserNotFound = 1605, 

        /// <summary>
        /// The account ID provided was not correct.
        /// </summary>
        AccountNotFound = 1606, 

        /// <summary>
        /// No matching items found; go add an account!
        /// </summary>
        ItemNotFound = 1610, 

        /// <summary>
        /// We failed to pull the required information from the institution - make sure the user can access their account; we have been notified.
        /// </summary>
        ExtractorError = 1700, 

        /// <summary>
        /// We failed to pull the required information from the institution - resubmit this query.
        /// </summary>
        ExtractorErrorRetry = 1701, 

        /// <summary>
        /// An unexpected error has occurred on our systems; we've been notified and are looking into it!
        /// </summary>
        PlaidError = 1702, 

        /// <summary>
        /// Portions of our system are down for maintence. This route is inaccessible. GET requests to Auth and Connect may succeed.
        /// </summary>
        PlannedMaintenance = 1800
    }
}