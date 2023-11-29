using Ardalis.Result;
using EcoFarm.Application.Interfaces.Messagings;
using EcoFarm.Application.Interfaces.Repositories;
using EcoFarm.Domain.Entities;
using EcoFarm.UseCases.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoFarm.UseCases.Products.Get
{
    public class GetProductQuery : IQuerySingle<ProductDTO>
    {
        public string Id { get; set; }
        public string Code { get; set; }
    }

    internal class GetProductHandler : IQuerySingleHandler<GetProductQuery, ProductDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetProductHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<ProductDTO>> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var query = _unitOfWork.Products
                .GetQueryable();
            var product = new Product();
            if (request is null)
            {
                return Result<ProductDTO>.Invalid();
            }
            if (!string.IsNullOrEmpty(request.Id))
            {
                product = await query.FirstOrDefaultAsync(x => x.ID.Equals(request.Id));
            }
            else if (!string.IsNullOrEmpty(request.Code))
            {
                product = await query.FirstOrDefaultAsync(x => x.CODE.Equals(request.Code));
            }
            else
            {
                return Result<ProductDTO>.Invalid();
            }
            if (product is null || product == default)
            {
                return Result<ProductDTO>.NotFound();
            }
            ProductDTO dto = new ProductDTO
            {
                Id = product.ID,
                Code = product.CODE,
                Name = product.NAME,
                Description = product.DESCRIPTION,
                PackageId = product.PACKAGE_ID,
                Quantity = product.QUANTITY,
                Sold = product.SOLD,
                Price = product.PRICE,
                PriceForRegistered = product.PRICE_FOR_REGISTERED,
                Currency = product.CURRENCY,
                CreatedTime = product.CREATED_TIME,
            };
            var pkg = await _unitOfWork.FarmingPackages.FindAsync(product.PACKAGE_ID);
            if (pkg is not null)
            {
                dto.PackageCode = pkg.CODE;
                dto.PackageName = pkg.NAME;
            }
            return Result<ProductDTO>.Success(dto);
        }
    }
}
