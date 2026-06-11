using BusTracker.Domain;
using BusTracker.Domain.Interfaces;
using BusTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BusTracker.Infrastructure.Repositories;

public class RouteSuggestionRepository : IRouteSuggestionRepository
{
    private readonly BusTrackerDbContext _db;

    public RouteSuggestionRepository(BusTrackerDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<RouteSuggestion>> GetAllAsync()
    {
        return await _db.RouteSuggestions
            .OrderByDescending(s => s.SubmittedAt)
            .ToListAsync();
    }

    public async Task<RouteSuggestion?> GetByIdAsync(int id)
    {
        return await _db.RouteSuggestions.FindAsync(id);
    }

    public async Task<RouteSuggestion> CreateAsync(RouteSuggestion suggestion)
    {
        suggestion.SubmittedAt = DateTime.UtcNow;
        suggestion.Status = SuggestionStatus.Pending;
        _db.RouteSuggestions.Add(suggestion);
        await _db.SaveChangesAsync();
        return suggestion;
    }

    public async Task<RouteSuggestion?> UpdateStatusAsync(int id, SuggestionStatus status)
    {
        var suggestion = await _db.RouteSuggestions.FindAsync(id);
        if (suggestion is null) return null;

        suggestion.Status = status;
        await _db.SaveChangesAsync();
        return suggestion;
    }
}
