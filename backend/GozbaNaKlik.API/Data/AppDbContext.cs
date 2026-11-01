using GozbaNaKlik.API.Models;
using Microsoft.EntityFrameworkCore;

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
    public DbSet<RestaurantOperatorAssignment> RestaurantOperatorAssignments { get; set; }

    public DbSet<MenuItem> MenuItems { get; set; }

    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

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

        // Preciznost cene porudzbine.
        modelBuilder.Entity<Order>()
            .Property(o => o.TotalPrice)
            .HasPrecision(12, 2);

        // Preciznost pojedinacne cene stavke.
        modelBuilder.Entity<OrderItem>()
            .Property(oi => oi.UnitPrice)
            .HasPrecision(12, 2);

        // Hash od "admin123", izracunat unapred (PasswordHasher generise random salt
        // pri svakom pozivu, pa se ne moze racunati ovde - HasData zahteva fiksnu vrednost).
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