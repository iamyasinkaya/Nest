using AutoMapper;
using Nest.Domain;

namespace Nest.Application;

public class HotelService : IHotelService
{
    private readonly IHotelRepository _hotelRepository;
    private readonly IMapper _mapper;

    public HotelService(IHotelRepository hotelRepository, IMapper mapper)
    {
        _hotelRepository = hotelRepository;
        _mapper = mapper;
    }

    public async Task<Hotel> CreateHotelAsync(CreateHoteldDto addHotel)
    {
        Hotel newHotel = _mapper.Map<Hotel>(addHotel);

        await _hotelRepository.CreateAsync(newHotel);

        return newHotel;
    }

    public async Task<GetHotelDto> GetHotelWithDetailsAsync(Guid id)
    {
        var hotel = await _hotelRepository.GetHotelWithDetailsAsync(id);
        var getHotelDto = _mapper.Map<GetHotelDto>(hotel);
        return getHotelDto;
    }

    public async Task<Hotel> UpdateHotelAsync(UpdateHotelDto updateHotel)
    {
        var oldHotel = await _hotelRepository.GetByIdAsync(updateHotel.Id);

        if (oldHotel is null)
        {
            return null;
        }

        _mapper.Map(updateHotel, oldHotel);

        await _hotelRepository.UpdateAsync(oldHotel);

        return oldHotel;
    }

    public async Task<bool> DeleteHotelAsync(Guid id)
    {
        var hotel = await _hotelRepository.GetByIdAsync(id);

        if (hotel is null)
        {
            return false;
        }

        await _hotelRepository.DeleteAsync(hotel.Id);

        return true;
    }
}
