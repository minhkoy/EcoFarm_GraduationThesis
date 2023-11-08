//using EcoFarm.Application.Interfaces.Localization;

using EcoFarm.Application.Features.Administration.AuthenticationFeatures.Commands.Login;
using EcoFarm.Application.Features.Tasks.ServicePackageFeatures.Commands.Create;
using EcoFarm.Application.Interfaces.Repositories;
using EcoFarm.Application.Localization.Services;
using FluentValidation;
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

    public static void AddValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<CreateServiceCommand>, CreateServiceValidator>();
        services.AddScoped<IValidator<LoginCommand>, LoginValidator>();
    }
}