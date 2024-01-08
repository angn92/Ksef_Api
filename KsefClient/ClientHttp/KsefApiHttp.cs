using KsefClient.KsefSession;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

namespace KsefClient.ClientHttp
{
    public class KsefApiHttp : IAuthChallenge
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public KsefApiHttp(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(_configuration.GetSection("KsefAddress").Value);
        }

        public async ValueTask<AuthorisationChallenge> GetAuthorisationChallengeAsync(string type, string identifier)
        {
            var challenge = await _httpClient.GetFromJsonAsync<AuthorisationChallenge>(UriConfiguration.AuthChallenge);
            return challenge;
        }
    }
}