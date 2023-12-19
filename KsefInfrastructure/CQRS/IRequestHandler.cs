using KsefInfrastructure.Command;

namespace KsefInfrastructure.CQRS
{
    public interface IRequestHandler<TRequest> where TRequest : IRequest
    {
        ValueTask HandleAsync(TRequest request, CancellationToken cancellationToken = default);
    }

    public interface IRequestHandler<TRequest, TResponse> where TRequest : IRequest
    {
        ValueTask<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken = default);
    }
}
