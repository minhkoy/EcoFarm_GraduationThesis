using EcoFarm.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoFarm.Domain.Entities
{
    [Table("PRODUCT_IMAGE")]
    public class ProductImage : BaseEntity
    {
        public string PRODUCT_ID { get; set; }
        public string DESCRIPTION { get; set; }
        public string IMAGE_URL { get; set; }
        [ForeignKey(nameof(PRODUCT_ID))]
        [InverseProperty(nameof(Product.ProductImages))]
        public virtual Product OwnedProduct { get; set; }
    }
}
