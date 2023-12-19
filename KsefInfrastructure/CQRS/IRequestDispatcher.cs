using KsefInfrastructure.Command;

namespace KsefInfrastructure.CQRS
{
    public interface IRequestDispatcher
    {
        Task DispatchAsync<TRequest>(TRequest command) where TRequest : IRequest;

        Task<TResponse> DispatchAsync<TRequest, TResponse>(TRequest command)
            where TRequest : IRequest
            where TResponse : class;
    }
}
