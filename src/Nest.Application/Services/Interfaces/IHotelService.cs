using Nest.Domain;

namespace Nest.Application;

public interface IHotelService
{
    Task<Hotel> CreateHotelAsync(CreateHoteldDto addHotel);
    Task<GetHotelDto> GetHotelWithDetailsAsync(Guid id);
    Task<Hotel> UpdateHotelAsync(UpdateHotelDto updateHotel);
    Task<bool> DeleteHotelAsync(Guid id);
}
