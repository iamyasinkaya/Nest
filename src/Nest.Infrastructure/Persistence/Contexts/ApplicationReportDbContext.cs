using Microsoft.EntityFrameworkCore;
using Nest.Domain;

namespace Nest.Infrastructure;

public class ApplicationReportDbContext : DbContext
{
    public ApplicationReportDbContext(DbContextOptions<ApplicationHotelDbContext> options) : base(options)
    {

    }

    public DbSet<Report> Reports { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Report Configuration
        modelBuilder.Entity<Report>(entity =>
        {
            entity.HasKey(r => r.Id);
            entity.Property(r => r.RequestedDate)
                .IsRequired();
            entity.Property(r => r.Status)
                .IsRequired();
            entity.Property(r => r.Location)
                .IsRequired()
                .HasMaxLength(255);
            entity.Property(r => r.LocationCode)
                  .IsRequired();
        });
    }
}
