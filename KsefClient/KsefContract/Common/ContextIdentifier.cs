using System.Text.Json.Serialization;

namespace KsefClient.KsefContract.Common
{
    public class ContextIdentifier
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("identifier")]
        public string Identifier { get; set; }
    }
}
