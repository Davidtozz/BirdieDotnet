using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BirdieDotnetAPI.Migrations
{
    /// <inheritdoc />
    public partial class RefreshTokenModelUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Tokens");

            migrationBuilder.DropColumn(
                name: "RevokedAt",
                table: "Tokens");

            migrationBuilder.RenameColumn(
                name: "Refresh",
                table: "Tokens",
                newName: "JwtId");

            migrationBuilder.RenameColumn(
                name: "Expires",
                table: "Tokens",
                newName: "ExpirationDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "JwtId",
                table: "Tokens",
                newName: "Refresh");

            migrationBuilder.RenameColumn(
                name: "ExpirationDate",
                table: "Tokens",
                newName: "Expires");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Tokens",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "RevokedAt",
                table: "Tokens",
                type: "datetime(6)",
                nullable: true);
        }
    }
}
