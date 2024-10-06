using Microsoft.EntityFrameworkCore;
using Nest.Domain;

namespace Nest.Infrastructure;

public class ManagerRepository : BaseRepository<Manager>, IManagerRepository
{
    public ManagerRepository(ApplicationDbContext context) : base(context) { }

    public async Task<IEnumerable<Manager>> GetByHotelIdAsync(Guid hotelId)
    {
        return await _dbSet.Where(m => m.HotelId == hotelId).ToListAsync();
    }
}
