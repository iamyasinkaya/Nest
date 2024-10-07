using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Nest.Domain;
using RabbitMQ.Client;
using System.Collections.Concurrent;

namespace Nest.Application;

public static class ApplicationServiceExtension
{
    public static IServiceCollection ApplicationService(this IServiceCollection services)
    {
        // Service Injections
        services.AddScoped<IHotelService, HotelService>();
        services.AddScoped<IContactInfoService, ContactInfoService>();
        services.AddScoped<IManagerService, ManagerService>();
        services.AddScoped<IReportService, ReportService>();


        // AutoMapper Injections
        var mappingConfig = new MapperConfiguration(cfg =>
        {

            cfg.AddProfile(new MappingProfile());
        });
        IMapper mapper = mappingConfig.CreateMapper();
        services.AddSingleton(mapper);

        // Configure RabbitMQ
        services.AddSingleton<IConnectionFactory>(sp =>
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",  // Ensure this is correct
                Password = "iraXlX9T2a9LBBSu",    // Ensure this is correct

            };
            return factory;
        });


        services.AddSingleton(sp =>
        {
            var factory = sp.GetRequiredService<IConnectionFactory>();
            var connection = factory.CreateConnection();
            return connection.CreateModel(); // Create a channel
        });

        // Register the in-memory cache
        services.AddSingleton(new ConcurrentDictionary<Guid, Report>());

        return services;
    }
}
