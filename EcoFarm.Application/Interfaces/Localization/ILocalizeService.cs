using EcoFarm.Application.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoFarm.Application.Interfaces.Localization
{
    public interface ILocalizeService
    {
        string GetMessage(LocalizationEnum localizationEnum, string culture);
        string GetParameterizedMessage(LocalizationEnum localizationEnum, params object[] args);
    }
}

