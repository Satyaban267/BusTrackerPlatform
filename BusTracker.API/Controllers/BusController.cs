using Microsoft.AspNetCore.Mvc;
using BusTracker.Domain;

namespace BusTracker.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BusesController : ControllerBase
{
    [HttpGet]
    public IActionResult GetBuses()
    {
        // Hardcoded dummy data for our "walking skeleton"
        var buses = new List<Bus>
        {
            new Bus { Id = 1, OperatorName = "Express Lines", Route = "NY to Boston", GeneralPrice = 35.50m },
            new Bus { Id = 2, OperatorName = "City Hopper", Route = "NY to Philly", GeneralPrice = 20.00m },
            new Bus { Id = 3, OperatorName = "Night Rider", Route = "Boston to DC", GeneralPrice = 55.00m }
        };

        return Ok(buses);
    }
}