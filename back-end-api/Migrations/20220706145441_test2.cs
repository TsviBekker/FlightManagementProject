using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace back_end_api.Migrations
{
    public partial class test2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StationId",
                table: "Flights",
                type: "int",
                nullable: true);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Stations_StationId",
                table: "Flights");

            migrationBuilder.DropIndex(
                name: "IX_Flights_StationId",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "StationId",
                table: "Flights");
        }
    }
}
