using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoFarm.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPackageAvatar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AVATAR_URL",
                table: "FARMING_PACKAGE",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AVATAR_URL",
                table: "FARMING_PACKAGE");
        }
    }
}
