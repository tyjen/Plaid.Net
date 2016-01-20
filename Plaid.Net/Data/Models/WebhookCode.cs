namespace Plaid.Net.Models
{
    using System;

    /// <summary>
    /// Code used by Plaid when invoking a webhook.
    /// </summary>
    public enum WebhookCode
    {
        /// <summary>
        /// Occurs once the initial transaction pull has finished.
        /// </summary>
        InitialPullComplete = 0, 

        /// <summary>
        /// Occurs once the historical transaction pull has completed, shortly after the initial transaction pull.
        /// </summary>
        HistoricalPullComplete = 1, 

        /// <summary>
        /// Occurs at set intervals throughout the day as data is updated from the financial institutions.
        /// </summary>
        TransactionsUpdated = 2, 

        /// <summary>
        /// Occurs when transactions have been removed from Plaid's system.
        /// </summary>
        TransactionsRemoved = 3, 

        /// <summary>
        /// Occurs when an user's webhook is updated via a PATCH request without credentials.
        /// </summary>
        WebhookUpdated = 4, 

        /// <summary>
        /// Triggered when an error has occured. Includes the relevant Plaid error code with details on both the error type and steps for error resolution.
        /// </summary>
        Other
    }
}