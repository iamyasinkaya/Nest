namespace Nest.Domain;

public class ContactInfo
{
    public Guid Id { get; set; }
    public ContactType Type { get; set; }
    public string Content { get; set; }

    // Foreign Key
    public Guid HotelId { get; set; }
    public Hotel Hotel { get; set; }
}
