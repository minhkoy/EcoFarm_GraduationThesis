using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoFarm.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderTimeline : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ADDRESS",
                table: "USER_ADDRESS");

            migrationBuilder.CreateTable(
                name: "ORDER_TIMELINE",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ORDER_ID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    STATUS = table.Column<int>(type: "int", nullable: false),
                    TIME = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VERSION = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CREATED_TIME = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CREATED_BY = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MODIFIED_TIME = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MODIFIED_BY = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IS_ACTIVE = table.Column<bool>(type: "bit", nullable: false),
                    IS_DELETE = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ORDER_TIMELINE", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ORDER_TIMELINE_ORDER_ORDER_ID",
                        column: x => x.ORDER_ID,
                        principalTable: "ORDER",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ORDER_TIMELINE_ORDER_ID",
                table: "ORDER_TIMELINE",
                column: "ORDER_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ORDER_TIMELINE");

            migrationBuilder.AddColumn<string>(
                name: "ADDRESS",
                table: "USER_ADDRESS",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
