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
        private readonly ILogger<InitTokenInvocation> _logger;
        private readonly IKsefMethods _ksefMethods;
        private readonly IXmlHelper _xmlHelper;
        private readonly IConfiguration _configuration;
        private readonly ICertificateHelper _certificateHelper;

        public InitTokenInvocation(ILogger<InitTokenInvocation> logger, IKsefMethods ksefMethods, IXmlHelper xmlHelper,
            IConfiguration configuration, ICertificateHelper certificateHelper)
        {
            _ksefMethods = ksefMethods;
            _logger = logger;
            _xmlHelper = xmlHelper;
            _configuration = configuration;
            _certificateHelper = certificateHelper;
        }

        public async ValueTask HandleAsync(InitTokenRequest request, CancellationToken cancellationToken = default)
        {
            Fail.IfNull(request.Command.Identifier);
            Fail.IfNull(request.Command.Type);

            if (request.Command.Identifier.Length != 10)
                throw new Exception("Wrong length of NIP number.");
            
            _logger.LogInformation("Starting generate authorization token");

            // Get auth chellenge first step
            var authorizationChallenge = await _ksefMethods.GetAuthorisationChallengeAsync(request.Command.Type, request.Command.Identifier);

            var pathToFile = BuildFilePath("InitSignedXmlFilePath");
            // Fill InitSessionSignedRq file
            _xmlHelper.PrepareInitSessionXmlRequest(pathToFile, authorizationChallenge.Challenge, request.Command.Identifier);

            

            // Xades
            var certificate = _certificateHelper.GetCertificate(Environment.GetEnvironmentVariable("Thumbprint").ToUpper() ?? "",
                StoreLocation.LocalMachine) ?? throw new InvalidOperationException();

            var xadesSign = _xmlHelper.PrepareXadesFile(pathToFile, certificate);

            if (xadesSign != null)
            {
                var initSession = await _ksefMethods.InitSignedSessionAsync(xadesSign);
            }
        }

        private string BuildFilePath(string fileName)
        {
            if(string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentNullException(nameof(fileName));

            var sb = new StringBuilder();
            sb.Append(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName);
            sb.Append(_configuration.GetSection(fileName).Value);
            return sb.ToString();
        }
    }
}
