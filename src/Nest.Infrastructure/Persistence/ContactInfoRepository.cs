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

    public async Task<int> GetContactInfoCountByLocationAsync(string location)
    {
        return await _dbSet.CountAsync(c => c.Content == location && c.Type == ContactType.PhoneNumber);
    }
}
