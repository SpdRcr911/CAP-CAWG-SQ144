// Data/AppDbContext.cs
using CAPSquadron_API.Extensions;
using CAPSquadron_API.Models;
using Microsoft.EntityFrameworkCore;

namespace CAPSquadron_API.Data;

public class AppDbContext : DbContext
{
    public DbSet<Achievement> Achievements { get; set; }
    public DbSet<Member> Members { get; set; }
    public DbSet<Flight> Flights { get; set; }
    public DbSet<FlightMember> FlightMembers { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

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

        modelBuilder.Entity<FlightMember>()
            .HasOne(fm => fm.Flight)
            .WithMany(f => f.FlightMembers)
            .HasForeignKey(fm => fm.FlightId);

        modelBuilder.Entity<FlightMember>()
            .HasOne(fm => fm.Member)
            .WithMany()
            .HasForeignKey(fm => fm.CAPID);

        modelBuilder.Entity<Achievement>().HasIndex(a => new { a.CAPID, a.AchvName }).IsUnique();
    }
}