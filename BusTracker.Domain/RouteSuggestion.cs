namespace BusTracker.Domain;

public enum SuggestionStatus
{
    Pending,
    Reviewed,
    Dismissed
}

/// <summary>
/// A public user's suggestion for a new bus route.
/// Admin reviews these and may act on them (e.g., contact an operator).
/// </summary>
public class RouteSuggestion
{
    public int Id { get; set; }

    public string SuggestedFrom { get; set; } = string.Empty;
    public string SuggestedTo { get; set; } = string.Empty;

    /// <summary>Optional via points for the suggested route</summary>
    public string? ViaPoints { get; set; }

    /// <summary>Why the user thinks this route is needed</summary>
    public string? Reason { get; set; }

    public string SubmittedByName { get; set; } = string.Empty;

    public string? SubmittedByEmail { get; set; }

    public SuggestionStatus Status { get; set; } = SuggestionStatus.Pending;

    public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
}
