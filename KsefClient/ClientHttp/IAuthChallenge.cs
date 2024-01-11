using JetBrains.Annotations;
using KsefClient.KsefContract.Session;

namespace KsefClient.ClientHttp
{
    public interface IAuthChallenge
    {
        ValueTask<AuthorisationChallengeResponse> GetAuthorisationChallengeAsync([NotNull] string type, [NotNull] string identifier);
    }
}
