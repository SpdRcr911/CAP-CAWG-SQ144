// Data/AppDbContext.cs
using CAPSquadron_API.Models;
using Microsoft.EntityFrameworkCore;

namespace CAPSquadron_API.Data;

public class AppDbContext : DbContext
{
    public DbSet<Member> Members { get; set; }
    public DbSet<Achievement> Achievements { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Member>().HasIndex(m => m.CAPID).IsUnique();
        modelBuilder.Entity<Achievement>().HasIndex(a => new { a.CAPID, a.AchvName }).IsUnique(); // Add unique constraint
    }
}