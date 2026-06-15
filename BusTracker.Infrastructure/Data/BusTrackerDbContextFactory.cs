using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BusTracker.Infrastructure.Data;

/// <summary>
/// Required by dotnet-ef CLI for design-time operations (migrations, scaffolding).
/// Uses a local SQLite file so no IConfiguration dependency is needed.
/// For PostgreSQL migrations, use: dotnet ef migrations add MigrationName -- --provider Npgsql
/// </summary>
public class BusTrackerDbContextFactory : IDesignTimeDbContextFactory<BusTrackerDbContext>
{
    public BusTrackerDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<BusTrackerDbContext>();

        // Check if a PostgreSQL provider was requested via CLI args
        var useNpgsql = args.Any(a => a.Equals("--provider", StringComparison.OrdinalIgnoreCase)
            && Array.IndexOf(args, a) + 1 < args.Length
            && args[Array.IndexOf(args, a) + 1].Equals("Npgsql", StringComparison.OrdinalIgnoreCase));

        if (useNpgsql)
        {
            // For generating PostgreSQL-specific migrations
            optionsBuilder.UseNpgsql("Host=localhost;Database=bustracker;Username=postgres;Password=postgres");
        }
        else
        {
            optionsBuilder.UseSqlite("Data Source=bustracker.db");
        }

        return new BusTrackerDbContext(optionsBuilder.Options);
    }
}
