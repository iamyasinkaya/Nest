using Microsoft.Extensions.DependencyInjection;

namespace Nest.Application;

public static class ApplicationServiceExtension
{
    public static IServiceCollection ApplicationService(this IServiceCollection services)
    {
        // Service injections
        services.AddScoped<IHotelService, HotelService>();
        services.AddScoped<IContactInfoService, ContactInfoService>();
        services.AddScoped<IManagerService, ManagerService>();
        services.AddScoped<IReportService, ReportService>();

        return services;
    }
}
