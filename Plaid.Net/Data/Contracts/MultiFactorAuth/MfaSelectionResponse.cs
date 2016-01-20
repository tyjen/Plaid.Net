namespace Plaid.Net.Contracts
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Plaid.Net.Models;

    /// <summary>
    /// Type of mfa response indicating the user should select the correct answer.
    /// </summary>
    /// <remarks>
    /// Some institutions require the user to answer a question from a limited set of answers, i.e. multiple-choice.
    /// Multiple questions may be returned, and the MFA answer submission must be a JSON-encoded array with answers 
    /// provided in the same order as the given questions.
    /// </remarks>
    internal class MfaSelectionResponse
    {
        /// <summary>
        /// Gets or sets the list of possible answers.
        /// </summary>
        [JsonProperty("answers")]
        public IList<string> Answers { get; set; }

        /// <summary>
        /// Gets or sets the question to answer.
        /// </summary>
        [JsonProperty("question")]
        public string Question { get; set; }

        /// <summary>
        /// Converts this contract into its <see cref="MultipleChoiceQuestion"/> model.
        /// </summary>
        /// <returns>The <see cref="MultipleChoiceQuestion"/> model.</returns>
        public MultipleChoiceQuestion ToSelectionItem()
        {
            return new MultipleChoiceQuestion { Options = new List<string>(this.Answers), Question = this.Question };
        }
    }
}