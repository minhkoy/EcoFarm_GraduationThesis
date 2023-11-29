using Ardalis.Result;
using EcoFarm.UseCases.Accounts.Login;
using EcoFarm.UseCases.Accounts.Signup;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace EcoFarm.UseCases
{
    public static class ServiceRegister
    {
        public static void AddValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<LoginCommand>, LoginValidator>();
            services.AddScoped<IValidator<SignupAsUserCommand>, SignupAsUserValidator>();
        }
    }
}
