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
        public string SellerEnterpriseId { get; set; }
        public string SellerEnterpriseCode { get; set; }
        public string SellerEnterpriseName { get; set; }
        public string Description { get; set; }
        public DateTime? EstimatedStartTime { get; set; }
        public DateTime? EstimatedEndTime { get; set; }
        public DateTime? CloseRegisterTime { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public decimal Price { get; set; }
        public int? QuantityStart { get; set; }
        public int? QuantityRegistered { get; set; }
        public int? QuantityRemain { get; set; }    
        public string RejectReason { get; set; }
        public ServicePackageApprovalStatus? ServicePackageApprovalStatus { get; set; }
        public string ServicePackageApprovalStatusName
        {
            get
            {
                return string.Empty;
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

        public class RegisteredUser
        {
            public string AccountId { get; set; }
            public string Username { get; set; }
            public string Name { get; set; }
            public DateTime? RegisteredTime { get; set; }
        }
    }
}
