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
}