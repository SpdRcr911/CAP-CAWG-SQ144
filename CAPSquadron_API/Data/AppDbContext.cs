// Data/AppDbContext.cs
using CAPSquadron_API.Extensions;
using CAPSquadron_API.Models;
using Microsoft.EntityFrameworkCore;

namespace CAPSquadron_API.Data;

public class AppDbContext : DbContext
{
    public DbSet<Member> Members { get; set; }
    public DbSet<Achievement> Achievements { get; set; }
    public DbSet<Flight> Flights { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            entity.SetTableName(entity.GetTableName()?.ToSnakeCase() ?? string.Empty);

            foreach (var property in entity.GetProperties())
            {
                property.SetColumnName(property.GetColumnName()?.ToSnakeCase() ?? string.Empty);
            }

            foreach (var key in entity.GetKeys())
            {
                key.SetName(key.GetName()?.ToSnakeCase() ?? string.Empty);
            }

            foreach (var fk in entity.GetForeignKeys())
            {
                fk.SetConstraintName(fk.GetConstraintName()?.ToSnakeCase() ?? string.Empty);
            }

            foreach (var index in entity.GetIndexes())
            {
                index.SetDatabaseName(index.GetDatabaseName()?.ToSnakeCase() ?? string.Empty);
            }
        }

        modelBuilder.Entity<Flight>()
            .HasOne(f => f.FlightCommander)
            .WithMany()
            .HasForeignKey(f => f.FlightCommanderCAPID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Flight>()
            .HasMany(f => f.Members)
            .WithOne(m => m.Flight)
            .HasForeignKey(m => m.FlightId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Flight>()
            .HasMany(f => f.FlightSergeants)
            .WithOne(m => m.FlightSergeantForFlight)
            .HasForeignKey(m => m.FlightSergeantForFlightId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Achievement>().HasIndex(a => new { a.CAPID, a.AchvName }).IsUnique();
    }
}