using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Laboration_4_Philip_Johansson
{
    // Modeller för Zippopotam API-svar
    public class ZippopotamResponse 
    {
        [JsonPropertyName("post code")] //JsonPropertyName används för att mappa JSON-fältet till egenskapen
        public string PostCode { get; set; }
        
        [JsonPropertyName("country")]
        public string Country { get; set; }
        
        [JsonPropertyName("country abbreviation")]
        public string CountryAbbreviation { get; set; }
        
        [JsonPropertyName("places")]
        public List<Place> Places { get; set; }
    }

    // Modell för en plats i Zippopotam API-svar
    public class Place
    {
        [JsonPropertyName("place name")]
        public string PlaceName { get; set; }
        
        [JsonPropertyName("longitude")]
        public string Longitude { get; set; }
        
        [JsonPropertyName("state")]
        public string State { get; set; }
        
        [JsonPropertyName("state abbreviation")]
        public string StateAbbreviation { get; set; }
        
        [JsonPropertyName("latitude")]
        public string Latitude { get; set; }
    }
}
