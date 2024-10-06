using Nest.Domain;

namespace Nest.Application;

public class GetHotelDto
{
    public Guid Id { get; set; }
    public string HotelName { get; set; }
    public ICollection<ContactInfo> ContactInfo { get; set; }
}
