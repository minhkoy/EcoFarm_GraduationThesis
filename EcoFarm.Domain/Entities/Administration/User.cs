using EcoFarm.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EcoFarm.Domain.Common.Values.Enums.HelperEnums;

namespace EcoFarm.Domain.Entities.Administration
{
    [Table("USER")]
    [Index(nameof(ACCOUNT_ID), IsUnique = true)]
    public class User : BaseNonExtendedEntity
    {
        public string NAME { get; set; }
        public string ACCOUNT_ID { get; set; }
        public DateTime? DATE_OF_BIRTH { get; set; }
        public string PHONE_NUMBER { get; set; }
        public GenderEnum? GENDER { get; set; }
        //public string BANK_ACCOUNT_NUMBER { get; set; }
        //public string BANK_ACCOUNT_NAME { get; set; }
        //public string BANK_NAME { get; set; }
        //public string BANK_BRANCH { get; set; }
        //public string BANK_IFSC_CODE { get; set; }
        //public string BANK_MICR_CODE { get; set; }
        [ForeignKey(nameof(ACCOUNT_ID))]
        [InverseProperty(nameof(Account.UserInfo))]
        public virtual Account AccountInfo { get; set; }
        [InverseProperty(nameof(UserAddress.UserInfo))]
        public virtual ICollection<UserAddress> Addresses { get; set; }
        [InverseProperty(nameof(UserActivityComment.UserInfo))]
        public virtual ICollection<UserActivityComment> UserActivityComments { get; set; }
        [InverseProperty(nameof(Order.UserInfo))]
        public virtual ICollection<Order> Orders { get; set; }
        [InverseProperty(nameof(UserPackageReview.UserInfo))]
        public virtual ICollection<UserPackageReview> UserReviews { get; set; }
        [InverseProperty(nameof(UserRegisterPackage.UserInfo))]
        public virtual ICollection<UserRegisterPackage> UserRegisterPackages { get; set; }
    }
}
