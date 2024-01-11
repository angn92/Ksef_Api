using JetBrains.Annotations;
using KsefClient.KsefContract.Session;
using System.Text;
using System.Text.Json;

namespace KsefClient.ClientHttp
{
    public class KsefApiHttp : IAuthChallenge
    {
        private readonly HttpClient _httpClient1;
        
        public KsefApiHttp(HttpClient httpClient1)
        {
            _httpClient1 = httpClient1;
        }

        public async ValueTask<AuthorisationChallengeResponse> GetAuthorisationChallengeAsync([NotNull] string type, [NotNull] string identifier)
        {
            var model = new AuthorisationChallengeRequest
            {
                ContextIdentifier = new ContextIdentifier
                {
                    Type = type,
                    Identifier = identifier
                }
            };

            var request = JsonSerializer.Serialize(model);
            var content = new StringContent(request, Encoding.UTF8, "application/json");

            var response = await _httpClient1.PostAsync("https://ksef-test.mf.gov.pl/api/online/Session/AuthorisationChallenge", content);

            var result = await response.Content.ReadAsStringAsync();

            var authChallengeResponse = JsonSerializer.Deserialize<AuthorisationChallengeResponse>(result);

            return new AuthorisationChallengeResponse(authChallengeResponse.Timestamp, authChallengeResponse.Challenge);
        }
    }
}