using EcoFarm.Domain.Common;
using EcoFarm.Domain.Entities.Administration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoFarm.Domain.Entities
{

    [Table("USER_PACKAGE_REVIEW")]
    public class UserPackageReview : BaseNonExtendedEntity
    {
        public string USER_ID { get; set; }
        public string PACKAGE_ID { get; set; }
        public string COMMENT { get; set; }
        public string ANSWER { get; set; }
        public int? RATING { get; set; }
        [ForeignKey(nameof(USER_ID))]
        [InverseProperty(nameof(User.UserReviews))]
        public virtual User UserInfo { get; set; }
        [ForeignKey(nameof(PACKAGE_ID))]
        [InverseProperty(nameof(FarmingPackage.UserReviews))]
        public virtual FarmingPackage Package { get; set; }
    }
}
