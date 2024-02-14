using KsefInfrastructure.CQRS;
using KsefInfrastructure.EF;
using KsefInfrastructure.Validation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Xml.Linq;

namespace KsefInfrastructure.Contract.Token.Validate
{
    public class TokenStatusInvocation : IRequestHandler<TokenStatusRequest, TokenStatusResponse>
    {
        private readonly ILogger<TokenStatusInvocation> _logger;
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public TokenStatusInvocation(ILogger<TokenStatusInvocation> logger, AppDbContext context, IConfiguration configuration)
        {
            _logger = logger;
            _context = context;
            _configuration = configuration;
        }

        public async ValueTask<TokenStatusResponse> HandleAsync(TokenStatusRequest request, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Checking status token");

            Fail.IfNull(request.Identifier);

            var authorizationToken = await _context.AuthorizationToken.FirstOrDefaultAsync(x => x.Nip == request.Identifier);
 
            return authorizationToken == null ? throw new Exception("No token") : new TokenStatusResponse
            {
                Status = authorizationToken.Status,
                ModifiedTime = authorizationToken.Modified
            };
        }
    }
}
