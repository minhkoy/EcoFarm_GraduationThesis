using EcoFarm.Domain.Common;
using EcoFarm.Domain.Entities.Administration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoFarm.Domain.Entities
{
    [Table("USER_ACTIVITY_COMMENT")]
    [Index(nameof(ACTIVITY_ID), nameof(USER_ID), IsUnique = true)]
    public class UserActivityComment : BaseNonExtendedEntity
    {
        public string ACTIVITY_ID { get; set; }
        public string USER_ID { get; set; }
        public string COMMENT { get; set; }
        /// <summary>
        /// Initially used to check if the comment is liked by the user. Support more reactions soon.
        /// </summary>
        public bool IS_LIKE { get; set; } = false;
        [ForeignKey(nameof(ACTIVITY_ID))]
        [InverseProperty(nameof(FarmingPackageActivity.UserActivityComments))]
        public virtual FarmingPackageActivity Activity { get; set; }
        [ForeignKey(nameof(USER_ID))]
        [InverseProperty(nameof(User.UserActivityComments))]
        public virtual User UserInfo { get; set; }
    }
}
