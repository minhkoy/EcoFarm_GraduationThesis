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
        public static readonly int SaltLength = 24;
        //Timezone
        /// <summary>
        /// VN timezone
        /// </summary>
        public static readonly string Timezone_VN = "SE Asia Standard Time";
        public class SignalREvents
        {
            public static string ReceiveMessage = "ReceiveMessage";
            public static string SendMessage = "SendMessage";
            public static string Notification = "Notification";
            public static string NewConnection = "NewConnection";
        }
        public class AccountTypes
        {
            public const string SuperAdmin = "SuperAdmin";
            public const string Admin = "Admin";
            public const string Seller = "Seller";
            public const string Customer = "Customer";
            public static Dictionary<AccountType, string> dctAccountType = new()
            {
                { AccountType.SuperAdmin, SuperAdmin },
                { AccountType.Admin, Admin },
                { AccountType.Seller, Seller },
                { AccountType.Customer, Customer }
            };
        }

        public class PackageStatuses
        {
            //public static string OpeningForRegister = "Đang mở đăng ký";
            //public static string ClosedForRegister = "Đã đóng đăng ký";
            public static string NotStarted = "Chưa bắt đầu";
            public static string Started = "Đã bắt đầu";
            public static string Ended = "Đã kết thúc";
            public static Dictionary<PackageStatus, string> dctPackageStatus = new()
            {
                //{ PackageStatus.OpeningForRegister, OpeningForRegister },
                //{ PackageStatus.ClosedForRegister, ClosedForRegister },
                { PackageStatus.NotStarted, NotStarted },
                { PackageStatus.Started, Started },
                { PackageStatus.Ended, Ended }
            };
        }
        public class PackageRegisterStatuses
        {
            public static string OpenForRegistered = "Đang mở đăng ký";
            public static string ClosedForRegister = "Đã đóng đăng ký";
            public static Dictionary<PackageRegisterStatus, string> dctPackageRegisterStatus = new()
            {
                { PackageRegisterStatus.OpenForRegister, OpenForRegistered },
                { PackageRegisterStatus.ClosedForRegister, ClosedForRegister }
            };
        }
        public class Genders
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

        public class OrderStatuses
        {
            public static string WaitingSellerConfirm = "Chờ nhà cung cấp/ chủ trang trại xác nhận";
            public static string SellerConfirmed = "Nhà cung cấp/ chủ trang trại đã xác nhận";
            public static string RejectedBySeller = "Nhà cung cấp/ chủ trang trại đã từ chối";
            public static string Preparing = "Nhà cung cấp/ chủ trang trại đang chuẩn bị hàng";
            public static string Shipping = "Đang giao hàng";
            public static string Shipped = "Đã giao hàng";
            public static string Received = "Đã nhận được hàng";
            public static string CancelledByCustomer = "Đã hủy bởi khách hàng";
            public static Dictionary<OrderStatus, string> dctOrderStatus = new()
            {
                {OrderStatus.WaitingSellerConfirm, WaitingSellerConfirm},
                {OrderStatus.SellerConfirmed, SellerConfirmed},
                {OrderStatus.RejectedBySeller, RejectedBySeller},
                {OrderStatus.Preparing, Preparing },
                {OrderStatus.Shipping, Shipping},
                {OrderStatus.Shipped, Shipped},
                {OrderStatus.Received, Received},
                {OrderStatus.CancelledByCustomer, CancelledByCustomer}

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

        public class PaymentMethods
        {
            public static string Cash = "Tiền mặt";
            public static string BankTransfer = "Chuyển khoản";
            public static string CreditCard = "Thẻ tín dụng";
            public static Dictionary<OrderPaymentMethod, string> dctPaymentMethod = new()
            {
                {OrderPaymentMethod.Cash, Cash},
                {OrderPaymentMethod.BankTransfer, BankTransfer},
                {OrderPaymentMethod.CreditCard, CreditCard}
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