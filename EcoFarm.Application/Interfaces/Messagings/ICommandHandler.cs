using Ardalis.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace EcoFarm.Application.Interfaces.Messagings
{
    public interface ICommandHandler<in TRequest, TResponse> : IRequestHandler<TRequest, Result<TResponse>>
        where TRequest : ICommand<TResponse>, new()
    {
    }

    //public abstract class ICommandHandler<TRequest, TResponse> : IRequestHandler<TRequest, Result<TResponse>>
    //    where TRequest : ICommand<TResponse>, new()
    //{
    //    protected readonly ILogger<ICommandHandler<TRequest, TResponse>> logger;
    //    public ICommandHandler(ILogger<ICommandHandler<TRequest, TResponse>> logger)
    //    {
    //        this.logger = logger;
    //    }

    //    public abstract Task<Result<TResponse>> Handle(TRequest request, CancellationToken cancellationToken);
    //}
}
