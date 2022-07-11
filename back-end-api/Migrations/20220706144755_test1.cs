using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace back_end_api.Migrations
{
    public partial class test1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Stations_FlightId",
                table: "Stations");

            migrationBuilder.CreateIndex(
                name: "IX_Stations_FlightId",
                table: "Stations",
                column: "FlightId",
                unique: true,
                filter: "[FlightId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Stations_FlightId",
                table: "Stations");

            migrationBuilder.CreateIndex(
                name: "IX_Stations_FlightId",
                table: "Stations",
                column: "FlightId");
        }
    }
}
