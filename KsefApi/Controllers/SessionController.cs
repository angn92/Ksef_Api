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
        private readonly ILogger<SessionController> _logger;

        public SessionController(IRequestDispatcher requestDispatcher, ILogger<SessionController> logger)
        {
            _requestDispatcher = requestDispatcher;
            _logger = logger;
        }

        /// <summary>
        /// Init session with Ksef API
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateSession")]
        public async Task<CreateSessionResponse> CreateSession([FromBody] CreateSessionRequest request)
        {
            _logger.LogInformation("Starting session ksef");
            return await _requestDispatcher.DispatchAsync<CreateSessionRequest, CreateSessionResponse>(request);
        }
    }
}
