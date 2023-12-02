using EcoFarm.Application.Interfaces.Messagings;
using EcoFarm.UseCases.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoFarm.UseCases.Products.Update
{
    public class UpdateProductCommand : ICommand<ProductDTO>
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } 
        public string PackageId { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        public decimal? PriceForRegistered { get; set; }
    }


}
