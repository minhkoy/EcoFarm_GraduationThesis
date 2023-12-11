using EcoFarm.Api.Abstraction.Behaviors;
using EcoFarm.Api.Hubs;
using EcoFarm.Api.Middlewares;
using EcoFarm.Application;
using EcoFarm.Application.Interfaces.Localization;
using EcoFarm.Application.Interfaces.Repositories;
using EcoFarm.Application.Localization.Services;
using EcoFarm.Domain.Common.Values.Constants;
using EcoFarm.Domain.Common.Values.Options;
using EcoFarm.Infrastructure.Contexts;
using EcoFarm.Infrastructure.Repositories;
using EcoFarm.UseCases;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Globalization;
using System.Reflection;
using System.Text;
using TokenHandler.Interfaces;
using TokenHandler.Models;
using TokenHandler.Services;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSignalR(o =>
{
    o.EnableDetailedErrors = true;
});
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(EcoFarm.UseCases.AssemblyReference).Assembly);
    //cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
    //cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyCorsPolicy", policy =>
    policy.AllowAnyHeader()
    .AllowAnyMethod()
    .AllowAnyOrigin());
    //.WithExposedHeaders("*"));
});
builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestBehavior<,>));
builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddValidatorsFromAssembly(EcoFarm.UseCases.AssemblyReference.Assembly, includeInternalTypes: true);
builder.Services.AddControllers();

//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//    .AddCookie(options =>
//    {
//        options.LoginPath = "/api/Account/Login";
//        options.AccessDeniedPath = "/api/Account/AccessDenied";
//    });
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
builder.Services.AddTransient<IAuthService, AuthService>();
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
        Title = "EcoFarm API",
        Description = "APIs cho EcoFarm project"
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

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    s.IncludeXmlComments(xmlPath);

});
builder.Services.AddDbContext<EcoContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("AzureConnStr"),
        b =>
        {
            b.MigrationsAssembly("EcoFarm.Infrastructure");
            b.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
        });
    //options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres"));
});
//builder.Services.AddValidators();
builder.Services.AddSingleton<ErrorHandlingMiddleware>();
builder.Services.Configure<JwtOption>(builder.Configuration.GetSection(nameof(JwtOption)));
builder.Services.Configure<JwtOptionConfig>(builder.Configuration.GetSection(nameof(JwtOptionConfig)));
builder.Services.Configure<CloudinaryAccountOption>(builder.Configuration.GetSection(nameof(CloudinaryAccountOption)));
builder.Services.AddHttpContextAccessor();
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUI();
}

app.UseCors("AllowAnyCorsPolicy");
app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);
app.UseHttpsRedirection();

app.UseAuthorization();
//app.UseMiddleware<RequestLocalizationMiddleware>();
//app.UseMiddleware<AuthenticationMiddleware>();
app.UseMiddleware<ErrorHandlingMiddleware>();
app.MapControllers();
app.MapHub<NotificationHub>("/notification");
app.MapHub<UserConnectionHub>("/user-connection");
app.MapHub<ChatHub>("/chat");

app.Run();