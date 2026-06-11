namespace BusTracker.Domain.Interfaces;

public interface IBusRepository
{
    /// <summary>Returns all active buses, optionally filtered by origin/destination keywords.</summary>
    Task<IEnumerable<Bus>> GetAllAsync(string? from = null, string? to = null);

    Task<Bus?> GetByIdAsync(int id);

    Task<Bus> CreateAsync(Bus bus);

    Task<Bus?> UpdateAsync(Bus bus);

    Task<bool> DeleteAsync(int id);
}
