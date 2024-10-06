using Nest.Domain;

namespace Nest.Application;

public class HotelService : BaseService<Hotel>, IHotelService
{
    private readonly IHotelRepository _hotelRepository;

    public HotelService(IHotelRepository hotelRepository) : base(hotelRepository)
    {
        _hotelRepository = hotelRepository;
    }

    public async Task<Hotel> GetHotelWithDetailsAsync(Guid id)
    {
        return await _hotelRepository.GetHotelWithDetailsAsync(id);
    }
}
