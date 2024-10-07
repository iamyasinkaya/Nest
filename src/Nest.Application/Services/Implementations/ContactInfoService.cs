using AutoMapper;
using Nest.Domain;

namespace Nest.Application;

public class ContactInfoService : IContactInfoService
{
    private readonly IContactInfoRepository _contactInfoRepository;
    private readonly IMapper _mapper;
    private readonly IHotelRepository _hotelRepository;

    public ContactInfoService(IContactInfoRepository contactInfoRepository, IMapper mapper, IHotelRepository hotelRepository)
    {
        _contactInfoRepository = contactInfoRepository;
        _mapper = mapper;
        _hotelRepository = hotelRepository;
    }

    public async Task<ContactInfo> CreateAsync(CreateContactInfoDto contactInfo)
    {
        var hotel = await _hotelRepository.GetByIdAsync(contactInfo.HotelId);

        if (hotel is null)
        {
            throw new KeyNotFoundException("Hotel not found with the given ID.");
        }

        if (contactInfo.Type == ContactType.Location)
        {
            if (contactInfo.LocationCode == 0)
            {
                throw new Exception("Location Code is not be empty.");
            }
        }

        ContactInfo newContactInfo = _mapper.Map<ContactInfo>(contactInfo);

        await _contactInfoRepository.CreateAsync(newContactInfo);

        return newContactInfo;
    }

    public async Task<GetContactInfoDto> GetContactInfoWithDetailsAsync(Guid id)
    {
        var contactInfo = await _contactInfoRepository.GetByIdAsync(id);
        var getContactInfoDto = _mapper.Map<GetContactInfoDto>(contactInfo);
        return getContactInfoDto;
    }

    public async Task<bool> RemoveContactInfoAsync(Guid id)
    {
        var contactInfo = await _contactInfoRepository.GetByIdAsync(id);

        if (contactInfo is null)
        {
            return false;
        }

        await _contactInfoRepository.DeleteAsync(id);

        return true;
    }
}
