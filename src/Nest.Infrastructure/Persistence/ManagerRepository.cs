using Microsoft.EntityFrameworkCore;
using Nest.Domain;

namespace Nest.Infrastructure;

public class ManagerRepository : BaseHotelRepository<Manager>, IManagerRepository
{
    public ManagerRepository(ApplicationHotelDbContext context) : base(context) { }

    public async Task<IEnumerable<Manager>> GetByHotelIdAsync(Guid hotelId)
    {
        return await _dbSet.Where(m => m.HotelId == hotelId).ToListAsync();
    }
}
