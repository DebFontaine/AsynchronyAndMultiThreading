using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AsyncDemo.Helpers
{

    public class CodeDefinitions
    {
        [JsonPropertyName("name")]

        public string Name { get; set; }
        [JsonPropertyName("code")]
        public string Code { get; set; }
        [JsonPropertyName("comment")]
        public string Comments { get; set; }
    }

}
