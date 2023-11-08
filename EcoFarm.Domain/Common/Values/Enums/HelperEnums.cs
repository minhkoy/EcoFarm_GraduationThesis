namespace EcoFarm.Domain.Common.Values.Enums;

public class HelperEnums
{
    //Administration
    public enum RoleType
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

    //Service Packages
    public enum ServicePackageType
    {
        Tourism = 1,
        Farming,
        PetCare,
        Others
    }

    public enum ServicePackageApprovalStatus
    {
        Pending = 1, //Waiting to be approved
        Accepted,
        Denied
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
}