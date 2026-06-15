using System.ComponentModel.DataAnnotations;

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
    [Required, MaxLength(100)] string ServiceName,
    [Phone, MaxLength(20)] string? ContactNumber,
    [Required, MaxLength(100)] string Origin,
    [Required, MaxLength(100)] string Destination,
    [MaxLength(500)] string? ViaPoints,
    [Required, MaxLength(20)] string DepartureTime,
    [MaxLength(20)] string? ReturnTime
);

// ─── Bus Registration DTOs ─────────────────────────────────────────────────────

public record BusRegistrationCreateDto(
    [Required, MaxLength(100)] string ServiceName,
    [Phone, MaxLength(20)] string? ContactNumber,
    [Required, MaxLength(100)] string Origin,
    [Required, MaxLength(100)] string Destination,
    [MaxLength(500)] string? ViaPoints,
    [Required, MaxLength(20)] string DepartureTime,
    [MaxLength(20)] string? ReturnTime,
    [Required, MaxLength(100)] string SubmittedByName,
    [Required, EmailAddress, MaxLength(200)] string SubmittedByEmail
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
    [Required] string Status,       // "Approved" | "Rejected"
    [MaxLength(500)] string? AdminRemarks
);

// ─── Route Suggestion DTOs ─────────────────────────────────────────────────────

public record RouteSuggestionCreateDto(
    [Required, MaxLength(100)] string SuggestedFrom,
    [Required, MaxLength(100)] string SuggestedTo,
    [MaxLength(500)] string? ViaPoints,
    [MaxLength(1000)] string? Reason,
    [Required, MaxLength(100)] string SubmittedByName,
    [EmailAddress, MaxLength(200)] string? SubmittedByEmail
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
    [Required] string Status   // "Reviewed" | "Dismissed"
);

// ─── Auth DTOs ─────────────────────────────────────────────────────────────────

public record LoginRequestDto(
    [Required, MaxLength(100)] string Username,
    [Required, MaxLength(100)] string Password
);

public record LoginResponseDto(
    string Token,
    DateTime ExpiresAt,
    string Username
);
