using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest.Domain;

namespace Nest.Infrastructure;

public static class InfrastructureServiceExtension
{
    public static IServiceCollection InfrastructureService(this IServiceCollection services, IConfiguration configuration)
    {
        // DbContext injection
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySql(configuration.GetConnectionString("DefaultConnection"),
                ServerVersion.AutoDetect(configuration.GetConnectionString("DefaultConnection")))
        );

        // Repository injections
        services.AddScoped<IHotelRepository, HotelRepository>();
        services.AddScoped<IContactInfoRepository, ContactInfoRepository>();
        services.AddScoped<IManagerRepository, ManagerRepository>();
        services.AddScoped<IReportRepository, ReportRepository>();

        return services;
    }
}

