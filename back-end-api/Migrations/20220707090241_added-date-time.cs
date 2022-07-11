using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace back_end_api.Migrations
{
    public partial class addeddatetime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DepartedAt",
                table: "DepartingFlights",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ArrivedAt",
                table: "ArrivingFlights",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DepartedAt",
                table: "DepartingFlights");

            migrationBuilder.DropColumn(
                name: "ArrivedAt",
                table: "ArrivingFlights");
        }
    }
}
