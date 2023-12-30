using KsefClient.KsefSession;

namespace KsefClient.ClientHttp
{
    public class KsefApiHttp : IAuthChallenge
    {
        public ValueTask<AuthorisationChallenge> GetAuthorisationChallengeAsync(string type, string identifier)
        {
            throw new NotImplementedException();
        }
    }
}
 