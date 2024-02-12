using JetBrains.Annotations;
using KsefClient.KsefContract.Session.AuthorisationChallenge;

namespace KsefClient.ClientHttp
{
    public interface IAuthChallenge
    {
        ValueTask<AuthorisationChallengeResponse> GetAuthorisationChallengeAsync([NotNull] string type, [NotNull] string identifier);
    }
}
