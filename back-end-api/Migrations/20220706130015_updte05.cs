using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace back_end_api.Migrations
{
    public partial class updte05 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FlightInStation",
                table: "Stations",
                newName: "FlightId");

            migrationBuilder.CreateIndex(
                name: "IX_Stations_FlightId",
                table: "Stations",
                column: "FlightId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stations_Flights_FlightId",
                table: "Stations",
                column: "FlightId",
                principalTable: "Flights",
                principalColumn: "FlightId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stations_Flights_FlightId",
                table: "Stations");

            migrationBuilder.DropIndex(
                name: "IX_Stations_FlightId",
                table: "Stations");

            migrationBuilder.RenameColumn(
                name: "FlightId",
                table: "Stations",
                newName: "FlightInStation");
        }
    }
}
