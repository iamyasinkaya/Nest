using Nest.Domain;

namespace Nest.Application;

public class ReportService : BaseService<Report>, IReportService
{
    private readonly IReportRepository _reportRepository;

    public ReportService(IReportRepository reportRepository) : base(reportRepository)
    {
        _reportRepository = reportRepository;
    }

    public async Task<IEnumerable<Report>> GetReportsByStatusAsync(ReportStatus status)
    {
        return await _reportRepository.GetReportsByStatusAsync(status);
    }
}
