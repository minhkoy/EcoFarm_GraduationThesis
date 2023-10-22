//using EcoFarm.Application.Interfaces.Localization;

using EcoFarm.Application.Interfaces.Localization;
using EcoFarm.Application.Localization.Resources;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EcoFarm.Application.Localization.Services
{
    public class LocalizeService : ILocalizeService
    {
        private readonly IStringLocalizer _localizer = null!;

        public LocalizeService(IStringLocalizerFactory factory)
        {
            var type = typeof(Resource);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            _localizer = factory.Create("Resource", assemblyName.Name);
        }

        public string GetMessage(LocalizationEnum localizationEnum)
        {
            //var specifiedCulture = new CultureInfo(culture);
            //CultureInfo.CurrentCulture = specifiedCulture;
            //CultureInfo.CurrentUICulture = specifiedCulture;
            var cul = CultureInfo.CurrentCulture;
            var rs = _localizer[localizationEnum.ToString()];
            return _localizer[localizationEnum.ToString()];
        }

        public string GetParameterizedMessage(LocalizationEnum localizationEnum, params object[] args)
        {
            return string.Format(_localizer[localizationEnum.ToString()], args);
        }
    }
}