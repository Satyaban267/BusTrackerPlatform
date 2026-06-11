namespace BusTracker.Domain.Interfaces;

public interface IBusRegistrationRepository
{
    Task<IEnumerable<BusRegistration>> GetAllAsync();

    Task<BusRegistration?> GetByIdAsync(int id);

    Task<BusRegistration> CreateAsync(BusRegistration registration);

    Task<BusRegistration?> UpdateStatusAsync(int id, RegistrationStatus status, string? adminRemarks);
}
