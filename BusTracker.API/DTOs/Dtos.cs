namespace BusTracker.API.DTOs;

// ─── Bus DTOs ──────────────────────────────────────────────────────────────────

public record BusDto(
    int Id,
    string ServiceName,
    string? ContactNumber,
    string Origin,
    string Destination,
    string? ViaPoints,
    string DepartureTime,
    string? ReturnTime,
    bool IsActive,
    IEnumerable<BusStopDto> Stops
);

public record BusStopDto(
    int Id,
    string StationName,
    string? ArrivalTime,
    string? DepartureTime,
    int StopOrder
);

public record BusCreateDto(
    string ServiceName,
    string? ContactNumber,
    string Origin,
    string Destination,
    string? ViaPoints,
    string DepartureTime,
    string? ReturnTime
);

// ─── Bus Registration DTOs ─────────────────────────────────────────────────────

public record BusRegistrationCreateDto(
    string ServiceName,
    string? ContactNumber,
    string Origin,
    string Destination,
    string? ViaPoints,
    string DepartureTime,
    string? ReturnTime,
    string SubmittedByName,
    string SubmittedByEmail
);

public record BusRegistrationDto(
    int Id,
    string ServiceName,
    string? ContactNumber,
    string Origin,
    string Destination,
    string? ViaPoints,
    string DepartureTime,
    string? ReturnTime,
    string SubmittedByName,
    string SubmittedByEmail,
    string Status,
    DateTime SubmittedAt,
    string? AdminRemarks
);

public record UpdateRegistrationStatusDto(
    string Status,       // "Approved" | "Rejected"
    string? AdminRemarks
);

// ─── Route Suggestion DTOs ─────────────────────────────────────────────────────

public record RouteSuggestionCreateDto(
    string SuggestedFrom,
    string SuggestedTo,
    string? ViaPoints,
    string? Reason,
    string SubmittedByName,
    string? SubmittedByEmail
);

public record RouteSuggestionDto(
    int Id,
    string SuggestedFrom,
    string SuggestedTo,
    string? ViaPoints,
    string? Reason,
    string SubmittedByName,
    string? SubmittedByEmail,
    string Status,
    DateTime SubmittedAt
);

public record UpdateSuggestionStatusDto(
    string Status   // "Reviewed" | "Dismissed"
);

// ─── Auth DTOs ─────────────────────────────────────────────────────────────────

public record LoginRequestDto(
    string Username,
    string Password
);

public record LoginResponseDto(
    string Token,
    DateTime ExpiresAt,
    string Username
);
