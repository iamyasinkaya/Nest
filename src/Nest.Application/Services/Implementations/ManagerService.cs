using AutoMapper;
using Nest.Domain;

namespace Nest.Application;

public class ManagerService : IManagerService
{
    private readonly IManagerRepository _managerRepository;
    private readonly IMapper _mapper;
    private readonly IHotelRepository _hotelRepository;

    public ManagerService(IManagerRepository managerRepository, IMapper mapper, IHotelRepository hotelRepository)
    {
        _managerRepository = managerRepository;
        _mapper = mapper;
        _hotelRepository = hotelRepository;
    }

    public async Task<Manager> CreateManagerAsync(CreateManagerDto manager)
    {
        var hotel = await _hotelRepository.GetByIdAsync(manager.HotelId);

        if (hotel is null)
        {
            throw new KeyNotFoundException("Hotel not found with the given ID.");
        }

        Manager newManager = _mapper.Map<Manager>(manager);

        await _managerRepository.CreateAsync(newManager);

        return newManager;
    }
}
