using KsefClient.ClientHttp;
using KsefInfrastructure.CQRS;
using KsefInfrastructure.Validation;

namespace KsefInfrastructure.Session
{
    public class CreateSessionInvocation : IRequestHandler<CreateSessionRequest, CreateSessionResponse>
    {
        private readonly IAuthChallenge _authChallenge;

        public CreateSessionInvocation(IAuthChallenge authChallenge)
        {
            _authChallenge = authChallenge;
        }

        public async ValueTask<CreateSessionResponse> HandleAsync(CreateSessionRequest request, CancellationToken cancellationToken = default)
        {
            Fail.IfNull(request.Command.NIP);
            Fail.IfNull(request.Command.Type);

            var ksefRequest = await _authChallenge.GetAuthorisationChallengeAsync(request.Command.Type, request.Command.NIP);

            var response = new CreateSessionResponse
            {
                Challenge = ksefRequest.Challenge,
                Timestamp = ksefRequest.Timestamp
            };

            return response;
        }
    }
}
