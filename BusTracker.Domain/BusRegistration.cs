namespace BusTracker.Domain;

public enum RegistrationStatus
{
    Pending,
    Approved,
    Rejected
}

/// <summary>
/// Represents an operator's request to register a new bus service.
/// Submitted publicly; approved or rejected by the admin.
/// Once approved, the admin can create the corresponding Bus entry.
/// </summary>
public class BusRegistration
{
    public int Id { get; set; }

    public string ServiceName { get; set; } = string.Empty;
    public string? ContactNumber { get; set; }
    public string Origin { get; set; } = string.Empty;
    public string Destination { get; set; } = string.Empty;
    public string? ViaPoints { get; set; }
    public string DepartureTime { get; set; } = string.Empty;
    public string? ReturnTime { get; set; }

    /// <summary>Name of the person submitting the registration</summary>
    public string SubmittedByName { get; set; } = string.Empty;

    /// <summary>Contact email of the submitter</summary>
    public string SubmittedByEmail { get; set; } = string.Empty;

    public RegistrationStatus Status { get; set; } = RegistrationStatus.Pending;

    public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;

    /// <summary>Admin notes on approval or rejection</summary>
    public string? AdminRemarks { get; set; }
}
