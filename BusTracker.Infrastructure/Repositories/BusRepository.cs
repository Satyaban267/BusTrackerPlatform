using BusTracker.Domain;
using BusTracker.Domain.Interfaces;
using BusTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BusTracker.Infrastructure.Repositories;

public class BusRepository : IBusRepository
{
    private readonly BusTrackerDbContext _db;

    public BusRepository(BusTrackerDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Bus>> GetAllAsync(string? from = null, string? to = null)
    {
        var query = _db.Buses
            .Include(b => b.Stops.OrderBy(s => s.StopOrder))
            .Where(b => b.IsActive);

        if (!string.IsNullOrWhiteSpace(from))
        {
            var fromLower = from.Trim().ToLower();
            query = query.Where(b =>
                b.Origin.ToLower().Contains(fromLower) ||
                (b.ViaPoints != null && b.ViaPoints.ToLower().Contains(fromLower)));
        }

        if (!string.IsNullOrWhiteSpace(to))
        {
            var toLower = to.Trim().ToLower();
            query = query.Where(b =>
                b.Destination.ToLower().Contains(toLower) ||
                (b.ViaPoints != null && b.ViaPoints.ToLower().Contains(toLower)));
        }

        return await query.OrderBy(b => b.DepartureTime).ToListAsync();
    }

    public async Task<Bus?> GetByIdAsync(int id)
    {
        return await _db.Buses
            .Include(b => b.Stops.OrderBy(s => s.StopOrder))
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<Bus> CreateAsync(Bus bus)
    {
        _db.Buses.Add(bus);
        await _db.SaveChangesAsync();
        return bus;
    }

    public async Task<Bus?> UpdateAsync(Bus bus)
    {
        var existing = await _db.Buses.FindAsync(bus.Id);
        if (existing is null) return null;

        existing.ServiceName = bus.ServiceName;
        existing.ContactNumber = bus.ContactNumber;
        existing.Origin = bus.Origin;
        existing.Destination = bus.Destination;
        existing.ViaPoints = bus.ViaPoints;
        existing.DepartureTime = bus.DepartureTime;
        existing.ReturnTime = bus.ReturnTime;
        existing.IsActive = bus.IsActive;

        await _db.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var bus = await _db.Buses.FindAsync(id);
        if (bus is null) return false;

        _db.Buses.Remove(bus);
        await _db.SaveChangesAsync();
        return true;
    }
}
