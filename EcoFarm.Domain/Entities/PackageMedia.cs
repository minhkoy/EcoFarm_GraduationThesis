using EcoFarm.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoFarm.Domain.Entities
{
    [Table("PACKAGE_MEDIA")]
    public class PackageMedia : BaseEntity
    {
        public string PACKAGE_ID { get; set; }
        public string DESCRIPTION { get; set; }
        public string MEDIA_TYPE { get; set; }
        public string MEDIA_URL { get; set; }

        [ForeignKey(nameof(PACKAGE_ID))]
        [InverseProperty(nameof(FarmingPackage.ServiceImages))]
        public virtual FarmingPackage Package { get; set; }
    }
}