using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EcoFarm.Domain.Common.Values.Enums.HelperEnums;

namespace EcoFarm.Domain.Common.Values.Constants
{
    public static class EFX
    {
        public static int SaltLength = 24; 
        public class AccountTypes
        {
            public static string Admin = "Quản trị hệ thống";
            public static string Seller = "Tổ chức/ cá nhân cung cấp dịch vụ";
            public static string Customer = "Người tìm dịch vụ";
            public static Dictionary<AccountType, string> dctAccountType = new()
            {
                { AccountType.Admin, Admin },
                { AccountType.Seller, Seller },
                { AccountType.Customer, Customer }
            };
        }

        public class GenderEnums
        {
            public static string Male = "Nam";
            public static string Female = "Nữ";
            public static string Other = "Khác";
            public static Dictionary<GenderEnum, string> dctGenderEnum = new()
            {
                { GenderEnum.Male, Male },
                { GenderEnum.Female, Female },
                { GenderEnum.Other, Other }
            };
        }

        public class ServiceTypes
        {
            public static string ServicePackage = "Gói dịch vụ";
            public static string Service = "Dịch vụ";
            public static string SellingProduct = "Bán sản phẩm";
            public static Dictionary<ServiceType, string> dctServiceType = new()
            {
                {ServiceType.ServicePackage, ServicePackage},
                {ServiceType.Service, Service},
                {ServiceType.SellingProduct, SellingProduct}
            };
        }

        public class FarmingPackageTypes
        {
            public static string Farming = "Trồng trọt";
            public static string PetCare = "Chăn nuôi";
            public static string Tourism = "Tham quan nông trại";
            public static string Multiple = "Kết hợp";
            public static string Others = "Khác";
            public static Dictionary<FarmingPackageType, string> dctFarmingPackageType = new()
            {
                {FarmingPackageType.Farming, Farming},
                {FarmingPackageType.PetCare, PetCare},
                {FarmingPackageType.Tourism, Tourism},
                {FarmingPackageType.Multiple, Multiple},
                {FarmingPackageType.Others, Others}
            };
        }

        public class PackageApprovalStatus
        {
            public static string Pending = "Chờ duyệt";
            public static string Approved = "Đã duyệt";
            public static string Denied = "Từ chối";
            public static Dictionary<ServicePackageApprovalStatus, string> dctServicePackageApprovalStatus = new()
            {
                {ServicePackageApprovalStatus.Pending, Pending},
                {ServicePackageApprovalStatus.Approved, Approved},
                {ServicePackageApprovalStatus.Rejected, Denied}
            };
        }
        #region PageSizes
        public static int DefaultPageSize = 10;
        #endregion
        public class ValidationErrorNames
        {
            public static string MissingRequiredInformation = "Argument error";
        }

        public class ValidationErrorMsgs
        {
            public static string MissingRequiredInformation = "Missing required information";
            public static string MissingRequiredInformationDetail = "{0} field is required";
        }

        public class Languages
        {
            public static string En = "en";
            public static string Vi = "vi";
        }
    }
}