namespace Plaid.Net.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using Plaid.Net.Models;

    /// <summary>
    /// The mfa response contract.
    /// </summary>
    internal class MfaResponse
    {
        /// <summary>
        /// Gets or sets the user's access token.
        /// </summary>
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        /// <summary>
        /// Gets or sets the mfa prompt. This can be
        /// a question, code, or selection-based response.
        /// </summary>
        [JsonProperty("mfa")]
        public object Prompt { get; set; }

        /// <summary>
        /// Gets or sets the type of multi-factor auth required.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// Converts this contract into its <see cref="AuthenticationPrompt"/> model.
        /// </summary>
        /// <returns>The <see cref="AuthenticationPrompt"/> model.</returns>
        public AuthenticationPrompt ToAuthenticationPrompt()
        {
            AuthenticationPrompt authPrompt = new AuthenticationPrompt();
            authPrompt.AuthType = new AuthType(this.Type);
            JArray mfaArray = this.Prompt as JArray;

            if (authPrompt.AuthType == AuthType.Device)
            {
                JObject jobj = this.Prompt as JObject;
                string message = jobj["message"].Value<string>();
                authPrompt.DeviceMessage = message;
            }
            else if (authPrompt.AuthType == AuthType.Code)
            {
                IList<MfaCodeResponse> codeList = mfaArray?.ToObject<IList<MfaCodeResponse>>();
                authPrompt.CodeDeliveryOptions = codeList?.Select(c => c.ToCodeOption()).ToList();
            }
            else if (authPrompt.AuthType == AuthType.Questions)
            {
                IList<MfaQuestionResponse> questionList = mfaArray?.ToObject<IList<MfaQuestionResponse>>();
                authPrompt.Questions = questionList?.Select(c => c.Question).ToList();
            }
            else if (authPrompt.AuthType == AuthType.Selection)
            {
                IList<MfaSelectionResponse> selectionList = mfaArray?.ToObject<IList<MfaSelectionResponse>>();
                authPrompt.MultipleChoiceQuestions = selectionList?.Select(c => c.ToSelectionItem()).ToList();
            }

            return authPrompt;
        }
    }
}