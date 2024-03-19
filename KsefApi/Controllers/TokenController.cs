using JetBrains.Annotations;
using KsefInfrastructure.Contract.Token.Create;
using KsefInfrastructure.Contract.Token.Validate;
using KsefInfrastructure.CQRS;
using Microsoft.AspNetCore.Mvc;

namespace KsefApi.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class TokenController : Controller
    {
        private readonly IRequestDispatcher _requestDispatcher;

        public TokenController(IRequestDispatcher requestDispatcher)
        {
            _requestDispatcher = requestDispatcher;
        }

        /// <summary>
        /// Check do you have active token or not
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ValidateToken/{identifier}")]
        public async Task<TokenStatusResponse> ValidateToken([NotNull] [FromRoute] string identifier)
        {
            var request = new TokenStatusRequest
            {
                Identifier = identifier
            };

            return await _requestDispatcher.DispatchAsync<TokenStatusRequest, TokenStatusResponse>(request);
        }

        /// <summary>
        /// Generate new token to communicate with Ksef API
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Generate")]
        public async ValueTask GenerateToken([NotNull] [FromBody] InitTokenCommand command)
        {
            var request = new InitTokenRequest(command);

            await _requestDispatcher.DispatchAsync<InitTokenRequest>(request);
        }
    }
}
