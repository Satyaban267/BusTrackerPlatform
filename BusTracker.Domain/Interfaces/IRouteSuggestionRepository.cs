namespace BusTracker.Domain.Interfaces;

public interface IRouteSuggestionRepository
{
    Task<IEnumerable<RouteSuggestion>> GetAllAsync();

    Task<RouteSuggestion?> GetByIdAsync(int id);

    Task<RouteSuggestion> CreateAsync(RouteSuggestion suggestion);

    Task<RouteSuggestion?> UpdateStatusAsync(int id, SuggestionStatus status);
}
