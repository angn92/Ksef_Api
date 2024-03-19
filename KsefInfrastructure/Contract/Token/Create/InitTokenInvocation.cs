using KsefClient.ClientHttp;
using KsefClient.Helpers;
using KsefInfrastructure.CQRS;
using KsefInfrastructure.Helper;
using KsefInfrastructure.Validation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace KsefInfrastructure.Contract.Token.Create
{
    public class InitTokenInvocation : IRequestHandler<InitTokenRequest>
    {
        private readonly IAuthChallenge _authChallenge;
        private readonly ILogger<InitTokenInvocation> _logger;
        private readonly IXmlHelper _xmlHelper;
        private readonly IConfiguration _configuration;
        private readonly ICertificateHelper _certificateHelper;

        public InitTokenInvocation(IAuthChallenge authChallenge, ILogger<InitTokenInvocation> logger, IXmlHelper xmlHelper,
            IConfiguration configuration, ICertificateHelper certificateHelper)
        {
            _authChallenge = authChallenge;
            _logger = logger;
            _xmlHelper = xmlHelper;
            _configuration = configuration;
            _certificateHelper = certificateHelper;
        }

        public async ValueTask HandleAsync(InitTokenRequest request, CancellationToken cancellationToken = default)
        {
            Fail.IfNull(request.Command.Identifier);
            Fail.IfNull(request.Command.Type);

            //todo: Validate NIP number
            
            


            _logger.LogInformation("Starting generate authorization token");
            // Get auth chellenge first step
            var authorizationChallenge = await _authChallenge.GetAuthorisationChallengeAsync(request.Command.Type, request.Command.Identifier);

            // Fill InitSessionSignedRq file
            _xmlHelper.PrepareInitSessionXmlRequest(BuildFilePath("InitSignedXmlFilePath"), authorizationChallenge.Challenge, 
                request.Command.Identifier);

            // Xades
            var certificate = _certificateHelper.GetCertificate(Environment.GetEnvironmentVariable("Thumbprint").ToUpper() ?? "",
                StoreLocation.LocalMachine) ?? throw new InvalidOperationException();

            var xadesSign = _xmlHelper.PrepareXadesFile(BuildFilePath("InitSignedXmlFilePath"), certificate);

            var initSession = await _authChallenge.InitSignedSession(BuildFilePath("InitSignedXmlFilePath"));
        }

        private string BuildFilePath(string fileName)
        {
            var sb = new StringBuilder();
            sb.Append(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName);
            sb.Append(_configuration.GetSection(fileName).Value);
            return sb.ToString();
        }
    }
}
