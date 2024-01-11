using System.Text.Json.Serialization;

namespace KsefClient.KsefContract.Session
{
    public class AuthorisationChallengeRequest
    {
        [JsonPropertyName("contextIdentifier")]
        public ContextIdentifier ContextIdentifier { get; set; }
    }

    public class ContextIdentifier
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("identifier")]
        public string Identifier { get; set; }
    }
}