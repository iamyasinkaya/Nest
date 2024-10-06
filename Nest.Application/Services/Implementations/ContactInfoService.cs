using Nest.Domain;

namespace Nest.Application;

public class ContactInfoService : BaseService<ContactInfo>, IContactInfoService
{
    private readonly IContactInfoRepository _contactInfoRepository;

    public ContactInfoService(IContactInfoRepository contactInfoRepository) : base(contactInfoRepository)
    {
        _contactInfoRepository = contactInfoRepository;
    }

    public async Task<IEnumerable<ContactInfo>> GetByHotelIdAsync(Guid hotelId)
    {
        return await _contactInfoRepository.GetByHotelIdAsync(hotelId);
    }
}
