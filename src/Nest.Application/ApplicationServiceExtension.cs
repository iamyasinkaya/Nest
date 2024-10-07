using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

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


        return services;
    }
}
