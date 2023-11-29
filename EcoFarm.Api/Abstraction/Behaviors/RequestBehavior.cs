using MediatR;
using System.Text.Json;

namespace EcoFarm.Api.Abstraction.Behaviors
{
    public class RequestBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<TRequest> _logger;
        public RequestBehavior(ILogger<TRequest> logger)
        {
            _logger = logger;
        }
        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var json = JsonSerializer.Serialize(request);
            _logger.LogInformation("Dữ liệu gửi api: {req}", json);
            return next();
        }
    }
}
