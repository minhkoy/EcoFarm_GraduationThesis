using EcoFarm.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoFarm.Domain.Entities
{

    [Table("FARMING_PACKAGE_ACTIVITY")]
    public class FarmingPackageActivity : BaseEntity
    {
        public string PACKAGE_ID { get; set; }
        public string DESCRIPTION { get; set; }
        [ForeignKey(nameof(PACKAGE_ID))]
        [InverseProperty(nameof(FarmingPackage.Activities))]
        public virtual FarmingPackage Package { get; set; }
        [InverseProperty(nameof(ActivityMedia.Activity))]
        public virtual ICollection<ActivityMedia> ActivityMedias { get; set; }
        [InverseProperty(nameof(UserActivityComment.Activity))]
        public virtual ICollection<UserActivityComment> UserActivityComments { get; set; }

    }
}
