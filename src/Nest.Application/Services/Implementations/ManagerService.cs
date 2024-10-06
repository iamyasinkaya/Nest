using Nest.Domain;

namespace Nest.Application;

public class ManagerService : IManagerService
{
    private readonly IManagerRepository _managerRepository;

    public ManagerService(IManagerRepository managerRepository)
    {
        _managerRepository = managerRepository;
    }

    
}
