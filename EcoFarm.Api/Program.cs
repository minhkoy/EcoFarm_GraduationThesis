using EcoFarm.Api.Abstraction.Behaviors;
using EcoFarm.Api.Middlewares;
using EcoFarm.Application;
using EcoFarm.Application.Interfaces.Localization;
using EcoFarm.Application.Interfaces.Repositories;
using EcoFarm.Application.Localization.Services;
using EcoFarm.Domain.Common.Values.Constants;
using EcoFarm.Domain.Common.Values.Options;
using EcoFarm.Infrastructure.Contexts;
using EcoFarm.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Globalization;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(EcoFarm.Application.AssemblyReference.Assembly));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddControllers();
builder.Services.AddLocalizationService();
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var vi = EFX.Languages.Vi;
    var supportedCultures = new CultureInfo[]
    {
        new CultureInfo(EFX.Languages.En),
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

        if (!defaultLanguage.Equals(vi) && !defaultLanguage.Equals(EFX.Languages.En))
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
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtOption:Issuer"],
            ValidAudience = builder.Configuration["JwtOption:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtOption:Key"]))
        };
    });

builder.Services.AddSwaggerGen(s =>
{
    s.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "V1",
        Title = "JWT Test API",
        Description = "APIs for Eco Farm project"
    });
    s.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer. \r\n\r\n
                            Enter 'Bearer[SPACE]' and then your token in the textinput below. \r\n\r\n
                            Example: 'Bearer 123456defabc' ",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });
    var securityRequirement = new OpenApiSecurityRequirement
    {
        {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = JwtBearerDefaults.AuthenticationScheme,
            }
        },
        new string[] {}
        }
    };
    s.AddSecurityRequirement(securityRequirement);
});
builder.Services.AddDbContext<EcoContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnStr"), b => b.MigrationsAssembly("JWT.Domain"));
});
builder.Services.AddValidators();
builder.Services.AddSingleton<ErrorHandlingMiddleware>();
builder.Services.Configure<JwtOption>(builder.Configuration.GetSection(nameof(JwtOption)));
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
app.UseMiddleware<ErrorHandlingMiddleware>();
app.MapControllers();

app.Run();