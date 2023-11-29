using EcoFarm.Domain.Common;
using EcoFarm.Domain.Entities.Administration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EcoFarm.Domain.Common.Values.Enums.HelperEnums;

namespace EcoFarm.Domain.Entities
{
    [Table("USER_REGISTER_PACKAGE")]
    public class UserRegisterPackage : BaseNonExtendedEntity
    {
        public string USER_ID { get; set; }
        public string PACKAGE_ID { get; set; }
        public decimal? PRICE { get; set; }
        public CurrencyType? CURRENCY { get; set; }
        public DateTime? REGISTER_TIME { get; set; }

        [ForeignKey(nameof(USER_ID))]
        [InverseProperty(nameof(User.UserRegisterPackages))]
        public virtual User UserInfo { get; set; }
        [ForeignKey(nameof(PACKAGE_ID))]
        [InverseProperty(nameof(FarmingPackage.UserRegisterPackages))]
        public virtual FarmingPackage PackageInfo { get; set; }
    }
}
