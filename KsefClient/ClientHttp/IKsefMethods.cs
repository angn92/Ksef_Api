using JetBrains.Annotations;
using KsefClient.KsefContract.Session.AuthorisationChallenge;
using KsefClient.KsefContract.Session.InitSigned;

namespace KsefClient.ClientHttp
{
    public interface IKsefMethods
    {
        ValueTask<AuthorisationChallengeResponse> GetAuthorisationChallengeAsync([NotNull] string type, [NotNull] string identifier);
        ValueTask<InitSignedResponse> InitSignedSessionAsync([NotNull] string initSession);
    }
}
