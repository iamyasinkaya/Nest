using Microsoft.EntityFrameworkCore;
using Nest.Domain;

namespace Nest.Infrastructure;

public class HotelRepository : BaseRepository<Hotel>, IHotelRepository
{
    public HotelRepository(ApplicationDbContext context) : base(context) { }

    public async Task<Hotel> GetHotelWithDetailsAsync(Guid id)
    {
        return await _dbSet
            .Include(h => h.ContactInfo)
            .Include(h => h.Manager)
            .FirstOrDefaultAsync(h => h.Id == id);
    }
}

