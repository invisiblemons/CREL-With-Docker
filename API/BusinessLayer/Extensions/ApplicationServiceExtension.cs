using CREL_BE.Entities;
using CREL_BE.Helpers;
using CREL_BE.Repositories;
using CREL_BE.Services;
using Microsoft.EntityFrameworkCore;

namespace CREL_BE.Extensions;

public static class ApplicationServiceExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<ImageKitSettings>(config.GetSection("ImagekitSettings"));
        services.AddScoped<IFileService, FileService>();
        services.AddScoped<IUnitOfWorkService, UnitOfWorkService>();
        services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
        services.AddDbContext<CRELDBContext>(options => options.UseSqlServer(config.GetConnectionString("Default")));
        return services;
    }
}