using Ardalis.Result;
using EcoFarm.Application.Interfaces.Messagings;
using EcoFarm.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoFarm.UseCases.Products.Delete
{
    public class DeleteProductCommand : ICommand<bool>
    {
        public string ProductId { get; set; }
        public DeleteProductCommand() { }
        public DeleteProductCommand(string productId)
        {
            ProductId = productId;
        }
    }

    internal class DeleteProductHandler : ICommandHandler<DeleteProductCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteProductHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<bool>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var id = request?.ProductId;
            //if (string.IsNullOrEmpty(id))
            //{
            //    return Result<bool>.Invalid();
            //}
            var product = await _unitOfWork.Products.FindAsync(id);
            if (product is null)
            {
                return Result<bool>.NotFound("Không tìm thấy sản phẩm");
            }
            if (!product.IS_ACTIVE)
            {
                return Result<bool>.Error("Sản phẩm đã bị khóa");
            }
            _unitOfWork.Products.Remove(product);
            await _unitOfWork.SaveChangesAsync();
            return Result<bool>.Success(true);
        }
    }
}
