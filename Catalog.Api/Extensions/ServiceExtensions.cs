using System.Reflection;
using AuthService;
using EmailService.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Catalog.Api.Extensions;

public static class ServiceExtensions
{
    public static void ApplySwaggerSettings(this IServiceCollection service)
    {
        string XmlCommentsFilePathContentShare()
        {
            // For PlatformServices used NuGet package Microsoft.Extensions.PlatformAbstractions v1.1.0
            var basePath = PlatformServices.Default.Application.ApplicationBasePath;
            var fileName = typeof(Program).GetTypeInfo().Assembly.GetName().Name + ".xml";
            return Path.Combine(basePath, fileName);
        }
            
        service.AddSwaggerGen(c =>
        {
            c.IncludeXmlComments(XmlCommentsFilePathContentShare());
            c.SwaggerDoc("v1", new OpenApiInfo {Title = "RestaurantCatalog", Version = "v1"});
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Description = @"JWT Authorization header using the Bearer scheme. 
                      Enter 'Bearer' [space] and then your token in the text input below. Example: 'Bearer 12345abcdef'",
                Name = "Authorization",
                In = ParameterLocation.Header,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
        });
    }

    public static void UseCustomAuth(this IServiceCollection service)
    {
        service.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = AuthOptions.ISSUER,
                    ValidateAudience = true,
                    ValidAudience = AuthOptions.AUDIENCE,
                    ValidateLifetime = true,
                    IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                    ValidateIssuerSigningKey = true,
                };
            });
    }

    public static void ConfigurationsSetUp(this IServiceCollection service, IConfiguration configuration)
    {
        var mailSettingsSection = configuration.GetSection(MailSettings.SectionName);
        service.Configure<MailSettings>(mailSettingsSection);
        
        var rabbitMqSection = configuration.GetSection(RabbitMqSettings.SectionName);
        service.Configure<RabbitMqSettings>(rabbitMqSection);
    }
}
