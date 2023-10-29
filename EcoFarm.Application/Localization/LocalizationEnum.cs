using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoFarm.Application.Localization
{
    public enum LocalizationEnum
    {
        #region General entities
        Code = 0,
        Name,
        Status,
        Description,
        #endregion

        #region Validation input error
        MissingRequiredFields,
        MissingRequiredFieldsDetail,
        UserHasNotLoggedIn,
        IncorrectDetail,
        InvalidDetail,

        
        #endregion

        #region Validation data error
        
        #endregion

        #region General messages
        Successful,
        #endregion
    }
}