using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BarberShopAPI.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "Barbers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                table: "Barbers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Barbers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "Barbers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "Barbers");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTime",
                table: "Barbers");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Barbers");

            migrationBuilder.DropColumn(
                name: "Token",
                table: "Barbers");
        }
    }
}
