using AutoMapper;
using Nest.Domain;

namespace Nest.Application;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // AddHotelDto'dan Hotel nesnesine dönüşüm
        CreateMap<CreateHoteldDto, Hotel>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()); // ID'yi ignore ediyoruz, çünkü yeni bir otel oluşturuluyor.


        // UpdateHotelDto'dan Hotel nesnesine dönüşüm
        CreateMap<UpdateHotelDto, Hotel>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()); // ID değiştirilmemeli, ignore ediyoruz.

        // GetHotelDto'dan Hotel nesnesine dönüşüm
        CreateMap<GetHotelDto, Hotel>().ReverseMap();



        CreateMap<CreateContactInfoDto, ContactInfo>().ForMember(dest=>dest.Id, opt => opt.Ignore());

        CreateMap<GetContactInfoDto, ContactInfo>().ReverseMap();
    }
}
