namespace Nest.Domain;

public class Hotel
{
    public Guid Id { get; set; }
    public string HotelName { get; set; }
    public ICollection<ContactInfo> ContactInfo { get; set; }
    public ICollection<Manager> Manager { get; set; }
}
