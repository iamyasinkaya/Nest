using Nest.Domain;

namespace Nest.Application;

public interface IManagerService : IBaseService<Manager>
{
    Task<IEnumerable<Manager>> GetByHotelIdAsync(Guid hotelId);
}
