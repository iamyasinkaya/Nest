using Microsoft.EntityFrameworkCore;
using Nest.Domain;

namespace Nest.Infrastructure;

public class ContactInfoRepository : BaseRepository<ContactInfo>, IContactInfoRepository
{
    public ContactInfoRepository(ApplicationDbContext context) : base(context) { }

    public async Task<IEnumerable<ContactInfo>> GetByHotelIdAsync(Guid hotelId)
    {
        return await _dbSet.Where(ci => ci.HotelId == hotelId).ToListAsync();
    }

    public async Task<int> GetContactInfoCountByLocationAsync(string location)
    {
        return await _dbSet.CountAsync(c => c.Content == location && c.Type == ContactType.PhoneNumber);
    }
}
