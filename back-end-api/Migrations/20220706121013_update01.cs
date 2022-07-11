using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace back_end_api.Migrations
{
    public partial class update01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__ArrivingF__Fligh__2B3F6F97",
                table: "ArrivingFlights");

            migrationBuilder.DropForeignKey(
                name: "FK__ArrivingF__Stati__2C3393D0",
                table: "ArrivingFlights");

            migrationBuilder.DropForeignKey(
                name: "FK__Departing__Fligh__276EDEB3",
                table: "DepartingFlights");

            migrationBuilder.DropForeignKey(
                name: "FK__Departing__Stati__286302EC",
                table: "DepartingFlights");

            migrationBuilder.DropIndex(
                name: "IX_DepartingFlights_FlightId",
                table: "DepartingFlights");

            migrationBuilder.DropIndex(
                name: "IX_DepartingFlights_StationId",
                table: "DepartingFlights");

            migrationBuilder.DropIndex(
                name: "IX_ArrivingFlights_FlightId",
                table: "ArrivingFlights");

            migrationBuilder.DropIndex(
                name: "IX_ArrivingFlights_StationId",
                table: "ArrivingFlights");

            migrationBuilder.AddColumn<int>(
                name: "FlightInStation",
                table: "Stations",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FlightInStation",
                table: "Stations");

            migrationBuilder.CreateIndex(
                name: "IX_DepartingFlights_FlightId",
                table: "DepartingFlights",
                column: "FlightId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartingFlights_StationId",
                table: "DepartingFlights",
                column: "StationId");

            migrationBuilder.CreateIndex(
                name: "IX_ArrivingFlights_FlightId",
                table: "ArrivingFlights",
                column: "FlightId");

            migrationBuilder.CreateIndex(
                name: "IX_ArrivingFlights_StationId",
                table: "ArrivingFlights",
                column: "StationId");

            migrationBuilder.AddForeignKey(
                name: "FK__ArrivingF__Fligh__2B3F6F97",
                table: "ArrivingFlights",
                column: "FlightId",
                principalTable: "Flights",
                principalColumn: "FlightId");

            migrationBuilder.AddForeignKey(
                name: "FK__ArrivingF__Stati__2C3393D0",
                table: "ArrivingFlights",
                column: "StationId",
                principalTable: "Stations",
                principalColumn: "StationId");

            migrationBuilder.AddForeignKey(
                name: "FK__Departing__Fligh__276EDEB3",
                table: "DepartingFlights",
                column: "FlightId",
                principalTable: "Flights",
                principalColumn: "FlightId");

            migrationBuilder.AddForeignKey(
                name: "FK__Departing__Stati__286302EC",
                table: "DepartingFlights",
                column: "StationId",
                principalTable: "Stations",
                principalColumn: "StationId");
        }
    }
}
