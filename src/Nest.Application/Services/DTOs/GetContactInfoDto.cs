using Nest.Domain;

namespace Nest.Application;

public class GetContactInfoDto
{
    public Guid Id { get; set; }
    public ContactType Type { get; set; }
    public string Content { get; set; }
    public Guid HotelId { get; set; }
}
