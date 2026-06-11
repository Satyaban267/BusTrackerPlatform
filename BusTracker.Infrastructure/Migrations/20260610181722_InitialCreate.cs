using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BusTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Buses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ServiceName = table.Column<string>(type: "TEXT", nullable: false),
                    ContactNumber = table.Column<string>(type: "TEXT", nullable: true),
                    Origin = table.Column<string>(type: "TEXT", nullable: false),
                    Destination = table.Column<string>(type: "TEXT", nullable: false),
                    ViaPoints = table.Column<string>(type: "TEXT", nullable: true),
                    DepartureTime = table.Column<string>(type: "TEXT", nullable: false),
                    ReturnTime = table.Column<string>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BusRegistrations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ServiceName = table.Column<string>(type: "TEXT", nullable: false),
                    ContactNumber = table.Column<string>(type: "TEXT", nullable: true),
                    Origin = table.Column<string>(type: "TEXT", nullable: false),
                    Destination = table.Column<string>(type: "TEXT", nullable: false),
                    ViaPoints = table.Column<string>(type: "TEXT", nullable: true),
                    DepartureTime = table.Column<string>(type: "TEXT", nullable: false),
                    ReturnTime = table.Column<string>(type: "TEXT", nullable: true),
                    SubmittedByName = table.Column<string>(type: "TEXT", nullable: false),
                    SubmittedByEmail = table.Column<string>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    SubmittedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    AdminRemarks = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusRegistrations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RouteSuggestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SuggestedFrom = table.Column<string>(type: "TEXT", nullable: false),
                    SuggestedTo = table.Column<string>(type: "TEXT", nullable: false),
                    ViaPoints = table.Column<string>(type: "TEXT", nullable: true),
                    Reason = table.Column<string>(type: "TEXT", nullable: true),
                    SubmittedByName = table.Column<string>(type: "TEXT", nullable: false),
                    SubmittedByEmail = table.Column<string>(type: "TEXT", nullable: true),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    SubmittedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RouteSuggestions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BusStops",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BusId = table.Column<int>(type: "INTEGER", nullable: false),
                    StationName = table.Column<string>(type: "TEXT", nullable: false),
                    ArrivalTime = table.Column<string>(type: "TEXT", nullable: true),
                    DepartureTime = table.Column<string>(type: "TEXT", nullable: true),
                    StopOrder = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusStops", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusStops_Buses_BusId",
                        column: x => x.BusId,
                        principalTable: "Buses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Buses",
                columns: new[] { "Id", "ContactNumber", "DepartureTime", "Destination", "IsActive", "Origin", "ReturnTime", "ServiceName", "ViaPoints" },
                values: new object[,]
                {
                    { 1, "9861891406", "6:10 AM", "Bhubaneswar", true, "Narsinghpur", "2:15 PM", "Giribala", null },
                    { 2, "09776359935", "10:45 AM", "Narsinghpur", true, "Bhubaneswar", null, "Pitabali", null },
                    { 3, "+919777510028", "11:30 AM", "Sagar", true, "Bhubaneswar", null, "Shibani", "Kanpur" },
                    { 4, null, "9:00 AM", "Anugul", true, "Bhubaneswar", null, "Dilkhus", "Khordha T-Bridge, Narsinghpur, Rusipada" },
                    { 5, "9668982220", "4:45 PM", "Narsinghpur", true, "Cuttack", null, "Subhadra Bus", null },
                    { 6, "+919776353077", "4:15 AM", "Kamaladiha", true, "Bhubaneswar", "11:30 AM", "Jagannath Bus", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BusStops_BusId",
                table: "BusStops",
                column: "BusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusRegistrations");

            migrationBuilder.DropTable(
                name: "BusStops");

            migrationBuilder.DropTable(
                name: "RouteSuggestions");

            migrationBuilder.DropTable(
                name: "Buses");
        }
    }
}
