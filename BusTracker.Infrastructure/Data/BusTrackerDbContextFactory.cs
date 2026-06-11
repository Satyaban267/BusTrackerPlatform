using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BusTracker.Infrastructure.Data;

/// <summary>
/// Required by dotnet-ef CLI for design-time operations (migrations, scaffolding).
/// Uses a local SQLite file so no IConfiguration dependency is needed.
/// </summary>
public class BusTrackerDbContextFactory : IDesignTimeDbContextFactory<BusTrackerDbContext>
{
    public BusTrackerDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<BusTrackerDbContext>();
        optionsBuilder.UseSqlite("Data Source=bustracker.db");

        return new BusTrackerDbContext(optionsBuilder.Options);
    }
}
