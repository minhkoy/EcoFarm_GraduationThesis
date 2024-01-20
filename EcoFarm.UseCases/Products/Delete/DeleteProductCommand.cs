using Ardalis.Result;
using EcoFarm.Application.Interfaces.Messagings;
using EcoFarm.Application.Interfaces.Repositories;
using EcoFarm.Domain.Common.Values.Constants;
using EcoFarm.Domain.Common.Values.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenHandler.Interfaces;
using static EcoFarm.Domain.Common.Values.Enums.HelperEnums;

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
        private readonly IAuthService _authService;
        public DeleteProductHandler(IUnitOfWork unitOfWork, IAuthService authService)
        {
            _unitOfWork = unitOfWork;
            _authService = authService;
        }
        public async Task<Result<bool>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var accountType = _authService.GetAccountTypeName();
            if (string.IsNullOrEmpty(accountType))
            {
                return Result<bool>.Unauthorized();
            }
            var type = EFX.AccountTypes.dctAccountType.FirstOrDefault(x => x.Value.Equals(accountType));
            if (type.Key != HelperEnums.AccountType.Seller)
            {
                return Result<bool>.Forbidden();
            }

            var id = request?.ProductId;
            var product = await _unitOfWork.Products.FindAsync(id);
            if (product is null)
            {
                return Result<bool>.NotFound("Không tìm thấy sản phẩm");
            }
            if (!product.IS_ACTIVE)
            {
                return Result<bool>.Error("Sản phẩm đã bị khóa");
            }

            if (!string.IsNullOrEmpty(product.PACKAGE_ID))
            {
                var package = await _unitOfWork.FarmingPackages.FindAsync(product.PACKAGE_ID);
                if (package is null)
                {
                    return Result<bool>.Error("Thông tin gói farming không chính xác");
                }
                if (!package.IS_ACTIVE)
                {
                    return Result<bool>.Error("Gói farming tạm thời bị khóa. Vui lòng thử lại sau");
                }
                if (!package.STATUS.Equals(ServicePackageApprovalStatus.Pending))
                {
                    return Result<bool>.Error("Gói farming đã bị khóa. Vui lòng thử lại sau");
                }
            }
            if (product.SOLD > 0)
            {
                return Result<bool>.Error("Sản phẩm đã được bán. Không thể xóa sản phẩm này");
            }
            _unitOfWork.Products.Remove(product);
            await _unitOfWork.SaveChangesAsync();
            return Result<bool>.Success(true);
        }
    }
}
