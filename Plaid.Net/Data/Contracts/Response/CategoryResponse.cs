namespace Plaid.Net.Contracts.Response
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Plaid.Net.Models;

    internal class CategoryResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("hierarchy")]
        public IList<string> Hierarchy { get; set; }

        public Category ToCategory()
        {
            Category c = new Category();
            c.Id = this.Id;
            c.Type = this.Type;

            if (this.Hierarchy != null)
            {
                c.Hierarchy = new List<string>(this.Hierarchy);
            }

            return c;
        }
    }
}
