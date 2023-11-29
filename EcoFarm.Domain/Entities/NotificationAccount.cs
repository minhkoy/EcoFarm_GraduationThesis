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
    [Table("NOTIFICATION_ACCOUNT")]
    public class NotificationAccount : BaseNonExtendedEntity
    {
        public string TO_ACCOUNT_ID { get; set; }
        public AccountType? TO_ACCOUNT_TYPE { get; set; }
        public string NOTIFICATION_ID { get; set; }
        public bool IS_READ { get; set; } = false;

        [ForeignKey(nameof(NOTIFICATION_ID))]
        [InverseProperty(nameof(Notification.NotificationUsers))]
        public virtual Notification NotificationInfo { get; set; }

        [ForeignKey(nameof(TO_ACCOUNT_ID))]
        [InverseProperty(nameof(Account.NotificationUsers))]
        public virtual Account UserInfo { get; set;}
    }
}
