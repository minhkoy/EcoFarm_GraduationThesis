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
    [Table("NOTIFICATION")]
    public class Notification : BaseNonExtendedEntity
    {
        public string FROM_ACCOUNT_ID { get; set; }
        public AccountType? FROM_ACCOUNT_TYPE { get; set; }
        public string CONTENT { get; set; }
        public NotificationObjectType? OBJECT_TYPE { get; set; }
        public NotificationActionType? ACTION_TYPE { get; set; }

        //Extended (XXX)
        [NotMapped]
        public string ObjectTypeName { get; set; }
        [NotMapped]
        public string ActionTypeName { get; set; }

        [ForeignKey(nameof(FROM_ACCOUNT_ID))]
        [InverseProperty(nameof(Account.FromNotifications))]
        public virtual Account FromAccount { get; set; }

        [InverseProperty(nameof(NotificationAccount.NotificationInfo))]
        public virtual ICollection<NotificationAccount> NotificationUsers { get; set; }

        //[ForeignKey(nameof(TO_ACCOUNT_ID))]
        //[InverseProperty(nameof(Account.ToNotifications))]
        //public virtual Account ToAccount { get; set; }
    }
}
