using JetBrains.Annotations;
using KsefClient.Common;
using KsefClient.Helpers;
using KsefClient.KsefContract.Session;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;

namespace KsefClient.ClientHttp
{
    public class KsefApiHttp : IAuthChallenge
    {
        private readonly HttpClient _httpClient;
        private readonly IUriHelper _uriHelper;
        private readonly ILogger<KsefApiHttp> _logger;
        private const string Json = "application/json";
        
        public KsefApiHttp(HttpClient httpClient, IUriHelper uriHelper, ILogger<KsefApiHttp> logger)
        {
            _httpClient = httpClient;
            _uriHelper = uriHelper;
            _logger = logger;
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
            var content = new StringContent(request, Encoding.UTF8, Json);

            var path = _uriHelper.GenerateUri(_httpClient.BaseAddress.ToString(), RestEndpoint.AuthChallenge);

            _logger.LogInformation(KsefLogData.BuildRequestLog(request, path, HttpMethod.Post));

            var httpResponseMessage = await _httpClient.PostAsync(path, content);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                // Error 
                
            }

            var result = httpResponseMessage.Content.ReadAsStringAsync().Result;
            var authChallengeResponse = JsonSerializer.Deserialize<AuthorisationChallengeResponse>(result);

            _logger.LogInformation(KsefLogData.BuildResponseLog(result, path, HttpMethod.Post, (int)httpResponseMessage.StatusCode));

            return new AuthorisationChallengeResponse(authChallengeResponse.Timestamp, authChallengeResponse.Challenge);
        }
    }
}