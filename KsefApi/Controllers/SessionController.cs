using KsefClient.ClientHttp;
using KsefInfrastructure.CQRS;
using KsefInfrastructure.Session;
using Microsoft.AspNetCore.Mvc;

namespace KsefApi.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly IRequestDispatcher _requestDispatcher;
        private readonly IAuthChallenge _authChallenge;
        
        public SessionController(IRequestDispatcher requestDispatcher, IAuthChallenge authChallenge)
        {
            _requestDispatcher = requestDispatcher;
            _authChallenge = authChallenge;
        }

        [HttpPost]
        [Route("CreateSession")]
        public async Task<CreateSessionResponse> CreateSession([FromBody] CreateSessionRequest request)
        {
            return await _requestDispatcher.DispatchAsync<CreateSessionRequest, CreateSessionResponse>(request);
        }
    }
}
