using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoFarm.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddActivityShortDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SHORT_DESCRIPTION",
                table: "FARMING_PACKAGE_ACTIVITY",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SHORT_DESCRIPTION",
                table: "FARMING_PACKAGE_ACTIVITY");
        }
    }
}
