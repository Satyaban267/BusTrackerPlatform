using BusTracker.API.DTOs;
using BusTracker.Domain;
using BusTracker.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusTracker.API.Controllers;

[ApiController]
[Route("api/registrations")]
public class BusRegistrationsController : ControllerBase
{
    private readonly IBusRegistrationRepository _repo;

    public BusRegistrationsController(IBusRegistrationRepository repo)
    {
        _repo = repo;
    }

    // POST /api/registrations — Public: operator submits a new bus registration
    [HttpPost]
    public async Task<IActionResult> SubmitRegistration([FromBody] BusRegistrationCreateDto dto)
    {
        var registration = new BusRegistration
        {
            ServiceName = dto.ServiceName,
            ContactNumber = dto.ContactNumber,
            Origin = dto.Origin,
            Destination = dto.Destination,
            ViaPoints = dto.ViaPoints,
            DepartureTime = dto.DepartureTime,
            ReturnTime = dto.ReturnTime,
            SubmittedByName = dto.SubmittedByName,
            SubmittedByEmail = dto.SubmittedByEmail
        };

        var created = await _repo.CreateAsync(registration);
        return CreatedAtAction(nameof(GetRegistration), new { id = created.Id }, ToDto(created));
    }

    // GET /api/registrations — Admin only: view all submissions
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetRegistrations()
    {
        var all = await _repo.GetAllAsync();
        return Ok(all.Select(ToDto));
    }

    // GET /api/registrations/{id} — Admin only
    [HttpGet("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetRegistration(int id)
    {
        var reg = await _repo.GetByIdAsync(id);
        if (reg is null) return NotFound(new { message = $"Registration {id} not found." });
        return Ok(ToDto(reg));
    }

    // PUT /api/registrations/{id}/status — Admin only: approve or reject
    [HttpPut("{id:int}/status")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateRegistrationStatusDto dto)
    {
        if (!Enum.TryParse<RegistrationStatus>(dto.Status, ignoreCase: true, out var status))
            return BadRequest(new { message = "Status must be 'Approved' or 'Rejected'." });

        var updated = await _repo.UpdateStatusAsync(id, status, dto.AdminRemarks);
        if (updated is null) return NotFound(new { message = $"Registration {id} not found." });

        return Ok(ToDto(updated));
    }

    // ─── Helper ────────────────────────────────────────────────────────────────

    private static BusRegistrationDto ToDto(BusRegistration r) => new(
        r.Id,
        r.ServiceName,
        r.ContactNumber,
        r.Origin,
        r.Destination,
        r.ViaPoints,
        r.DepartureTime,
        r.ReturnTime,
        r.SubmittedByName,
        r.SubmittedByEmail,
        r.Status.ToString(),
        r.SubmittedAt,
        r.AdminRemarks
    );
}
