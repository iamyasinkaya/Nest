namespace Nest.Domain;

public class Report
{

    public Guid Id { get; set; }
    public DateTime RequestedDate { get; set; }
    public ReportStatus Status { get; set; }
    public string Location { get; set; }
    public int LocationCode { get; set; }
    public int HotelCount { get; set; }
    public int PhoneNumberCount { get; set; }

}
