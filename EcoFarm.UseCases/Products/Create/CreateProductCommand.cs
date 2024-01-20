//using Ardalis.Result;
//using EcoFarm.Application.Common.Results;
using Ardalis.Result;
using EcoFarm.Application.Interfaces.Messagings;
using EcoFarm.Application.Interfaces.Repositories;
using EcoFarm.Domain.Entities;
using EcoFarm.Infrastructure.Services.Interfaces;
using EcoFarm.UseCases.DTOs;
using Microsoft.EntityFrameworkCore;
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
        public string Avatar { get; set; }
        public CurrencyType? Currency { get; set; }
    }

    internal class CreateProductHandler : ICommandHandler<CreateProductCommand, ProductDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;
        private readonly ICloudinaryService _cloudinaryService;

        public CreateProductHandler(IUnitOfWork unitOfWork, IAuthService authService,
            ICloudinaryService cloudinaryService)
        {
            _unitOfWork = unitOfWork;
            _authService = authService;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<Result<ProductDTO>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var erpId = _authService.GetAccountEntityId();
            var erp = await _unitOfWork.SellerEnterprises.FindAsync(erpId);
            if (erp == null)
            {
                return Result<ProductDTO>.Forbidden();
            }
            FarmingPackage pkg = null;
            if (!string.IsNullOrEmpty(request.PackageId))
            {
                pkg = await _unitOfWork.FarmingPackages.FindAsync(request.PackageId);
                if (pkg == null)
                {
                    return Result<ProductDTO>.NotFound("Không tìm thấy thông tin gói farming");
                }
                if (!pkg.IS_ACTIVE)
                {
                    return Result<ProductDTO>.Error("Gói farming đã bị khóa");
                }
            }

            var existedProduct = await _unitOfWork.Products
                .GetQueryable()
                .AnyAsync(x => string.Equals(x.ENTERPRISE_ID, erpId) && string.Equals(x.CODE, request.Code));
            if (existedProduct)
            {
                return Result.Error($"Đã tồn tại gói farming với mã {request.Code}");
            }
            var product = new Product
            {
                CODE = request.Code,
                NAME = request.Name,
                DESCRIPTION = request.Description,
                PACKAGE_ID = request.PackageId,
                ENTERPRISE_ID = _authService.GetAccountEntityId(),
                QUANTITY = request.Quantity,
                SOLD = 0,
                PRICE = request.Price,
                PRICE_FOR_REGISTERED = request.PriceForRegistered,
                CURRENCY = request.Currency,
                WEIGHT = request.Weight ?? 0,
            };
            _unitOfWork.Products.Add(product);

            var imageUrl = _cloudinaryService.UploadBase64Image(request.Avatar);
            if (!string.IsNullOrEmpty(imageUrl))
            {
                _unitOfWork.ProductMedias.Add(new ProductMedia
                {
                    PRODUCT_ID = product.ID,
                    MEDIA_TYPE = "img",
                    MEDIA_URL = imageUrl,
                });
            }
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
                PackageCode = pkg?.CODE,
                PackageName = pkg?.NAME,
                Quantity = product.QUANTITY,
                Sold = product.SOLD,
                Price = product.PRICE,
                PriceForRegistered = product.PRICE_FOR_REGISTERED,
                Currency = product.CURRENCY,
            });
        }
    }
}
