using AutoMapper;
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

        // Bir MapperConfiguration nesnesi oluşturuluyor. Bu, AutoMapper kütüphanesinin haritalama yapılandırmasını tanımlar.
        var mappingConfig = new MapperConfiguration(cfg =>
        {
            // MapperConfiguration'a bir profil ekleniyor. Bu profil, MappingProfile sınıfından türetilmiş bir sınıftır ve AutoMapper'a özel haritalama kurallarını içerir.
            cfg.AddProfile(new MappingProfile()); // Önceki cevapta oluşturduğumuz profili kullanın
        });

        // Oluşturulan MapperConfiguration nesnesi kullanılarak bir IMapper nesnesi oluşturuluyor.
        // Bu, gerçek haritalama işlemlerini yapmak için kullanılacaktır.
        IMapper mapper = mappingConfig.CreateMapper();

        // Servislerin koleksiyonunu kullanarak bu IMapper nesnesi bir Singleton olarak ekleniyor.
        // Bu, uygulama boyunca aynı nesnenin kullanılmasını sağlar.
        services.AddSingleton(mapper);


        return services;
    }
}
