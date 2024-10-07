using Microsoft.EntityFrameworkCore;
using Nest.Domain;

namespace Nest.Infrastructure;

public class HotelRepository : BaseHotelRepository<Hotel>, IHotelRepository
{
    public HotelRepository(ApplicationHotelDbContext context) : base(context) { }

    public async Task<Hotel> GetHotelWithDetailsAsync(Guid id)
    {
        return await _dbSet
            .Include(h => h.ContactInfo)
            .Include(h => h.Manager)
            .FirstOrDefaultAsync(h => h.Id == id);
    }
}

