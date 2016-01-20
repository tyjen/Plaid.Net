namespace Plaid.Net.Contracts.Response
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Plaid.Net.Models;

    /// <summary>
    /// Institution response.
    /// </summary>
    internal class InstitutionResponse
    {
        [JsonProperty("credentials")]
        public CredentialsResponse Credentials { get; set; }

        [JsonProperty("has_mfa")]
        public bool HasMfa { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("mfa")]
        public IList<string> Mfa { get; set; }

        [JsonProperty("products")]
        public IList<string> Products { get; set; }

        public Institution ToInstitution()
        {
            Institution inst = new Institution();
            inst.HasMfa = this.HasMfa;
            inst.Id = this.Id;
            inst.Name = this.Name;
            inst.PasswordHint = this.Credentials?.Password;
            inst.UsernameHint = this.Credentials?.Username;

            if (this.Mfa != null)
            {
                inst.MfaDescriptions = new List<string>(this.Mfa);
            }
            if (!string.IsNullOrWhiteSpace(this.Type))
            {
                inst.Type = new InstitutionType(this.Type);
            }

            return inst;
        }
    }

    internal class CredentialsResponse
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}