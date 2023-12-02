//using Ardalis.Result;
//using EcoFarm.Application.Common.Results;
using Ardalis.Result;
using EcoFarm.Application.Interfaces.Messagings;
using EcoFarm.Application.Interfaces.Repositories;
using EcoFarm.Domain.Entities;
using EcoFarm.UseCases.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenHandler.Interfaces;
using static EcoFarm.Domain.Common.Values.Enums.HelperEnums;

namespace EcoFarm.UseCases.Products.Create
{
    public class CreateProductCommand : ICommand<ProductDTO>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal? Weight { get; set; }
        public string Description { get; set; }
        public string PackageId { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        public decimal? PriceForRegistered { get; set; }
        public CurrencyType? Currency { get; set; }
    }

    internal class CreateProductHandler : ICommandHandler<CreateProductCommand, ProductDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;

        public CreateProductHandler(IUnitOfWork unitOfWork, IAuthService authService)
        {
            _unitOfWork = unitOfWork;
            _authService = authService;
        }

        public async Task<Result<ProductDTO>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var erpId = _authService.GetAccountEntityId();
            var erp = await _unitOfWork.SellerEnterprises.FindAsync(erpId);
            if (erp == null)
            {
                return Result<ProductDTO>.Forbidden();
            }
            var pkg = await _unitOfWork.FarmingPackages.FindAsync(request.PackageId);
            if (pkg == null)
            {
                return Result<ProductDTO>.NotFound("Không tìm thấy thông tin gói farming");
            }
            if (!pkg.IS_ACTIVE)
            {
                return Result<ProductDTO>.Error("Gói farming đã bị khóa");
            }

            var product = new Product
            {
                CODE = request.Code,
                NAME = request.Name,
                DESCRIPTION = request.Description,
                PACKAGE_ID = request.PackageId,
                QUANTITY = request.Quantity,
                SOLD = 0,
                PRICE = request.Price,
                PRICE_FOR_REGISTERED = request.PriceForRegistered,
                CURRENCY = request.Currency,
                WEIGHT = request.Weight ?? 0,
            };
            _unitOfWork.Products.Add(product);
            await _unitOfWork.SaveChangesAsync();
            return Result<ProductDTO>.Success(new ProductDTO
            {
                Id = product.ID,
                Code = product.CODE,
                Name = product.NAME,
                Description = product.DESCRIPTION,
                Weight = product.WEIGHT,
                CreatedTime = product.CREATED_TIME,
                PackageId = product.PACKAGE_ID,
                PackageCode = pkg.CODE,
                PackageName = pkg.NAME,
                Quantity = product.QUANTITY,
                Sold = product.SOLD,
                Price = product.PRICE,
                PriceForRegistered = product.PRICE_FOR_REGISTERED,
                Currency = product.CURRENCY,
            });
        }
    }
}
