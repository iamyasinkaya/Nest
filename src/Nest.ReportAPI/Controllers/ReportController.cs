using Microsoft.AspNetCore.Mvc;
using Nest.Application;
using System.Text;

namespace Nest.ReportAPI
{
    [ApiController]
    [Route("api/reports")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;


        public ReportController(IReportService reportService)
        {
            _reportService = reportService;

        }

        [HttpPost("request")]
        public async Task<IActionResult> RequestAsync([FromQuery] ReportRequestDto reportRequest)
        {
            var report = await _reportService.RequestReportAsync(reportRequest);
            return Accepted(new { ReportId = report.Id });
        }

        [HttpGet("download-report/{reportId}")]
        public async Task<IActionResult> DownloadReportAsync(Guid reportId)
        {



            string reportContent = await _reportService.GenerateReportAsync(reportId);


            if (string.IsNullOrEmpty(reportContent))
            {
                return NotFound("Report not found.");
            }


            var fileBytes = Encoding.UTF8.GetBytes(reportContent);
            return File(fileBytes, "text/plain", $"Report_{reportId}.txt");
        }

    }
}
