using Nest.Domain;

namespace Nest.Application;

public interface IReportService
{

    Task<Report> RequestReportAsync(ReportRequestDto reportRequest);
    Task<string> GenerateReportAsync(Guid reportId);
}
