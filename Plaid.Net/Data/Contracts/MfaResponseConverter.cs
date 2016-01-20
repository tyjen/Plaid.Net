namespace Plaid.Net.Contracts
{
    using System;
    using System.Linq;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Converter for JSON.net to convert a <see cref="MfaResponse"/> to/from json.
    /// </summary>
    internal class MfaResponseConverter : JsonConverter
    {
        /// <inheritdoc/>
        public override bool CanConvert(Type objectType)
        {
            return typeof(MfaResponse).IsAssignableFrom(objectType);
        }

        /// <inheritdoc/>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader == null) throw new ArgumentNullException(nameof(reader));
            if (objectType == null) throw new ArgumentNullException(nameof(objectType));
            if (serializer == null) throw new ArgumentNullException(nameof(serializer));

            // Load JObject from stream
            JObject jObject = JObject.Load(reader);

            string mfaType = jObject["type"].Value<string>();
            string accessToken = jObject["access_token"].Value<string>();

            switch (mfaType)
            {
                case "questions":
                    return new MfaResponse { AccessToken = accessToken, Type = mfaType, Prompt = jObject["mfa"].Values<MfaQuestionResponse>().ToList() };
                case "list":
                    return new MfaResponse { AccessToken = accessToken, Type = mfaType, Prompt = jObject["mfa"].Values<MfaCodeResponse>().ToList() };
                case "selections":
                    return new MfaResponse { AccessToken = accessToken, Type = mfaType, Prompt = jObject["mfa"].Values<MfaSelectionResponse>().ToList() };
                default:
                    throw new NotSupportedException("Unknown mfa response type: " + mfaType);
            }
        }

        /// <inheritdoc/>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotSupportedException();
        }
    }
}