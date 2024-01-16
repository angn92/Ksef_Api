using System.Text.Json.Serialization;

namespace KsefInfrastructure.Session
{
    public class CreateSessionResponse
    {
        [JsonPropertyName("Timestamp")]
        public string Timestamp { get; set; }

        [JsonPropertyName("Challenge")]
        public string Challenge { get; set; }
    }
}
