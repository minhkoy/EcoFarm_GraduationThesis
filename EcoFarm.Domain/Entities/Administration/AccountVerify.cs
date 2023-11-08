using EcoFarm.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EcoFarm.Domain.Common.Values.Enums.HelperEnums;

namespace EcoFarm.Domain.Entities.Administration
{
    [Table("ACCOUNT_VERIFY")]
    public class AccountVerify : BaseNonExtendedEntity
    {
        public string ACCOUNT_ID { get; set; }
        public string VERIFY_CODE { get; set; }
        public DateTime? VERIFY_REQUEST_TIME { get; set; }
        public DateTime? VERIFY_EXPIRE_TIME { get; set; }
        public bool IS_VERIFIED { get; set; } = false;
        public VerifyReason VERIFY_REASON { get; set; }

        [ForeignKey(nameof(ACCOUNT_ID))]
        [InverseProperty(nameof(Account.AccountVerifies))]
        public virtual Account RequestAccount { get; set; }
    }
}
