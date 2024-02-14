using KsefClient.ClientHttp;
using KsefClient.Helpers;
using KsefInfrastructure.CQRS;
using KsefInfrastructure.Validation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KsefInfrastructure.Contract.Token.Create
{
    public class InitTokenInvocation : IRequestHandler<InitTokenRequest>
    {
        private readonly IAuthChallenge _authChallenge;
        private readonly ILogger<InitTokenInvocation> _logger;
        private readonly IXmlHelper _xmlHelper;
        private readonly IConfiguration _configuration;

        public InitTokenInvocation(IAuthChallenge authChallenge, ILogger<InitTokenInvocation> logger, IXmlHelper xmlHelper,
            IConfiguration configuration)
        {
            _authChallenge = authChallenge;
            _logger = logger;
            _xmlHelper = xmlHelper;
            _configuration = configuration;
        }

        public async ValueTask HandleAsync(InitTokenRequest request, CancellationToken cancellationToken = default)
        {
            Fail.IfNull(request.Command.Identifier);
            Fail.IfNull(request.Command.Type);

            //todo: Validate NIP number

            _logger.LogInformation("Starting generate authorization token");
            // Get auth chellenge first step
            var authorizationChallenge = await _authChallenge.GetAuthorisationChallengeAsync(request.Command.Type, request.Command.Identifier);

            // InitSigned second step
            _xmlHelper.PrepareInitSessionXmlRequest(BuildFilePath(), authorizationChallenge.Challenge, request.Command.Identifier, "token");
        }

        private string BuildFilePath()
        {
            var sb = new StringBuilder();
            sb.Append(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName);
              var path = _configuration.GetSection("InitSignedXmlFilePath").Value;

            sb.Append(path);
            return sb.ToString();
        }
    }
}
