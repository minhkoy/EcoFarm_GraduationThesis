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
        public class Roles
        {
            public static string Admin = "Quản trị hệ thống";
            public static string Seller = "Tổ chức/ cá nhân cung cấp dịch vụ";
            public static string Customer = "Người tìm dịch vụ";
            public static Dictionary<RoleType, string> dctRoles = new()
            {
                { RoleType.Admin, Admin },
                { RoleType.Seller, Seller },
                { RoleType.Customer, Customer }
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