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
        public string ENTERPRISE_ID { get; set; }
        public int? TYPE { get; set; }
        public int? QUANTITY { get; set; }

    }
}
