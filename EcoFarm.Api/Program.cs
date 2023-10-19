using EcoFarm.Application;
using EcoFarm.Application.Interfaces.Localization;
using EcoFarm.Application.Interfaces.Repositories;
using EcoFarm.Application.Localization.Services;
using EcoFarm.Domain.Common.Values.Constants;
using EcoFarm.Infrastructure.Repositories;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(EcoFarm.Application.AssemblyReference.Assembly));
builder.Services.AddControllers();
builder.Services.AddLocalizationService();
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var vi = Constants.Languages.Vi;
    var supportedCultures = new CultureInfo[]
    {
        new CultureInfo(Constants.Languages.En),
        new CultureInfo(vi),
    };
    //options.DefaultRequestCulture = new RequestCulture(culture: vi, uiCulture: vi);
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;

    //options.AddInitialRequestCultureProvider(new CustomRequestCultureProvider(async context =>
    //{
    //    return new ProviderCultureResult("vi");
    //}));
    options.RequestCultureProviders.Insert(0, new CustomRequestCultureProvider(context =>
    {
        var languages = context.Request.Headers["Accept-Language"].ToString();
        var currentLanguage = languages.Split(",").FirstOrDefault();
        var defaultLanguage = string.IsNullOrEmpty(currentLanguage) ? vi : currentLanguage;

        if (!defaultLanguage.Equals(vi) && !defaultLanguage.Equals(Constants.Languages.En))
        {
            defaultLanguage = vi;
        }

        return Task.FromResult(new ProviderCultureResult(defaultLanguage, defaultLanguage));
    }));
});
builder.Services.AddTransient<ILocalizeService, LocalizeService>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);
app.UseHttpsRedirection();

app.UseAuthorization();
//app.UseMiddleware<RequestLocalizationMiddleware>();
app.MapControllers();

app.Run();