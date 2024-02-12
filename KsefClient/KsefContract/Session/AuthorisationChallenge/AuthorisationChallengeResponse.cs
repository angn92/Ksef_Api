using JetBrains.Annotations;
using System.Text.Json.Serialization;

namespace KsefClient.KsefContract.Session.AuthorisationChallenge
{
    public class AuthorisationChallengeResponse
    {
        [JsonPropertyName("timestamp")]
        public string Timestamp { get; set; }

        [JsonPropertyName("challenge")]
        public string Challenge { get; set; }

        public AuthorisationChallengeResponse([NotNull] string timestamp, [NotNull] string challenge)
        {
            Timestamp = timestamp;
            Challenge = challenge;
        }
    }
}
