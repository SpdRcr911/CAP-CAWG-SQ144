// Data/AppDbContext.cs
using CAPSquadron_API.Extensions;
using CAPSquadron_API.Models;
using Microsoft.EntityFrameworkCore;

namespace CAPSquadron_API.Data;

public class AppDbContext : DbContext
{
    public DbSet<CadetPromotionsFullTrack> CadetPromotionsFullTracks { get; set; }
    public DbSet<AttendanceSignIn> AttendanceSignIns { get; set; }
    public DbSet<Flight> Flights { get; set; }
    public DbSet<FlightMember> FlightMembers { get; set; }
    public DbSet<QualityCadetUnitReport> QualityCadetUnitReports { get; set; }
    public DbSet<Member> Members { get; set; }
    public DbSet<AttendanceReport> AttendanceReports { get; set; }
    public DbSet<CadetPhysicalFitnessTrainingReport> CadetPhysicalFitnessTrainingReports { get; set; }
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
            .HasForeignKey(fm => fm.CAPID)
            .HasPrincipalKey(m => m.CAPID)
            .OnDelete(DeleteBehavior.Restrict); 

        modelBuilder.Entity<CadetPromotionsFullTrack>().HasIndex(a => new { a.CAPID, a.AchvName }).IsUnique();

        modelBuilder.Entity<QualityCadetUnitReport>().ToTable("quality_cadet_unit_reports", t => t.ExcludeFromMigrations());
    }
    public void TruncateAttendanceReports()
    {
        Database.ExecuteSqlRaw("TRUNCATE TABLE attendance_reports RESTART IDENTITY");
    }
    public void TruncateCadetPhysicalFitnessTrainingReports()
    {
        Database.ExecuteSqlRaw("TRUNCATE TABLE cadet_physical_fitness_training_reports RESTART IDENTITY");
    }
}
