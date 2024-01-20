using Ardalis.Result;
using EcoFarm.Application.Interfaces.Messagings;
using EcoFarm.UseCases.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoFarm.UseCases.ShoppingCarts.OrderCart
{
    public class OrderCartProductsCommand : ICommand<ShoppingCartDTO>
    {

    }

    internal class Handler : ICommandHandler<OrderCartProductsCommand, ShoppingCartDTO>
    {
        public Task<Result<ShoppingCartDTO>> Handle(OrderCartProductsCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
