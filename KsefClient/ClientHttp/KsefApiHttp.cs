using JetBrains.Annotations;
using KsefClient.Helpers;
using KsefClient.KsefContract.Session;
using System.Text;
using System.Text.Json;

namespace KsefClient.ClientHttp
{
    public class KsefApiHttp : IAuthChallenge
    {
        private readonly HttpClient _httpClient;
        private readonly IUriHelper _uriHelper;

        public KsefApiHttp(HttpClient httpClient, IUriHelper uriHelper)
        {
            _httpClient = httpClient;
            _uriHelper = uriHelper;
            _httpClient.BaseAddress = new Uri("https://ksef-test.mf.gov.pl/api/");
        }

        public async ValueTask<AuthorisationChallengeResponse> GetAuthorisationChallengeAsync([NotNull] string type, [NotNull] string identifier)
        {
            var model = new AuthorisationChallengeRequest()
            {
                ContextIdentifier = new ContextIdentifier
                {
                    Type = type,
                    Identifier = identifier
                }
            };

            var request = JsonSerializer.Serialize(model);
            var content = new StringContent(request, Encoding.UTF8, "application/json");

            var httpResponseMessage = await _httpClient.PostAsync(_uriHelper.GenerateUri(_httpClient.BaseAddress.ToString(), RestEndpoint.AuthChallenge), content);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                // Error 
                
            }

            var result = httpResponseMessage.Content.ReadAsStringAsync().Result;
            var authChallengeResponse = JsonSerializer.Deserialize<AuthorisationChallengeResponse>(result);

            return new AuthorisationChallengeResponse(authChallengeResponse.Timestamp, authChallengeResponse.Challenge);
        }
    }
}