using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountryDataApplication.Models
{
    public class CountryResponse
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("numericCode")]
        public string NumericCode { get; set; }

        [JsonProperty("capital")]
        public string Capital { get; set; }

        [JsonProperty("area")]
        public double Area { get; set; }

        [JsonProperty("population")]
        public int Population { get; set; }

        [JsonProperty("region")]
        public string Region{ get; set; }
    }
}
