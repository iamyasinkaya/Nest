using Nest.Domain;

namespace Nest.Application;

public interface IReportService : IBaseService<Report>
{
    Task<IEnumerable<Report>> GetReportsByStatusAsync(ReportStatus status);
}
