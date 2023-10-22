//using EcoFarm.Application.Interfaces.Localization;

using EcoFarm.Application.Interfaces.Repositories;
using EcoFarm.Application.Localization.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System.Globalization;

namespace EcoFarm.Application;

public static class ServiceRegister
{
    public static void AddLocalizationService(this IServiceCollection services)
    {
        services.AddLocalization(options => { options.ResourcesPath = "Localization/Resources"; });
        //services.AddTransient<ILocalizeService, LocalizeService>();
        //services.AddSingleton<LocalizeService>();
        //services.Configure<RequestLocalizationOptions>
    }
}