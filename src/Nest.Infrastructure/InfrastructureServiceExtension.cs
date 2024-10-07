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
            options.UseMySql(configuration.GetConnectionString("DefaultHotelConnection"),
                ServerVersion.AutoDetect(configuration.GetConnectionString("DefaultHotelConnection")))
        );

        // DbContext Report Injections
        services.AddDbContext<ApplicationReportDbContext>(options =>
            options.UseMySql(configuration.GetConnectionString("DefaultReportConnection"),
                ServerVersion.AutoDetect(configuration.GetConnectionString("DefaultReportConnection")))
        );

        // Repository Injections
        services.AddScoped<IHotelRepository, HotelRepository>();
        services.AddScoped<IContactInfoRepository, ContactInfoRepository>();
        services.AddScoped<IManagerRepository, ManagerRepository>();
        services.AddScoped<IReportRepository, ReportRepository>();

        // RabbitMQ Settings
        services.Configure<RabbitMQSettings>(options =>
        {
            options.HostName = configuration["RabbitMQ:HostName"];
            options.UserName = configuration["RabbitMQ:UserName"];
            options.Password = configuration["RabbitMQ:Password"];
            options.QueueName = configuration["RabbitMQ:QueueName"];
        });


        // RabbitMq Injections
        services.AddSingleton<IMessageQueueService, RabbitMqService>();

        return services;
    }
}

