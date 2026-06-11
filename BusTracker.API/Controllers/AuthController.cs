using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BusTracker.API.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BusTracker.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _config;

    public AuthController(IConfiguration config)
    {
        _config = config;
    }

    // POST /api/auth/login
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequestDto dto)
    {
        var adminUsername = _config["Admin:Username"];
        var adminPassword = _config["Admin:Password"];

        if (dto.Username != adminUsername || dto.Password != adminPassword)
            return Unauthorized(new { message = "Invalid username or password." });

        var token = GenerateJwtToken(dto.Username);
        return Ok(token);
    }

    // ─── Helper ────────────────────────────────────────────────────────────────

    private LoginResponseDto GenerateJwtToken(string username)
    {
        var jwtKey = _config["Jwt:Key"]!;
        var issuer = _config["Jwt:Issuer"]!;
        var audience = _config["Jwt:Audience"]!;
        var expiryHours = int.Parse(_config["Jwt:ExpiryHours"] ?? "8");

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiry = DateTime.UtcNow.AddHours(expiryHours);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, "Admin"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: expiry,
            signingCredentials: credentials
        );

        return new LoginResponseDto(
            Token: new JwtSecurityTokenHandler().WriteToken(token),
            ExpiresAt: expiry,
            Username: username
        );
    }
}
