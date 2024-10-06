using Nest.Domain;

namespace Nest.Application;

public class ManagerService : BaseService<Manager>, IManagerService
{
    private readonly IManagerRepository _managerRepository;

    public ManagerService(IManagerRepository managerRepository) : base(managerRepository)
    {
        _managerRepository = managerRepository;
    }

    public async Task<IEnumerable<Manager>> GetByHotelIdAsync(Guid hotelId)
    {
        return await _managerRepository.GetByHotelIdAsync(hotelId);
    }
}
