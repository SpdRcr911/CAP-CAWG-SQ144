using CAPSquadron_API.Models;
using Microsoft.EntityFrameworkCore;

namespace CAPSquadron_API.Data;

public class QueryDbContext : DbContext
{
    public QueryDbContext(DbContextOptions<QueryDbContext> options) : base(options) { }

    public DbSet<PersonalCadetTrackerDto> PersonalCadetTrackers { get; set; }
    public DbSet<MemberAttributesDto> MemberAttributes { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PersonalCadetTrackerDto>().HasNoKey();
        modelBuilder.Entity<MemberAttributesDto>().HasNoKey();
        base.OnModelCreating(modelBuilder);
    }
}
