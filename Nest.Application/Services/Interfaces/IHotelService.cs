using Nest.Domain;

namespace Nest.Application;

public interface IHotelService : IBaseService<Hotel>
{
    Task<Hotel> GetHotelWithDetailsAsync(Guid id);
}
