using EcoFarm.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoFarm.Domain.Entities
{
    [Table("ACTIVITY_MEDIA")]
    public class ActivityMedia : BaseEntity
    {
        public string ACTIVITY_ID { get; set; }
        public string MEDIA_URL { get; set; }
        public string IMAGE_DESCRIPTION { get; set; }
        public string IMAGE_TYPE { get; set; }
        [ForeignKey(nameof(ACTIVITY_ID))]
        [InverseProperty(nameof(FarmingPackageActivity.ActivityMedias))]
        public virtual FarmingPackageActivity Activity { get; set; }

    }
}
