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
        
        public SessionController(IRequestDispatcher requestDispatcher)
        {
            _requestDispatcher = requestDispatcher;
        }

        /// <summary>
        /// End point for for starting connection with Ksef API
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateSession")]
        public async Task<CreateSessionResponse> CreateSession([FromBody] CreateSessionRequest request)
        {
            return await _requestDispatcher.DispatchAsync<CreateSessionRequest, CreateSessionResponse>(request);
        }
    }
}
