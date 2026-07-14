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


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Hash od "admin123", izracunat unapred (PasswordHasher generise random salt
        // pri svakom pozivu, pa se ne moze racunati ovde - HasData zahteva fiksnu vrednost).
        const string seedAdminPasswordHash = "AQAAAAIAAYagAAAAEKSqiTVL7qi6w2Gudvv5zFdzM2mFmGThBwj1by8cRHcWE4K76EWw77qSAK1J/nFvuA==";

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