using System.Text;
using BusTracker.API.Middleware;
using BusTracker.Domain.Interfaces;
using BusTracker.Infrastructure.Data;
using BusTracker.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// ── Database ────────────────────────────────────────────────────────────────────
// Use PostgreSQL in Production, SQLite in Development
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;

if (builder.Environment.IsProduction())
{
    builder.Services.AddDbContext<BusTrackerDbContext>(options =>
        options.UseNpgsql(connectionString));
}
else
{
    builder.Services.AddDbContext<BusTrackerDbContext>(options =>
        options.UseSqlite(connectionString));
}

// ── Repository DI ───────────────────────────────────────────────────────────────
builder.Services.AddScoped<IBusRepository, BusRepository>();
builder.Services.AddScoped<IBusRegistrationRepository, BusRegistrationRepository>();
builder.Services.AddScoped<IRouteSuggestionRepository, RouteSuggestionRepository>();

// ── JWT Authentication ──────────────────────────────────────────────────────────
var jwtKey = builder.Configuration["Jwt:Key"]!;
var jwtIssuer = builder.Configuration["Jwt:Issuer"]!;
var jwtAudience = builder.Configuration["Jwt:Audience"]!;

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

builder.Services.AddAuthorization();

// ── Controllers & OpenAPI ───────────────────────────────────────────────────────
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// ── Health Checks ───────────────────────────────────────────────────────────────
builder.Services.AddHealthChecks();

// ── CORS ────────────────────────────────────────────────────────────────────────
var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>() ?? ["http://localhost:4200"];

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins(allowedOrigins)
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// ═══════════════════════════════════════════════════════════════════════════════
var app = builder.Build();

// ── Auto-migrate on startup (development only) ──────────────────────────────────
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<BusTrackerDbContext>();
    db.Database.Migrate();
}

// ── Global Exception Handling ───────────────────────────────────────────────────
app.UseMiddleware<ExceptionHandlingMiddleware>();

// ── Pipeline ────────────────────────────────────────────────────────────────────
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("AllowAngularApp");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapHealthChecks("/health");

app.Run();
