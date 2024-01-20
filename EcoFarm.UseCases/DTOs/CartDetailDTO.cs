﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoFarm.UseCases.DTOs
{
    public class CartDetailDTO
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public decimal? ProductPrice { get; set; }
        public int? Quantity { get; set; }

    }
}
