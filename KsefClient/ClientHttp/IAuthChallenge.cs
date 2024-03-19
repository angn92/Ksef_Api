using JetBrains.Annotations;
using KsefClient.KsefContract.Session.AuthorisationChallenge;
using KsefClient.KsefContract.Session.InitSigned;
using System.Security.Cryptography.X509Certificates;

namespace KsefClient.ClientHttp
{
    public interface IAuthChallenge
    {
        ValueTask<AuthorisationChallengeResponse> GetAuthorisationChallengeAsync([NotNull] string type, [NotNull] string identifier);
        ValueTask<InitSignedResponse> InitSignedSession([NotNull] string initSignedFilePath);
    }
}
