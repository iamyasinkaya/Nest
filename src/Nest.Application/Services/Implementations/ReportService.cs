using AutoMapper;
using Nest.Domain;

namespace Nest.Application;

public class ReportService : IReportService
{
    private readonly IReportRepository _reportRepository;
    private readonly IMapper _mapper;
    private readonly IContactInfoRepository _contactInfoRepository;

    public ReportService(IReportRepository reportRepository, IMapper mapper, IContactInfoRepository contactInfoRepository)
    {
        _reportRepository = reportRepository;
        _mapper = mapper;
        _contactInfoRepository = contactInfoRepository;
    }

    public async Task<Report> RequestReportAsync(ReportRequestDto reportRequest)
    {
       
        var location = reportRequest.Location; // location bilgisi ReportRequestDto'dan alınacak

       
        var hotelCount = await _reportRepository.GetHotelCountByLocationAsync(location);

        
        var contactInfoCount = await _contactInfoRepository.GetContactInfoCountByLocationAsync(location);

       
        var report = new Report
        {
            Id = Guid.NewGuid(),
            Location = location,
            HotelCount = hotelCount,
            PhoneNumberCount = contactInfoCount,
            RequestedDate = DateTime.UtcNow,
            Status = ReportStatus.Completed
        };
        await _reportRepository.CreateAsync(report);
        return report;
    }

    public async Task CreateReportAsync(Report report)
    {
        await _reportRepository.CreateAsync(report);
    }

    public async Task UpdateReportStatusAsync(Report report)
    {
        await _reportRepository.UpdateAsync(report);
    }

    public async Task<Report> GetReportByIdAsync(Guid reportId)
    {
        return await _reportRepository.GetByIdAsync(reportId);
    }


}