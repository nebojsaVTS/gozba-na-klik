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
}
