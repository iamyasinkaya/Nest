using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest.Domain;

namespace Nest.Infrastructure;

public static class InfrastructureServiceExtension
{
    public static IServiceCollection InfrastructureService(this IServiceCollection services, IConfiguration configuration)
    {
        // DbContext Hotel Injections
        services.AddDbContext<ApplicationHotelDbContext>(options =>
            options.UseMySql(configuration.GetConnectionString("HotelDb"),
                ServerVersion.AutoDetect(configuration.GetConnectionString("HotelDb")))
        );

        // DbContext Report Injections
        services.AddDbContext<ApplicationReportDbContext>(options =>
            options.UseMySql(configuration.GetConnectionString("ReportingDb"),
                ServerVersion.AutoDetect(configuration.GetConnectionString("ReportingDb")))
        );

        // Repository Injections
        services.AddScoped<IHotelRepository, HotelRepository>();
        services.AddScoped<IContactInfoRepository, ContactInfoRepository>();
        services.AddScoped<IManagerRepository, ManagerRepository>();
        services.AddScoped<IReportRepository, ReportRepository>();

        // RabbitMQSettings'i appsettings.json'dan yapılandır
        services.Configure<RabbitMQSettings>(configuration.GetSection("RabbitMQSettings"));

        // RabbitMQService'i singleton olarak ekle
        services.AddSingleton<IRabbitMQService, RabbitMQService>();

        return services;
    }
}

