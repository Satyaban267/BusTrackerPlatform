using BusTracker.API.DTOs;
using BusTracker.Domain;
using BusTracker.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusTracker.API.Controllers;

[ApiController]
[Route("api/suggestions")]
public class RouteSuggestionsController : ControllerBase
{
    private readonly IRouteSuggestionRepository _repo;

    public RouteSuggestionsController(IRouteSuggestionRepository repo)
    {
        _repo = repo;
    }

    // POST /api/suggestions — Public: user submits a route suggestion
    [HttpPost]
    public async Task<IActionResult> SubmitSuggestion([FromBody] RouteSuggestionCreateDto dto)
    {
        var suggestion = new RouteSuggestion
        {
            SuggestedFrom = dto.SuggestedFrom,
            SuggestedTo = dto.SuggestedTo,
            ViaPoints = dto.ViaPoints,
            Reason = dto.Reason,
            SubmittedByName = dto.SubmittedByName,
            SubmittedByEmail = dto.SubmittedByEmail
        };

        var created = await _repo.CreateAsync(suggestion);
        return CreatedAtAction(nameof(GetSuggestion), new { id = created.Id }, ToDto(created));
    }

    // GET /api/suggestions — Admin only: view all suggestions
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetSuggestions()
    {
        var all = await _repo.GetAllAsync();
        return Ok(all.Select(ToDto));
    }

    // GET /api/suggestions/{id} — Admin only
    [HttpGet("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetSuggestion(int id)
    {
        var suggestion = await _repo.GetByIdAsync(id);
        if (suggestion is null) return NotFound(new { message = $"Suggestion {id} not found." });
        return Ok(ToDto(suggestion));
    }

    // PUT /api/suggestions/{id}/status — Admin only: mark reviewed or dismissed
    [HttpPut("{id:int}/status")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateSuggestionStatusDto dto)
    {
        if (!Enum.TryParse<SuggestionStatus>(dto.Status, ignoreCase: true, out var status))
            return BadRequest(new { message = "Status must be 'Reviewed' or 'Dismissed'." });

        var updated = await _repo.UpdateStatusAsync(id, status);
        if (updated is null) return NotFound(new { message = $"Suggestion {id} not found." });

        return Ok(ToDto(updated));
    }

    // ─── Helper ────────────────────────────────────────────────────────────────

    private static RouteSuggestionDto ToDto(RouteSuggestion s) => new(
        s.Id,
        s.SuggestedFrom,
        s.SuggestedTo,
        s.ViaPoints,
        s.Reason,
        s.SubmittedByName,
        s.SubmittedByEmail,
        s.Status.ToString(),
        s.SubmittedAt
    );
}
