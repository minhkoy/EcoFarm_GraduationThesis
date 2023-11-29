using EcoFarm.Application.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoFarm.Application.Interfaces.Messagings_Prev
{
    public interface ICommand<TResponse> : IRequest<Result<TResponse>>
    {

    }
}
