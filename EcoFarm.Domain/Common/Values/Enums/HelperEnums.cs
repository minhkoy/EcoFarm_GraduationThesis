namespace EcoFarm.Domain.Common.Values.Enums;

public class HelperEnums
{
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
}