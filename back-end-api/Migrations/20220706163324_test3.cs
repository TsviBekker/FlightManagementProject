using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace back_end_api.Migrations
{
    public partial class test3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Stations_StationId",
                table: "Flights");

            migrationBuilder.DropForeignKey(
                name: "FK_Stations_Flights_FlightId",
                table: "Stations");

            migrationBuilder.DropIndex(
                name: "IX_Stations_FlightId",
                table: "Stations");

            migrationBuilder.DropIndex(
                name: "IX_Flights_StationId",
                table: "Flights");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Stations_FlightId",
                table: "Stations",
                column: "FlightId",
                unique: true,
                filter: "[FlightId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_StationId",
                table: "Flights",
                column: "StationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_Stations_StationId",
                table: "Flights",
                column: "StationId",
                principalTable: "Stations",
                principalColumn: "StationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stations_Flights_FlightId",
                table: "Stations",
                column: "FlightId",
                principalTable: "Flights",
                principalColumn: "FlightId");
        }
    }
}
