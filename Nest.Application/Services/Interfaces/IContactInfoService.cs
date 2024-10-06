using Nest.Domain;

namespace Nest.Application;

public interface IContactInfoService : IBaseService<ContactInfo>
{
    Task<IEnumerable<ContactInfo>> GetByHotelIdAsync(Guid hotelId);
}
