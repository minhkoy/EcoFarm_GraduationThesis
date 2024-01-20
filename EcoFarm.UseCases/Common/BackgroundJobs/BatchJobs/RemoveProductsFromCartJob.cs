using EcoFarm.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenHandler.Interfaces;

namespace EcoFarm.UseCases.Common.BackgroundJobs.BatchJobs
{
    public class RemoveProductsFromCartJob
    {
        private readonly IUnitOfWork _unitOfWork;
        public RemoveProductsFromCartJob(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Run(string userId, List<string> productIds)
        {
            var cart = await _unitOfWork.ShoppingCarts
                .GetQueryable()
                .FirstOrDefaultAsync(x => string.Equals(x.USER_ID, userId));
            if (cart is null)
            {
                return;
            }
            var cartDetail = await _unitOfWork.CartDetails
                .GetQueryable()
                .Where(x => string.Equals(x.CART_ID, cart.ID) && productIds.Contains(x.PRODUCT_ID))
                .ToListAsync();
            if (cartDetail is null || cartDetail.Count < 1)
            {
                return;
            }
            _unitOfWork.CartDetails.ForceRemoveRange(cartDetail);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
