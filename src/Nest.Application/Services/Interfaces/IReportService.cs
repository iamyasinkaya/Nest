using Nest.Domain;

namespace Nest.Application;

public interface IReportService
{
    Task CreateReportAsync(Report report);
    Task UpdateReportStatusAsync(Report report);
    Task<Report> GetReportByIdAsync(Guid reportId);
    Task<Report> RequestReportAsync(ReportRequestDto reportRequest);
}
