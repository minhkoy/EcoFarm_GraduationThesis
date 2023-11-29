namespace EcoFarm.Domain.Common.Values.Enums;

public class HelperEnums
{
    //User
    public enum GenderEnum
    {
        Male = 0,
        Female,
        Other
    }
    //Administration
    public enum AccountType
    {
        SuperAdmin = 0,
        Admin = 1,
        Seller,
        Customer
    }

    public enum VerifyReason
    {
        ForgotPassword = 0,
        Register,
        ChangeEmail,
        ChangePassword,
        ChangePhone,
    }
    //Notification
    public enum NotificationObjectType
    {
        Account = 0,
        Package,
        Product,
        Order,
        PackageReview,
        ActivityComment,
        Others
    }
    public enum NotificationActionType
    {
        Create = 0,
        Update,
        Delete,
        Approve,
        Reject,
        Report,
        Others
    }
    //Service Packages
    public enum FarmingPackageType
    {
        Tourism = 1,
        Farming,
        PetCare,
        Multiple,
        Others
    }

    public enum ServicePackageApprovalStatus
    {
        Pending = 1, //Waiting to be approved
        Approved,
        Rejected
    }

    public enum OrderStatus 
    {
        WaitingSellerConfirm = 1,
        SellerConfirmed,
        InProgress,
        Completed,
        Shipping,
        Shipped
    }

    public enum OrderPaymentStatus
    {
        Unpaid = 1,
        Paid
    }

    public enum OrderPaymentMethod
    {
        Cash = 1,
        BankTransfer,
        CreditCard
    }

    public enum ServiceType
    {
        ServicePackage = 0,
        Service = 1,
        SellingProduct = 2
    }

    public enum ResultTypes
    {
        Ok,
        Redirect,
        BadRequest,
        Unexpected,
        Unauthorized,
        Forbidden,
        NotFound,
        InternalServerError
    }

    public enum EventType
    {
        NotifyFarmingEvent = 1,
        UserOnline,

    }

    public enum CurrencyType
    {
        VND = 0,
        USD
    }
}