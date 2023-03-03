using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace CREL_BE.Extensions;
public static class IdentityServiceExtensions
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"])),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });

        // services.AddAuthorization(opt =>
        // {
        //     opt.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
        // });

        // services.AddAuthorization(opt =>
        // {
        //     opt.AddPolicy("RequireStaffRole", policy => policy.RequireRole("Staff"));
        // });

        // services.AddAuthorization(opt =>
        // {
        //     opt.AddPolicy("RequireAgentOrAdminRole", policy => policy.RequireRole("Admin", "Agent"));
        // });
        return services;
    }
}
