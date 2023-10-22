using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoFarm.Application.Localization
{
    public enum LocalizationEnum
    {
        MissingRequiredFields = 0,
        MissingRequiredFieldsDetail,
        InvalidDataFieldDetail,
        UserHasNotLoggedIn,
        UsernameOrPasswordEmpty
    }
}