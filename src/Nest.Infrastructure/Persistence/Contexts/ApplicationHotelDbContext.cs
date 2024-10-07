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

            //Example Hotels

            entity.HasData(
                new Hotel
                {
                    Id = new Guid("37c26834-c2fd-4574-843a-38fdd9474eb6"),
                    HotelName = "Test 1",

                },
                new Hotel
                {
                    Id = new Guid("b9dc2b4c-5ab5-46bc-b523-6cd9e698e47c"),
                    HotelName = "Test 2",

                },
                new Hotel
                {
                    Id = new Guid("5988e09b-38e0-4769-b180-73fffbbd8d6b"),
                    HotelName = "Test 3",

                },
                new Hotel
                {
                    Id = new Guid("c7ea298b-ff64-4688-a172-2f581a1115bb"),
                    HotelName = "Test 4",

                },
                new Hotel
                {
                    Id = new Guid("27dd1fe5-c906-4347-bfa0-67a109082b4f"),
                    HotelName = "Test 5",

                },
                new Hotel
                {
                    Id = new Guid("31de4b1d-fa48-4594-b85f-f43af0621dde"),
                    HotelName = "Test 6",

                },
                new Hotel
                {
                    Id = new Guid("d8c31825-06d1-4a9e-b8a3-01aa3ba87299"),
                    HotelName = "Test 7",

                },
                new Hotel
                {
                    Id = new Guid("873d9ced-962f-4ab2-94a5-5e19d401e604"),
                    HotelName = "Test 8",

                },
                new Hotel
                {
                    Id = new Guid("53950c18-cb4e-462b-81e4-00aa4fd8dd62"),
                    HotelName = "Test 9",

                },
                new Hotel
                {
                    Id = new Guid("dec8da0e-903c-4cab-9afc-0487f77d7069"),
                    HotelName = "Test 10",

                }
                );
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

            entity.HasData(
                new ContactInfo
                {
                    Id = new Guid("5243226f-6501-4a0f-af3a-dcc98668095d"),
                    HotelId = new Guid("37c26834-c2fd-4574-843a-38fdd9474eb6"),
                    Type = ContactType.Location,
                    LocationCode = 34,
                    Content = "İstanbul"
                },
                 new ContactInfo
                 {
                     Id = new Guid("7bbcedba-54a8-441e-a08b-14818c1ec8ee"),
                     HotelId = new Guid("b9dc2b4c-5ab5-46bc-b523-6cd9e698e47c"),
                     Type = ContactType.Location,
                     LocationCode = 34,
                     Content = "İstanbul"
                 },
                  new ContactInfo
                  {
                      Id = new Guid("c97de36f-d632-42bc-b4d4-d90f35dbb1e0"),
                      HotelId = new Guid("5988e09b-38e0-4769-b180-73fffbbd8d6b"),
                      Type = ContactType.Location,
                      LocationCode = 34,
                      Content = "İstanbul"
                  },
                   new ContactInfo
                   {
                       Id = new Guid("55b66f8f-1466-48b0-8254-d1f3861cd0e7"),
                       HotelId = new Guid("c7ea298b-ff64-4688-a172-2f581a1115bb"),
                       Type = ContactType.Location,
                       LocationCode = 34,
                       Content = "İstanbul"
                   },
                    new ContactInfo
                    {
                        Id = new Guid("0fc19e82-5d70-4998-91e1-bcafbbfe9a4b"),
                        HotelId = new Guid("27dd1fe5-c906-4347-bfa0-67a109082b4f"),
                        Type = ContactType.Location,
                        LocationCode = 34,
                        Content = "İstanbul"
                    }


                );
                
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