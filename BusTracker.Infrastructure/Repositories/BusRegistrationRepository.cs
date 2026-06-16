using BusTracker.Domain;
using BusTracker.Domain.Interfaces;
using BusTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BusTracker.Infrastructure.Repositories;

public class BusRegistrationRepository : IBusRegistrationRepository
{
    private readonly BusTrackerDbContext _db;

    public BusRegistrationRepository(BusTrackerDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<BusRegistration>> GetAllAsync()
    {
        return await _db.BusRegistrations
            .OrderByDescending(r => r.SubmittedAt)
            .ToListAsync();
    }

    public async Task<BusRegistration?> GetByIdAsync(int id)
    {
        return await _db.BusRegistrations.FindAsync(id);
    }

    public async Task<BusRegistration> CreateAsync(BusRegistration registration)
    {
        registration.SubmittedAt = DateTime.UtcNow;
        registration.Status = RegistrationStatus.Pending;
        _db.BusRegistrations.Add(registration);
        await _db.SaveChangesAsync();
        return registration;
    }

    public async Task<BusRegistration?> UpdateStatusAsync(int id, RegistrationStatus status, string? adminRemarks)
    {
        var registration = await _db.BusRegistrations.FindAsync(id);
        if (registration is null) return null;

        // Auto-create active Bus entry if status transitions to Approved
        if (registration.Status != RegistrationStatus.Approved && status == RegistrationStatus.Approved)
        {
            var bus = new Bus
            {
                ServiceName = registration.ServiceName,
                ContactNumber = registration.ContactNumber,
                Origin = registration.Origin,
                Destination = registration.Destination,
                ViaPoints = registration.ViaPoints,
                DepartureTime = registration.DepartureTime,
                ReturnTime = registration.ReturnTime,
                IsActive = true
            };
            _db.Buses.Add(bus);
        }

        registration.Status = status;
        registration.AdminRemarks = adminRemarks;
        await _db.SaveChangesAsync();
        return registration;
    }
}
