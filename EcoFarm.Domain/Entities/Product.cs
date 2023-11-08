using EcoFarm.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoFarm.Domain.Entities
{
    [Table("PRODUCT")]
    public class Product : BaseEntity
    {
        public string DESCRIPTION { get; set; }
        public string SERVICE_ID { get; set; }
        public int? TYPE { get; set; }
        public int? QUANTITY { get; set; }
        public int? SOLD { get; set; }

        [NotMapped]
        public int? CURRENT_QUANTITY { get => QUANTITY - SOLD; }

        [ForeignKey(nameof(SERVICE_ID))]
        [InverseProperty(nameof(ServicePackage.Products))]
        public virtual ServicePackage Service { get; set; }
        [InverseProperty(nameof(ProductImage.OwnedProduct))]
        public virtual ICollection<ProductImage> ProductImages { get; set; }
    }
}
