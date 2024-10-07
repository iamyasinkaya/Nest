using System.Text.Json.Serialization;

namespace Nest.Domain;

public class ContactInfo
{
    public Guid Id { get; set; }
    public ContactType Type { get; set; }
    public string Content { get; set; }
    public int LocationCode { get; set; }

    // Foreign Key
    public Guid HotelId { get; set; }

    [JsonIgnore]
    public Hotel Hotel { get; set; }

}
