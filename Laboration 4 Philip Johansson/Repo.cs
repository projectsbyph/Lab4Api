using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Laboration_4_Philip_Johansson
{
    public class Repo // Representerar ett GitHub-repo
    {
        [JsonPropertyName("name")] //JsonPropertyName används för att mappa JSON-fältet till egenskapen 
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("html_url")]
        public string HtmlUrl { get; set; }

        [JsonPropertyName("homepage")]
        public string Homepage { get; set; }

        [JsonPropertyName("watchers")]
        public int Watchers { get; set; }

        [JsonPropertyName("pushed_at")]
        public DateTime PushedAt { get; set; }
    }
}
