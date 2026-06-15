using BusTracker.API.DTOs;
using BusTracker.Domain;
using BusTracker.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusTracker.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BusesController : ControllerBase
{
    private readonly IBusRepository _repo;

    public BusesController(IBusRepository repo)
    {
        _repo = repo;
    }

    // GET /api/buses?from=Narsinghpur&to=Bhubaneswar
    [HttpGet]
    public async Task<IActionResult> GetBuses([FromQuery] string? from, [FromQuery] string? to)
    {
        var buses = await _repo.GetAllAsync(from, to);
        return Ok(buses.Select(ToDto));
    }

    // GET /api/buses/{id}
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetBus(int id)
    {
        var bus = await _repo.GetByIdAsync(id);
        if (bus is null) return NotFound(new { message = $"Bus with ID {id} not found." });
        return Ok(ToDto(bus));
    }

    // POST /api/buses — Admin only
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateBus([FromBody] BusCreateDto dto)
    {
        var bus = new Bus
        {
            ServiceName = dto.ServiceName,
            ContactNumber = dto.ContactNumber,
            Origin = dto.Origin,
            Destination = dto.Destination,
            ViaPoints = dto.ViaPoints,
            DepartureTime = dto.DepartureTime,
            ReturnTime = dto.ReturnTime,
            IsActive = true
        };

        var created = await _repo.CreateAsync(bus);
        return CreatedAtAction(nameof(GetBus), new { id = created.Id }, ToDto(created));
    }

    // PUT /api/buses/{id} — Admin only
    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateBus(int id, [FromBody] BusCreateDto dto)
    {
        var bus = new Bus
        {
            Id = id,
            ServiceName = dto.ServiceName,
            ContactNumber = dto.ContactNumber,
            Origin = dto.Origin,
            Destination = dto.Destination,
            ViaPoints = dto.ViaPoints,
            DepartureTime = dto.DepartureTime,
            ReturnTime = dto.ReturnTime
        };

        var updated = await _repo.UpdateAsync(bus);
        if (updated is null) return NotFound(new { message = $"Bus with ID {id} not found." });
        return Ok(ToDto(updated));
    }

    // DELETE /api/buses/{id} — Admin only
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteBus(int id)
    {
        var deleted = await _repo.DeleteAsync(id);
        if (!deleted) return NotFound(new { message = $"Bus with ID {id} not found." });
        return NoContent();
    }

    // ─── Helper ────────────────────────────────────────────────────────────────

    private static BusDto ToDto(Bus b) => new(
        b.Id,
        b.ServiceName,
        b.ContactNumber,
        b.Origin,
        b.Destination,
        b.ViaPoints,
        b.DepartureTime,
        b.ReturnTime,
        b.IsActive,
        b.Stops.Select(s => new BusStopDto(s.Id, s.StationName, s.ArrivalTime, s.DepartureTime, s.StopOrder))
    );
}