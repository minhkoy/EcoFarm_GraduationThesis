using Ardalis.Result;
using EcoFarm.Application.Interfaces.Messagings;
using EcoFarm.Application.Interfaces.Repositories;
using EcoFarm.Domain.Common.Values.Constants;
using EcoFarm.UseCases.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenHandler.Interfaces;

namespace EcoFarm.UseCases.Products.Update
{
    public class UpdateProductCommand : ICommand<ProductDTO>
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } 
        public string PackageId { get; set; }
        public int? QuantityRemain { get; set; }
        public decimal? Price { get; set; }
        public decimal? PriceForRegistered { get; set; }
    }

    internal class Handler : ICommandHandler<UpdateProductCommand, ProductDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;
        public Handler(IUnitOfWork unitOfWork, IAuthService authService)
        {
            _unitOfWork = unitOfWork;
            _authService = authService;
        }
        public async Task<Result<ProductDTO>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            if (_authService.GetAccountTypeName() != EFX.AccountTypes.Seller)
                return Result.Forbidden();
            var product = await _unitOfWork.Products.FindAsync(request.Id);
            if (product == null)
                return Result.NotFound("Không tìm thấy sản phẩm cần cập nhật");
            if (!string.Equals(product.ENTERPRISE_ID, _authService.GetAccountEntityId()))
            {
                return Result.Forbidden();
            }
            if (!string.Equals(product.PACKAGE_ID, request.PackageId) && !string.IsNullOrEmpty(product.PACKAGE_ID))
            {
                return Result.Error("Không thể thay đổi thông tin gói farming liên quan!");
            }
            product.CODE = request.Code;
            product.NAME = request.Name;
            product.DESCRIPTION = request.Description;
            product.PACKAGE_ID = request.PackageId;
            product.QUANTITY = request.QuantityRemain + product.SOLD;
            product.PRICE = request.Price;
            product.PRICE_FOR_REGISTERED = request.PriceForRegistered;

            _unitOfWork.Products.Update(product);
            await _unitOfWork.SaveChangesAsync();

            return Result.SuccessWithMessage("Cập nhật thông tin sản phẩm thành công");
        }
    }
}
