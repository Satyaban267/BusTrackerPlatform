namespace BusTracker.Domain;

/// <summary>
/// Phase 2: Intermediate stop/station for a bus route with per-station timings.
/// Schema is created now; data will be populated when intermediate stations are ready.
/// </summary>
public class BusStop
{
    public int Id { get; set; }

    public int BusId { get; set; }
    public Bus Bus { get; set; } = null!;

    public string StationName { get; set; } = string.Empty;

    /// <summary>Scheduled arrival time at this station e.g. "7:30 AM"</summary>
    public string? ArrivalTime { get; set; }

    /// <summary>Scheduled departure time from this station e.g. "7:35 AM"</summary>
    public string? DepartureTime { get; set; }

    /// <summary>Sequence order of the stop along the route (1 = first stop after origin)</summary>
    public int StopOrder { get; set; }
}
