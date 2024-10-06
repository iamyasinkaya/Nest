namespace Nest.Domain;

public interface IHotelRepository : IRepository<Hotel>
{
    Task<Hotel> GetHotelWithDetailsAsync(Guid id);
}