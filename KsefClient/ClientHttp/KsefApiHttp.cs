using JetBrains.Annotations;
using KsefClient.Common;
using KsefClient.Helpers;
using KsefClient.KsefContract.Common;
using KsefClient.KsefContract.Session.AuthorisationChallenge;
using KsefClient.KsefContract.Session.InitSigned;
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
        private readonly IXmlHelper _xmlHelper;
        private const string Json = "application/json";
        private const string Octet = "application/octet-stream";

        public KsefApiHttp(HttpClient httpClient, IUriHelper uriHelper, ILogger<KsefApiHttp> logger, IXmlHelper xmlHelper)
        {
            _httpClient = httpClient;
            _uriHelper = uriHelper;
            _logger = logger;
            _xmlHelper = xmlHelper;
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
                throw new Exception($"Sending request is fail {httpResponseMessage.StatusCode}"); 
            
            var result = httpResponseMessage.Content.ReadAsStringAsync().Result;
            var authChallengeResponse = JsonSerializer.Deserialize<AuthorisationChallengeResponse>(result);

            _logger.LogInformation(KsefLogData.BuildResponseLog(result, path, HttpMethod.Post, (int)httpResponseMessage.StatusCode));

            return new AuthorisationChallengeResponse(authChallengeResponse.Timestamp, authChallengeResponse.Challenge);
        }

        public async ValueTask<InitSignedResponse> InitSignedSession([NotNull] string initSession)
        {
            var content = new StringContent(initSession, Encoding.UTF8, Octet);

            var path = _uriHelper.GenerateUri(_httpClient.BaseAddress.ToString(), RestEndpoint.InitSegned);

            var httpResponseMessage = await _httpClient.PostAsync(path, content);

            var result = httpResponseMessage.Content.ReadAsStringAsync().Result;
            var initSignedResponse = JsonSerializer.Deserialize<InitSignedResponse>(result);

            return initSignedResponse;
        }
    }
}