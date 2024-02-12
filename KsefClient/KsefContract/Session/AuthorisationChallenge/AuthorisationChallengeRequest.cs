using KsefClient.KsefContract.Common;
using System.Text.Json.Serialization;

namespace KsefClient.KsefContract.Session.AuthorisationChallenge
{
    public class AuthorisationChallengeRequest
    {
        [JsonPropertyName("contextIdentifier")]
        public ContextIdentifier ContextIdentifier { get; set; }
    }
}