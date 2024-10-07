namespace Nest.Domain;

public class Manager
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    // Foreign Key
    public Guid HotelId { get; set; }
    public Hotel Hotel { get; set; }
}
