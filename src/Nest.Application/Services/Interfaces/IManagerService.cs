using Nest.Domain;

namespace Nest.Application;

public interface IManagerService
{
    Task<Manager> CreateManagerAsync(CreateManagerDto manager);
}
