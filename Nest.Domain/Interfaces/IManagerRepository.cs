namespace Nest.Domain;

public interface IManagerRepository : IRepository<Manager>
{
    Task<IEnumerable<Manager>> GetByHotelIdAsync(Guid hotelId);
}