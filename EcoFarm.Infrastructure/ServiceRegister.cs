using EcoFarm.Application.Interfaces.Repositories;
using EcoFarm.Infrastructure.Repositories;
using EcoFarm.Infrastructure.Services.Implementations;
using EcoFarm.Infrastructure.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace EcoFarm.Infrastructure;

public static class ServiceRegister
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddTransient<ICloudinaryService, CloudinaryService>();
    }
}