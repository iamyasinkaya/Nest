using Microsoft.EntityFrameworkCore;
using Nest.Domain;

namespace Nest.Infrastructure;

public class ContactInfoRepository : BaseHotelRepository<ContactInfo>, IContactInfoRepository
{
    public ContactInfoRepository(ApplicationHotelDbContext context) : base(context) { }

    public async Task<IEnumerable<ContactInfo>> GetByHotelIdAsync(Guid hotelId)
    {
        return await _dbSet.Where(ci => ci.HotelId == hotelId).ToListAsync();
    }

    public async Task<int> GetContactInfoCountByLocationAsync(long location)
    {
        return await _dbSet.CountAsync(c => c.LocationCode == location && c.Type == ContactType.PhoneNumber);
    }
    public async Task<int> GetHotelCountByLocationAsync(int location)
    {
        return await _dbSet.CountAsync(h => h.LocationCode == location && h.Type == ContactType.Location);
    }
}
