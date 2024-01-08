using Microsoft.AspNetCore.Mvc;

namespace KsefApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        [HttpGet]
        [Route("/[controller]/[action]/{id}")]
        public ActionResult Overview(int? id)
        {
            return Ok(id);
        }
    }
}
