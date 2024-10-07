namespace Nest.Domain;

public interface IReportRepository : IRepository<Report>
{
    Task<IEnumerable<Report>> GetReportsByStatusAsync(ReportStatus status);
   
}