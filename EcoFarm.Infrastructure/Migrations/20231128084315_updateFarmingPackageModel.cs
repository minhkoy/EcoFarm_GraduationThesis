using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoFarm.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateFarmingPackageModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "APPROVE_OR_REJECT_BY",
                table: "FARMING_PACKAGE",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "APPROVE_OR_REJECT_TIME",
                table: "FARMING_PACKAGE",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "APPROVE_OR_REJECT_BY",
                table: "FARMING_PACKAGE");

            migrationBuilder.DropColumn(
                name: "APPROVE_OR_REJECT_TIME",
                table: "FARMING_PACKAGE");
        }
    }
}
