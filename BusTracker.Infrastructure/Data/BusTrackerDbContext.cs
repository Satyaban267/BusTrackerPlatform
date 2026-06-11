using BusTracker.Domain;
using Microsoft.EntityFrameworkCore;

namespace BusTracker.Infrastructure.Data;

public class BusTrackerDbContext : DbContext
{
    public BusTrackerDbContext(DbContextOptions<BusTrackerDbContext> options)
        : base(options) { }

    public DbSet<Bus> Buses => Set<Bus>();
    public DbSet<BusStop> BusStops => Set<BusStop>();
    public DbSet<BusRegistration> BusRegistrations => Set<BusRegistration>();
    public DbSet<RouteSuggestion> RouteSuggestions => Set<RouteSuggestion>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Bus → BusStop (one-to-many)
        modelBuilder.Entity<BusStop>()
            .HasOne(s => s.Bus)
            .WithMany(b => b.Stops)
            .HasForeignKey(s => s.BusId)
            .OnDelete(DeleteBehavior.Cascade);

        // Seed data — your real 6 bus services
        modelBuilder.Entity<Bus>().HasData(
            new Bus
            {
                Id = 1,
                ServiceName = "Giribala",
                ContactNumber = "9861891406",
                Origin = "Narsinghpur",
                Destination = "Bhubaneswar",
                ViaPoints = null,
                DepartureTime = "6:10 AM",
                ReturnTime = "2:15 PM",
                IsActive = true
            },
            new Bus
            {
                Id = 2,
                ServiceName = "Pitabali",
                ContactNumber = "09776359935",
                Origin = "Bhubaneswar",
                Destination = "Narsinghpur",
                ViaPoints = null,
                DepartureTime = "10:45 AM",
                ReturnTime = null,
                IsActive = true
            },
            new Bus
            {
                Id = 3,
                ServiceName = "Shibani",
                ContactNumber = "+919777510028",
                Origin = "Bhubaneswar",
                Destination = "Sagar",
                ViaPoints = "Kanpur",
                DepartureTime = "11:30 AM",
                ReturnTime = null,
                IsActive = true
            },
            new Bus
            {
                Id = 4,
                ServiceName = "Dilkhus",
                ContactNumber = null,
                Origin = "Bhubaneswar",
                Destination = "Anugul",
                ViaPoints = "Khordha T-Bridge, Narsinghpur, Rusipada",
                DepartureTime = "9:00 AM",
                ReturnTime = null,
                IsActive = true
            },
            new Bus
            {
                Id = 5,
                ServiceName = "Subhadra Bus",
                ContactNumber = "9668982220",
                Origin = "Cuttack",
                Destination = "Narsinghpur",
                ViaPoints = null,
                DepartureTime = "4:45 PM",
                ReturnTime = null,
                IsActive = true
            },
            new Bus
            {
                Id = 6,
                ServiceName = "Jagannath Bus",
                ContactNumber = "+919776353077",
                Origin = "Bhubaneswar",
                Destination = "Kamaladiha",
                ViaPoints = null,
                DepartureTime = "4:15 AM",
                ReturnTime = "11:30 AM",
                IsActive = true
            }
        );
    }
}
