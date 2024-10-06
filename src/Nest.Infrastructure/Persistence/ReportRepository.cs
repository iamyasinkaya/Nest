﻿using Microsoft.EntityFrameworkCore;
using Nest.Domain;

namespace Nest.Infrastructure;

public class ReportRepository : BaseRepository<Report>, IReportRepository
{
    public ReportRepository(ApplicationDbContext context) : base(context) { }

    public async Task<IEnumerable<Report>> GetReportsByStatusAsync(ReportStatus status)
    {
        return await _dbSet.Where(r => r.Status == status).ToListAsync();
    }

    public async Task<int> GetHotelCountByLocationAsync(string location)
    {
        return await _dbSet.CountAsync(h => h.Location == location);
    }
}
