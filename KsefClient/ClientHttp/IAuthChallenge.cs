using KsefClient.KsefSession;

namespace KsefClient.ClientHttp
{
    public interface IAuthChallenge
    {
        ValueTask<AuthorisationChallenge> GetAuthorisationChallengeAsync(string type, string identifier);
    }
}
