namespace BusTracker.Domain;

public class Bus
{
    public int Id { get; set; }

    /// <summary>Bus/service name e.g. "Giribala", "Jagannath Bus"</summary>
    public string ServiceName { get; set; } = string.Empty;

    /// <summary>Operator contact number e.g. "9861891406", "+919777510028"</summary>
    public string? ContactNumber { get; set; }

    /// <summary>Origin/departure location e.g. "Narsinghpur", "Bhubaneswar"</summary>
    public string Origin { get; set; } = string.Empty;

    /// <summary>Final destination e.g. "Bhubaneswar", "Anugul"</summary>
    public string Destination { get; set; } = string.Empty;

    /// <summary>Optional intermediate stops e.g. "Khordha T-Bridge, Narsinghpur, Rusipada"</summary>
    public string? ViaPoints { get; set; }

    /// <summary>Departure time string e.g. "6:10 AM"</summary>
    public string DepartureTime { get; set; } = string.Empty;

    /// <summary>Optional return time e.g. "2:15 PM" — null if no return service</summary>
    public string? ReturnTime { get; set; }

    public bool IsActive { get; set; } = true;

    /// <summary>Phase 2: intermediate stops with per-station timings</summary>
    public ICollection<BusStop> Stops { get; set; } = [];
}
