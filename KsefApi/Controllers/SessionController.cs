using KsefClient.ClientHttp;
using Microsoft.AspNetCore.Mvc;

namespace KsefApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly IAuthChallenge _authChallenge;
        

        public SessionController(IAuthChallenge authChallenge)
        {
            _authChallenge = authChallenge;
            
        }

        [HttpGet]
        [Route("/[controller]/[action]/{id}")]
        public async Task<string> Overview(int? id)
        {
            await _authChallenge.GetAuthorisationChallengeAsync("onip", "6770065406");
            return "aa";
        }
    }
}
