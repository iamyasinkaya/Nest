namespace Nest.Domain;

public interface IContactInfoRepository : IRepository<ContactInfo>
{
    Task<IEnumerable<ContactInfo>> GetByHotelIdAsync(Guid hotelId);
    Task<int> GetContactInfoCountByLocationAsync(string location);
}