using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoFarm.Domain.Common.Values.Constants
{
    public static class Constants
    {
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