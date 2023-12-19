using Autofac;
using KsefInfrastructure.Command;

namespace KsefInfrastructure.CQRS
{
    public class RequestDispatcher : IRequestDispatcher
    {
        private readonly IComponentContext _context;

        public RequestDispatcher(IComponentContext context)
        {
            _context = context;
        }

        public async Task DispatchAsync<TRequest>(TRequest request) where TRequest : IRequest
        {
            if(request == null)
                throw new Exception($"Request {request} can not be null.");

            var handler = _context.Resolve<IRequestHandler<TRequest>>();
            await handler.HandleAsync(request);
        }

        public async Task<TResponse> DispatchAsync<TRequest, TResponse>(TRequest request)
            where TRequest : IRequest
            where TResponse : class
        {
            if (request == null)
                throw new Exception($"Request {request} can not be null.");

            var handler = _context.Resolve<IRequestHandler<TRequest, TResponse>>();
            return await handler.HandleAsync(request);
        }
    }
}
