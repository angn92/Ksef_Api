using JetBrains.Annotations;

namespace KsefClient.KsefSession
{
    public class AuthorisationChallenge
    {
        public string Timestamp { get; set; }
        public string Challenge { get; set; }

        public AuthorisationChallenge([NotNull] string timestamp, [NotNull] string challenge)
        {
            Timestamp = timestamp;
            Challenge = challenge;
        }
    }
}
