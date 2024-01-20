using System.ComponentModel.DataAnnotations.Schema;
using EcoFarm.Domain.Common;
using EcoFarm.Domain.Common.Values.Enums;
using static EcoFarm.Domain.Common.Values.Enums.HelperEnums;

namespace EcoFarm.Domain.Entities;

[Table("FARMING_PACKAGE")]
public class FarmingPackage : BaseEntity
{
    public string DESCRIPTION { get; set; }
    public string SELLER_ENTERPRISE_ID { get; set; }
    public DateTime? ESTIMATED_START_TIME { get; set; }
    public DateTime? ESTIMATED_END_TIME { get; set; }
    public DateTime? CLOSE_REGISTER_TIME { get; set; }
    /// <summary>
    /// Auto close register when reach max quantity
    /// </summary>
    public bool IS_AUTO_CLOSE_REGISTER { get; set; } = false;
    public DateTime? START_TIME { get; set; }
    public DateTime? END_TIME { get; set; }
    public int? QUANTITY_START { get; set; }
    public int QUANTITY_REGISTERED { get; set; } = 0;
    public decimal PRICE { get; set; } = 0;
    public CurrencyType CURRENCY { get; set; } = CurrencyType.VND;
    //Temporary left null, may be used for discounting the service (later if needed)
    //public decimal? DISCOUNT_PRICE { get; set; }
    //public DateTime? DISCOUNT_START { get; set; }
    //public DateTime? DISCOUNT_END { get; set; }
    public ServicePackageApprovalStatus STATUS { get; set; } = ServicePackageApprovalStatus.Approved;
    public string APPROVE_OR_REJECT_BY { get; set; }
    public DateTime? APPROVE_OR_REJECT_TIME { get; set; }
    public FarmingPackageType PACKAGE_TYPE { get; set; }
    public string REJECT_REASON { get; set; }
    public long TOTAL_RATING_POINTS { get; set; } = 0;
    public int NUMBERS_OF_RATING { get; set; } = 0;

    public string AVATAR_URL { get; set; }

    //Extension properties
    [NotMapped] public decimal AverageRating => NUMBERS_OF_RATING == 0 ? 0 : Math.Round(TOTAL_RATING_POINTS * 1.00M / NUMBERS_OF_RATING, 2);
    [NotMapped] public int QuantityRemain => !QUANTITY_START.HasValue ? 0 : QUANTITY_START.Value - QUANTITY_REGISTERED;
    //Inverse properties
    [ForeignKey(nameof(SELLER_ENTERPRISE_ID))]
    [InverseProperty(nameof(SellerEnterprise.EnterpiseServicePackages))]
    public virtual SellerEnterprise Enterprise { get; set; }
    [InverseProperty(nameof(FarmingPackageActivity.Package))]
    public virtual ICollection<FarmingPackageActivity> Activities { get; set; }
    [InverseProperty(nameof(Product.Package))]
    public virtual Product ProductInfo { get; set; }
    //[InverseProperty(nameof(CartDetail.Package))]
    //public virtual ICollection<CartDetail> CartDetails { get; set; }
    [InverseProperty(nameof(PackageMedia.Package))]
    public virtual ICollection<PackageMedia> ServiceImages { get; set; }
    [InverseProperty(nameof(UserPackageReview.Package))]
    public virtual ICollection<UserPackageReview> UserReviews { get; set; }
    [InverseProperty(nameof(UserRegisterPackage.PackageInfo))]
    public virtual ICollection<UserRegisterPackage> UserRegisterPackages { get; set; }
}