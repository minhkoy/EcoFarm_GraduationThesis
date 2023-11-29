using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoFarm.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class rework_domain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ACTIVITY_MEDIA_FarmingPackageActivity_ACTIVITY_ID",
                table: "ACTIVITY_MEDIA");

            migrationBuilder.DropForeignKey(
                name: "FK_FarmingPackageActivity_FARMING_PACKAGE_PACKAGE_ID",
                table: "FarmingPackageActivity");

            migrationBuilder.DropForeignKey(
                name: "FK_USER_ACTIVITY_COMMENT_FarmingPackageActivity_ACTIVITY_ID",
                table: "USER_ACTIVITY_COMMENT");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FarmingPackageActivity",
                table: "FarmingPackageActivity");

            migrationBuilder.DropColumn(
                name: "DISCOUNT_END",
                table: "FARMING_PACKAGE");

            migrationBuilder.DropColumn(
                name: "DISCOUNT_PRICE",
                table: "FARMING_PACKAGE");

            migrationBuilder.RenameTable(
                name: "FarmingPackageActivity",
                newName: "FARMING_PACKAGE_ACTIVITY");

            migrationBuilder.RenameColumn(
                name: "SERVICE_TYPE",
                table: "FARMING_PACKAGE",
                newName: "PACKAGE_TYPE");

            migrationBuilder.RenameColumn(
                name: "DISCOUNT_START",
                table: "FARMING_PACKAGE",
                newName: "CLOSE_REGISTER_TIME");

            migrationBuilder.RenameIndex(
                name: "IX_FarmingPackageActivity_PACKAGE_ID",
                table: "FARMING_PACKAGE_ACTIVITY",
                newName: "IX_FARMING_PACKAGE_ACTIVITY_PACKAGE_ID");

            migrationBuilder.AddColumn<int>(
                name: "CURRENCY",
                table: "USER_REGISTER_PACKAGE",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PRICE",
                table: "USER_REGISTER_PACKAGE",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "REGISTER_TIME",
                table: "USER_REGISTER_PACKAGE",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AVATAR_URL",
                table: "SELLER_ENTERPRISE",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CURRENCY",
                table: "FARMING_PACKAGE",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IS_AUTO_CLOSE_REGISTER",
                table: "FARMING_PACKAGE",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LATEST_GENERATED_TOKEN",
                table: "ACCOUNT",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FARMING_PACKAGE_ACTIVITY",
                table: "FARMING_PACKAGE_ACTIVITY",
                column: "ID");

            migrationBuilder.CreateTable(
                name: "NOTIFICATION",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FROM_ACCOUNT_ID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    FROM_ACCOUNT_TYPE = table.Column<int>(type: "int", nullable: true),
                    CONTENT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OBJECT_TYPE = table.Column<int>(type: "int", nullable: true),
                    ACTION_TYPE = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_NOTIFICATION", x => x.ID);
                    table.ForeignKey(
                        name: "FK_NOTIFICATION_ACCOUNT_FROM_ACCOUNT_ID",
                        column: x => x.FROM_ACCOUNT_ID,
                        principalTable: "ACCOUNT",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "NOTIFICATION_ACCOUNT",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TO_ACCOUNT_ID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TO_ACCOUNT_TYPE = table.Column<int>(type: "int", nullable: true),
                    NOTIFICATION_ID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IS_READ = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_NOTIFICATION_ACCOUNT", x => x.ID);
                    table.ForeignKey(
                        name: "FK_NOTIFICATION_ACCOUNT_ACCOUNT_TO_ACCOUNT_ID",
                        column: x => x.TO_ACCOUNT_ID,
                        principalTable: "ACCOUNT",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_NOTIFICATION_ACCOUNT_NOTIFICATION_NOTIFICATION_ID",
                        column: x => x.NOTIFICATION_ID,
                        principalTable: "NOTIFICATION",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_NOTIFICATION_FROM_ACCOUNT_ID",
                table: "NOTIFICATION",
                column: "FROM_ACCOUNT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_NOTIFICATION_ACCOUNT_NOTIFICATION_ID",
                table: "NOTIFICATION_ACCOUNT",
                column: "NOTIFICATION_ID");

            migrationBuilder.CreateIndex(
                name: "IX_NOTIFICATION_ACCOUNT_TO_ACCOUNT_ID",
                table: "NOTIFICATION_ACCOUNT",
                column: "TO_ACCOUNT_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ACTIVITY_MEDIA_FARMING_PACKAGE_ACTIVITY_ACTIVITY_ID",
                table: "ACTIVITY_MEDIA",
                column: "ACTIVITY_ID",
                principalTable: "FARMING_PACKAGE_ACTIVITY",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_FARMING_PACKAGE_ACTIVITY_FARMING_PACKAGE_PACKAGE_ID",
                table: "FARMING_PACKAGE_ACTIVITY",
                column: "PACKAGE_ID",
                principalTable: "FARMING_PACKAGE",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_USER_ACTIVITY_COMMENT_FARMING_PACKAGE_ACTIVITY_ACTIVITY_ID",
                table: "USER_ACTIVITY_COMMENT",
                column: "ACTIVITY_ID",
                principalTable: "FARMING_PACKAGE_ACTIVITY",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ACTIVITY_MEDIA_FARMING_PACKAGE_ACTIVITY_ACTIVITY_ID",
                table: "ACTIVITY_MEDIA");

            migrationBuilder.DropForeignKey(
                name: "FK_FARMING_PACKAGE_ACTIVITY_FARMING_PACKAGE_PACKAGE_ID",
                table: "FARMING_PACKAGE_ACTIVITY");

            migrationBuilder.DropForeignKey(
                name: "FK_USER_ACTIVITY_COMMENT_FARMING_PACKAGE_ACTIVITY_ACTIVITY_ID",
                table: "USER_ACTIVITY_COMMENT");

            migrationBuilder.DropTable(
                name: "NOTIFICATION_ACCOUNT");

            migrationBuilder.DropTable(
                name: "NOTIFICATION");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FARMING_PACKAGE_ACTIVITY",
                table: "FARMING_PACKAGE_ACTIVITY");

            migrationBuilder.DropColumn(
                name: "CURRENCY",
                table: "USER_REGISTER_PACKAGE");

            migrationBuilder.DropColumn(
                name: "PRICE",
                table: "USER_REGISTER_PACKAGE");

            migrationBuilder.DropColumn(
                name: "REGISTER_TIME",
                table: "USER_REGISTER_PACKAGE");

            migrationBuilder.DropColumn(
                name: "AVATAR_URL",
                table: "SELLER_ENTERPRISE");

            migrationBuilder.DropColumn(
                name: "CURRENCY",
                table: "FARMING_PACKAGE");

            migrationBuilder.DropColumn(
                name: "IS_AUTO_CLOSE_REGISTER",
                table: "FARMING_PACKAGE");

            migrationBuilder.DropColumn(
                name: "LATEST_GENERATED_TOKEN",
                table: "ACCOUNT");

            migrationBuilder.RenameTable(
                name: "FARMING_PACKAGE_ACTIVITY",
                newName: "FarmingPackageActivity");

            migrationBuilder.RenameColumn(
                name: "PACKAGE_TYPE",
                table: "FARMING_PACKAGE",
                newName: "SERVICE_TYPE");

            migrationBuilder.RenameColumn(
                name: "CLOSE_REGISTER_TIME",
                table: "FARMING_PACKAGE",
                newName: "DISCOUNT_START");

            migrationBuilder.RenameIndex(
                name: "IX_FARMING_PACKAGE_ACTIVITY_PACKAGE_ID",
                table: "FarmingPackageActivity",
                newName: "IX_FarmingPackageActivity_PACKAGE_ID");

            migrationBuilder.AddColumn<DateTime>(
                name: "DISCOUNT_END",
                table: "FARMING_PACKAGE",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DISCOUNT_PRICE",
                table: "FARMING_PACKAGE",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FarmingPackageActivity",
                table: "FarmingPackageActivity",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ACTIVITY_MEDIA_FarmingPackageActivity_ACTIVITY_ID",
                table: "ACTIVITY_MEDIA",
                column: "ACTIVITY_ID",
                principalTable: "FarmingPackageActivity",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_FarmingPackageActivity_FARMING_PACKAGE_PACKAGE_ID",
                table: "FarmingPackageActivity",
                column: "PACKAGE_ID",
                principalTable: "FARMING_PACKAGE",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_USER_ACTIVITY_COMMENT_FarmingPackageActivity_ACTIVITY_ID",
                table: "USER_ACTIVITY_COMMENT",
                column: "ACTIVITY_ID",
                principalTable: "FarmingPackageActivity",
                principalColumn: "ID");
        }
    }
}
