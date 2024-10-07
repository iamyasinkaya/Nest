using AutoMapper;
using Nest.Domain;

namespace Nest.Application
{
    public class ReportService : IReportService
    {
        private readonly IMapper _mapper;
        private readonly IContactInfoRepository _contactInfoRepository;
        private readonly IRabbitMQService _rabbitMQService;
        private readonly IReportRepository _reportRepository;

        public ReportService(IMapper mapper, IContactInfoRepository contactInfoRepository, IRabbitMQService rabbitMQService, IReportRepository reportRepository)
        {
            _mapper = mapper;
            _contactInfoRepository = contactInfoRepository;
            _rabbitMQService = rabbitMQService;
            _reportRepository = reportRepository;
        }

        public async Task<Report> RequestReportAsync(ReportRequestDto reportRequest)
        {
            var contactInfos = await _contactInfoRepository.GetAllByConditionAsync(x => x.LocationCode == reportRequest.LocationCode);
            var phoneNumbers = await _contactInfoRepository
                .GetAllByConditionAsync(x => x.Type == ContactType.PhoneNumber &&
                                              contactInfos.Select(ci => ci.HotelId).Contains(x.HotelId));

            var report = new Report
            {
                Id = Guid.NewGuid(),
                HotelCount = contactInfos.Count(),
                LocationCode = reportRequest.LocationCode,
                RequestedDate = DateTime.Now,
                Status = ReportStatus.Preparing,
                PhoneNumberCount = phoneNumbers.Count(),
                Location = EnumHelper.GetLocationNameFromCode(reportRequest.LocationCode),
            };

            _rabbitMQService.Publish("report-requests", report);
            await _reportRepository.CreateAsync(report);
            return report;
        }

        public async Task<string> GenerateReportAsync(Guid reportId)
        {
            try
            {
                var report = await _rabbitMQService.GetReportAsync("report-requests", reportId, TimeSpan.FromSeconds(30));

                if (report != null)
                {
                    report.Status = ReportStatus.Completed;
                    await _reportRepository.UpdateAsync(report);

                    string reportContent = $"Hotel Count: {report.HotelCount}\n" +
                                           $"Location Code: {report.LocationCode}\n" +
                                           $"Requested Date: {report.RequestedDate}\n" +
                                           $"Status: {report.Status}\n" +
                                           $"Phone Number Count: {report.PhoneNumberCount}\n" +
                                           $"Location: {report.Location}";
                    return reportContent;
                }
            }
            catch (TimeoutException)
            {
                // Zaman aşımı durumunda yapılacak işlemler
                var report = await _reportRepository.GetByIdAsync(reportId);
                if (report != null)
                {
                    report.Status = ReportStatus.Preparing;
                    await _reportRepository.UpdateAsync(report);
                }
            }

            return string.Empty;
        }
    }
}