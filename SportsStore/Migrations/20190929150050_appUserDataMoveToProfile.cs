using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SportsStore.Migrations
{
    public partial class appUserDataMoveToProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "CustomerProfiles",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "CustomerProfiles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "CustomerProfiles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "CustomerProfiles",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "CustomerProfiles");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "CustomerProfiles");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "CustomerProfiles");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "CustomerProfiles");
        }
    }
}
