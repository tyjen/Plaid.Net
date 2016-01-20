namespace Plaid.Net.Models
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// A multiple choice question with a set of answers the user must answer for two-factor auth.
    /// </summary>
    /// <remarks>
    /// Some institutions require the user to answer a question from a limited set of answers, i.e. multiple-choice.
    /// </remarks>
    public class MultipleChoiceQuestion
    {
        /// <summary>
        /// Gets or sets the possible answers.
        /// </summary>
        [JsonProperty("answers")]
        public IList<string> Options { get; set; }

        /// <summary>
        /// Gets or sets the multiple-choice question.
        /// </summary>
        [JsonProperty("question")]
        public string Question { get; set; }
    }
}