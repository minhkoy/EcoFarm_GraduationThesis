using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoFarm.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ModifyOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RECEIVER_NAME",
                table: "ORDER",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RECEIVER_PHONE",
                table: "ORDER",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RECEIVER_NAME",
                table: "ORDER");

            migrationBuilder.DropColumn(
                name: "RECEIVER_PHONE",
                table: "ORDER");
        }
    }
}
