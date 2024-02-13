using JetBrains.Annotations;
using KsefInfrastructure.CQRS;
using Microsoft.AspNetCore.Mvc;

namespace KsefApi.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class StatusController : Controller
    {
        private readonly IRequestDispatcher _requestDispatcher;

        public StatusController(IRequestDispatcher requestDispatcher)
        {
            _requestDispatcher = requestDispatcher;
        }

        public async Task<TokenStatusResponse> ValidateToken([NotNull] string identifier)
        {

        }
    }
}
