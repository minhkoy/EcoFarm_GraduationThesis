using EcoFarm.Domain.Common.Values.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EcoFarm.Domain.Common.Values.Enums.HelperEnums;

namespace EcoFarm.UseCases.DTOs
{
    public class FarmingPackageDTO
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedTime { get; set; }
        public string CreatedBy { get; set; }
        public string SellerEnterpriseId { get; set; }
        //public string SellerEnterpriseCode { get; set; }
        public string SellerEnterpriseName { get; set; }
        public EnterpriseDTO Enterprise { get; set; }
        public string Description { get; set; }
        public DateTime? EstimatedStartTime { get; set; }
        public DateTime? EstimatedEndTime { get; set; }
        public DateTime? CloseRegisterTime { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public decimal? Price { get; set; }
        public CurrencyType? Currency { get; set; }
        public string CurrencyName { get => Currency.HasValue ? Currency.Value.ToString() : string.Empty; }
        public int? QuantityStart { get; set; }
        public int? QuantityRegistered { get; set; }
        public int? QuantityRemain { get; set; }    
        public string RejectReason { get; set; }
        public ServicePackageApprovalStatus? ServicePackageApprovalStatus { get; set; }
        public string ServicePackageApprovalStatusName
        {
            get
            {
                if (!ServicePackageApprovalStatus.HasValue) return string.Empty;
                return EFX.PackageApprovalStatus.dctServicePackageApprovalStatus.GetValueOrDefault(ServicePackageApprovalStatus.Value);
            }
        }
        public FarmingPackageType? PackageType { get; set; }
        public string PackageTypeName
        {
            get
            {
                if (!PackageType.HasValue) return string.Empty;
                return EFX.FarmingPackageTypes.dctFarmingPackageType.GetValueOrDefault(PackageType.Value);
            }
        }
        public string ServiceTypeName { get; set; }
        public List<RegisteredUser> RegisteredUsers { get; set; } = new();
        public int? NumbersOfRating { get; set; }

        public decimal? AverageRating { get; set; }

        public bool IsRegisteredByCurrentUser { get; set; }

        public class RegisteredUser : UserDTO
        {
            public DateTime? RegisteredTime { get; set; }
        }
    }
}
