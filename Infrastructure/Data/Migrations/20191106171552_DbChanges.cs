using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class DbChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CompletedOn",
                table: "Projects",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartedOn",
                table: "Projects",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Projects",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompletedOn",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "StartedOn",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Projects");
        }
    }
}
