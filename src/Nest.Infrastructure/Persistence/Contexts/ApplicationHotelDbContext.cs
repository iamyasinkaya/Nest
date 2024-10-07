using Microsoft.EntityFrameworkCore;
using Nest.Domain;

namespace Nest.Infrastructure;

public class ApplicationHotelDbContext : DbContext
{
    public ApplicationHotelDbContext(DbContextOptions<ApplicationHotelDbContext> options) : base(options)
    {
    }

    public DbSet<Hotel> Hotels { get; set; }
    public DbSet<ContactInfo> ContactInfos { get; set; }
    public DbSet<Manager> Managers { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Hotel Configuration
        modelBuilder.Entity<Hotel>(entity =>
        {
            entity.HasKey(h => h.Id);
            entity.Property(h => h.HotelName)
                .IsRequired()
                .HasMaxLength(255);
            entity.HasMany(h => h.ContactInfo)
                .WithOne(ci => ci.Hotel)
                .HasForeignKey(ci => ci.HotelId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasMany(h => h.Manager)
                .WithOne(m => m.Hotel)
                .HasForeignKey(m => m.HotelId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // ContactInfo Configuration
        modelBuilder.Entity<ContactInfo>(entity =>
        {
            entity.HasKey(ci => ci.Id);
            entity.Property(ci => ci.Type)
                .IsRequired();
            entity.Property(ci => ci.Content)
                .IsRequired()
                .HasMaxLength(500);
        });

        // Manager Configuration
        modelBuilder.Entity<Manager>(entity =>
        {
            entity.HasKey(m => m.Id);
            entity.Property(m => m.FirstName)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(m => m.LastName)
                .IsRequired()
                .HasMaxLength(100);
        });



    }
}