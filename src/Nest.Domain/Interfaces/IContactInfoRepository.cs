namespace Nest.Domain;

public interface IContactInfoRepository : IRepository<ContactInfo>
{
    Task<IEnumerable<ContactInfo>> GetByHotelIdAsync(Guid hotelId);
    Task<int> GetContactInfoCountByLocationAsync(long location);
    Task<int> GetHotelCountByLocationAsync(int location);
}