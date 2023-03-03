using System.Reflection;
using Microsoft.OpenApi.Models;

namespace CREL_BE.Extensions;

public static class SwaggerConfigExtension
{
    public static IServiceCollection AddSwaggerConfig(this IServiceCollection services, IConfiguration config)
    {
        services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "CREALESTATE API",
                    Version = "v1",
                    Description = "An API for CREALESTATE project",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Đỗ Thành Đat",
                         Email = "dat7124@gmail.com",
                         Url = new Uri("https://www.facebook.com/ghetquadia"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "CREALESTATE API LICX",
                        Url = new Uri("https://example.com/license"),
                    }
                });
                //Set the comments path for the Swagger JSON and UI.

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n\
                                    Enter 'Bearer' [space] and then your token in the text input below.
                                    \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                    {
                        {
                        new OpenApiSecurityScheme
                            {
                            Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                            },
                        new List<string>()
                        }
                    });
            });
        return services;
    }

}
