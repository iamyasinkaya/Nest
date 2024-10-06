using Microsoft.AspNetCore.Mvc;
using Nest.Application;
using Nest.Domain;
using System.Text;

namespace Nest.ReportAPI;

[ApiController]
[Route("api/reports")]
public class ReportController : ControllerBase
{
    private readonly IReportService _reportService;
    private readonly IMessageQueueService _messageQueueService;

    public ReportController(IReportService reportService, IMessageQueueService messageQueueService)
    {
        _reportService = reportService;
        _messageQueueService = messageQueueService;
    }

    [HttpPost("request")]
    public async Task<IActionResult> RequestReport([FromBody] ReportRequestDto reportRequest)
    {

        var report = await _reportService.RequestReportAsync(reportRequest);

        await _messageQueueService.SendMessageAsync(report);

        return Accepted(new { ReportId = report.Id });
    }

    [HttpGet("{reportId}/download")]
    public async Task<IActionResult> DownloadReport(Guid reportId)
    {
        var report = await _reportService.GetReportByIdAsync(reportId);

        if (report == null)
        {
            return NotFound();
        }

        if (report.Status != ReportStatus.Completed)
        {
            return BadRequest("Rapor henüz tamamlanmadı.");
        }

        var reportContent = new StringBuilder();
        reportContent.AppendLine($"Rapor ID: {report.Id}");
        reportContent.AppendLine($"Rapor Talep Tarihi: {report.RequestedDate}");
        reportContent.AppendLine($"Konum: {report.Location}");
        reportContent.AppendLine($"Otellerin Sayısı: {report.HotelCount}");
        reportContent.AppendLine($"Telefon Numaraları Sayısı: {report.PhoneNumberCount}");

        var byteArray = Encoding.UTF8.GetBytes(reportContent.ToString());
        var stream = new MemoryStream(byteArray);

        return File(stream, "text/plain", $"Rapor_{reportId}.txt");
    }
}