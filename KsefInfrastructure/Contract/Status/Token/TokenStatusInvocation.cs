using KsefInfrastructure.CQRS;
using KsefInfrastructure.EF;
using KsefInfrastructure.Validation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KsefInfrastructure.Contract.Status.Token
{
    public class TokenStatusInvocation : IRequestHandler<TokenStatusRequest, TokenStatusResponse>
    {
        private readonly ILogger<TokenStatusInvocation> _logger;
        private readonly AppDbContext _context;

        public TokenStatusInvocation(ILogger<TokenStatusInvocation> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async ValueTask<TokenStatusResponse> HandleAsync(TokenStatusRequest request, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Checking status token");

            Fail.IfNull(request.Identifier);

            var authorizationToken = await _context.AuthorizationToken.FirstOrDefaultAsync(x => x.Nip == request.Identifier);

            return authorizationToken == null ? throw new Exception() : new TokenStatusResponse
            {
                Status = authorizationToken.Status,
                ModifiedTime = authorizationToken.Modified
            };
        }
    }
}
