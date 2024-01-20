using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoFarm.UseCases.DTOs
{
    public class ShoppingCartDTO
    {
        public string Id { get; set; }
        public bool? IsOrdered { get; set; }
        public int? TotalQuantity { get; set; }
        public decimal? TotalPrice { get; set; }
        public List<CartDetailDTO> Products { get; set; }
    }
}
