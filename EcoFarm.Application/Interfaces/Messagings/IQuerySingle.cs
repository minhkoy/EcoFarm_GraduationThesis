using Ardalis.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoFarm.Application.Interfaces.Messagings
{
    public interface IQuerySingle<TResponse> : IRequest<Result<TResponse>>
    {
    }
}
