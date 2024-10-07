using Nest.Domain;

namespace Nest.Application;

public class CreateContactInfoDto
{
    public ContactType Type { get; set; }
    public string Content { get; set; }
    public int LocationCode { get; set; }
    public Guid HotelId { get; set; }
}
