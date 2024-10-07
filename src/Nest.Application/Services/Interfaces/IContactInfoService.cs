using Nest.Domain;

namespace Nest.Application;

public interface IContactInfoService
{
    Task<ContactInfo> CreateAsync(CreateContactInfoDto contactInfo);
    Task<GetContactInfoDto> GetContactInfoWithDetailsAsync(Guid id);
    Task<bool> RemoveContactInfoAsync(Guid id);
}
