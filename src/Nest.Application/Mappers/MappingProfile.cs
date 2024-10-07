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
        CreateMap<Hotel, GetHotelDto>()
             .ForMember(dest => dest.ContactInfo, opt => opt.MapFrom(src => src.ContactInfo))
             .ReverseMap();


        // CreateContactInfoDto'dan ContactInfo nesnesine dönüşüm
        CreateMap<CreateContactInfoDto, ContactInfo>()
    .ForMember(dest => dest.Id, opt => opt.Ignore());



        // ContactInfo'dan GetContactInfoDto nesnesine dönüşüm
        CreateMap<ContactInfo, GetContactInfoDto>().ReverseMap();


        // CreateManagerDto'dan Manager nesnesine dönüşüm
        CreateMap<CreateManagerDto, Manager>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        // ReportRequestDto'dan Report nesnesine dönüşüm
        CreateMap<ReportRequestDto, Report>()
               .ForMember(dest => dest.Id, opt => opt.Ignore())
               .ForMember(dest => dest.RequestedDate, opt => opt.Ignore())
               .ForMember(dest => dest.Status, opt => opt.Ignore())
               .ForMember(dest => dest.HotelCount, opt => opt.Ignore())
               .ForMember(dest => dest.PhoneNumberCount, opt => opt.Ignore());
    }
}
