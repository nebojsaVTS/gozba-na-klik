using GozbaNaKlik.API.Models;
using Microsoft.EntityFrameworkCore;
using GozbaNaKlik.API.Data.Seed.Sid;

namespace GozbaNaKlik.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    public DbSet<Restaurant> Restaurants { get; set; }

    public DbSet<Address> Addresses { get; set; }

    public DbSet<Municipality> Municipalities { get; set; }

    public DbSet<Settlement> Settlements { get; set; }

    public DbSet<Street> Streets { get; set; }

    public DbSet<RestaurantOperatorAssignment> RestaurantOperatorAssignments { get; set; }

    public DbSet<MenuItem> MenuItems { get; set; }

    public DbSet<Order> Orders { get; set; }

    public DbSet<OrderItem> OrderItems { get; set; }

    public DbSet<RestaurantWorkingHours> RestaurantWorkingHours { get; set; }

    public DbSet<RestaurantSpecialWorkingDay> RestaurantSpecialWorkingDays { get; set; }

    public DbSet<RestaurantDeliveryZone> RestaurantDeliveryZones { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ORDER -> USER
        // Ne dozvoljavamo da brisanje korisnika obrise istoriju porudzbina.
        modelBuilder.Entity<Order>()
            .HasOne(o => o.User)
            .WithMany()
            .HasForeignKey(o => o.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        // ORDER -> RESTAURANT
        // Ne dozvoljavamo da brisanje restorana obrise istoriju porudzbina.
        modelBuilder.Entity<Order>()
            .HasOne(o => o.Restaurant)
            .WithMany()
            .HasForeignKey(o => o.RestaurantId)
            .OnDelete(DeleteBehavior.Restrict);

        // ORDER -> ORDER ITEMS
        // Ako se obrise sama porudzbina, brisu se i njene stavke.
        modelBuilder.Entity<Order>()
            .HasMany(o => o.Items)
            .WithOne(oi => oi.Order)
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        // MUNICIPALITY -> SETTLEMENTS
        // Ako se obrise opstina, brisu se i naselja koja joj pripadaju.
        modelBuilder.Entity<Settlement>()
            .HasOne(s => s.Municipality)
            .WithMany(m => m.Settlements)
            .HasForeignKey(s => s.MunicipalityId)
            .OnDelete(DeleteBehavior.Cascade);

        // Jedna opstina ne moze imati dva naselja sa istim imenom.
        modelBuilder.Entity<Settlement>()
            .HasIndex(s => new
            {
                s.MunicipalityId,
                s.Name
            })
            .IsUnique();

        // ADDRESS -> STREET
        // StreetId je za sada nullable jer postojece adrese jos koriste
        // tekstualna Street i City polja.
        modelBuilder.Entity<Address>()
            .HasOne(a => a.StreetEntity)
            .WithMany(s => s.Addresses)
            .HasForeignKey(a => a.StreetId)
            .OnDelete(DeleteBehavior.Restrict);

        // SETTLEMENT -> STREETS
        // Ako se obrise naselje, brisu se i ulice koje mu pripadaju.
        modelBuilder.Entity<Street>()
            .HasOne(s => s.Settlement)
            .WithMany(s => s.Streets)
            .HasForeignKey(s => s.SettlementId)
            .OnDelete(DeleteBehavior.Cascade);

        // Jedno naselje ne moze imati dve ulice sa istim imenom.
        modelBuilder.Entity<Street>()
            .HasIndex(s => new
            {
                s.SettlementId,
                s.Name
            })
            .IsUnique();

        // RESTAURANT -> WORKING HOURS
        // Ako se restoran obrise, brise se i njegovo redovno radno vreme.
        modelBuilder.Entity<RestaurantWorkingHours>()
            .HasOne(wh => wh.Restaurant)
            .WithMany(r => r.WorkingHours)
            .HasForeignKey(wh => wh.RestaurantId)
            .OnDelete(DeleteBehavior.Cascade);

        // Jedan restoran moze imati samo jedan unos za isti dan u nedelji.
        modelBuilder.Entity<RestaurantWorkingHours>()
            .HasIndex(wh => new
            {
                wh.RestaurantId,
                wh.DayOfWeek
            })
            .IsUnique();

        // RESTAURANT -> SPECIAL WORKING DAYS
        // Ako se restoran obrise, brisu se i posebni dani rada.
        modelBuilder.Entity<RestaurantSpecialWorkingDay>()
            .HasOne(sw => sw.Restaurant)
            .WithMany(r => r.SpecialWorkingDays)
            .HasForeignKey(sw => sw.RestaurantId)
            .OnDelete(DeleteBehavior.Cascade);

        // Jedan restoran moze imati samo jedan poseban unos za isti datum.
        modelBuilder.Entity<RestaurantSpecialWorkingDay>()
            .HasIndex(sw => new
            {
                sw.RestaurantId,
                sw.Date
            })
            .IsUnique();

        // RESTAURANT -> DELIVERY ZONES
        // Svaki restoran ima sopstvena pravila dostave po naselju.
        modelBuilder.Entity<RestaurantDeliveryZone>()
            .HasOne(dz => dz.Restaurant)
            .WithMany(r => r.DeliveryZones)
            .HasForeignKey(dz => dz.RestaurantId)
            .OnDelete(DeleteBehavior.Cascade);

        // SETTLEMENT -> RESTAURANT DELIVERY ZONES
        // Naselje ne moze da se obrise dok postoji restoran koji ga koristi
        // kao zonu dostave.
        modelBuilder.Entity<RestaurantDeliveryZone>()
            .HasOne(dz => dz.Settlement)
            .WithMany(s => s.RestaurantDeliveryZones)
            .HasForeignKey(dz => dz.SettlementId)
            .OnDelete(DeleteBehavior.Restrict);

        // Jedan restoran moze imati samo jedno pravilo dostave
        // za isto naselje.
        modelBuilder.Entity<RestaurantDeliveryZone>()
            .HasIndex(dz => new
            {
                dz.RestaurantId,
                dz.SettlementId
            })
            .IsUnique();

        // Preciznost cene dostave.
        modelBuilder.Entity<RestaurantDeliveryZone>()
            .Property(dz => dz.DeliveryFee)
            .HasPrecision(12, 2);

        // Preciznost minimalnog iznosa porudzbine.
        modelBuilder.Entity<RestaurantDeliveryZone>()
            .Property(dz => dz.MinimumOrderAmount)
            .HasPrecision(12, 2);

        // Preciznost cene porudzbine.
        modelBuilder.Entity<Order>()
            .Property(o => o.TotalPrice)
            .HasPrecision(12, 2);

        // Preciznost pojedinacne cene stavke.
        modelBuilder.Entity<OrderItem>()
            .Property(oi => oi.UnitPrice)
            .HasPrecision(12, 2);

        // Seed podaci za opstinu Sid.
        SidSeed.Seed(modelBuilder);
        SidStreetsSeed.Seed(modelBuilder);

        // Hash od "admin123", izracunat unapred.
        const string seedAdminPasswordHash =
            "AQAAAAIAAYagAAAAEKSqiTVL7qi6w2Gudvv5zFdzM2mFmGThBwj1by8cRHcWE4K76EWw77qSAK1J/nFvuA==";

        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = -1,
                Username = "admin1",
                Email = "admin1@gozbanaklik.com",
                Password = seedAdminPasswordHash,
                Role = UserRoles.Administrator
            },
            new User
            {
                Id = -2,
                Username = "admin2",
                Email = "admin2@gozbanaklik.com",
                Password = seedAdminPasswordHash,
                Role = UserRoles.Administrator
            },
            new User
            {
                Id = -3,
                Username = "admin3",
                Email = "admin3@gozbanaklik.com",
                Password = seedAdminPasswordHash,
                Role = UserRoles.Administrator
            }
        );
    }
}