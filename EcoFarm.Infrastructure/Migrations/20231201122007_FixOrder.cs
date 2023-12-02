using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoFarm.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ORDER_FARMING_PACKAGE_PACKAGE_ID",
                table: "ORDER");

            migrationBuilder.DropIndex(
                name: "IX_PRODUCT_PACKAGE_ID",
                table: "PRODUCT");

            migrationBuilder.RenameColumn(
                name: "IS_PURCHASED",
                table: "SHOPPING_CART",
                newName: "IS_ORDERED");

            migrationBuilder.RenameColumn(
                name: "SERVICE_TYPE",
                table: "ORDER",
                newName: "TOTAL_QUANTITY");

            migrationBuilder.RenameColumn(
                name: "QUANTITY",
                table: "ORDER",
                newName: "PAYMENT_METHOD");

            migrationBuilder.RenameColumn(
                name: "PACKAGE_ID",
                table: "ORDER",
                newName: "ENTERPRISE_ID");

            migrationBuilder.RenameIndex(
                name: "IX_ORDER_PACKAGE_ID",
                table: "ORDER",
                newName: "IX_ORDER_ENTERPRISE_ID");

            migrationBuilder.AlterColumn<string>(
                name: "USER_ID",
                table: "SHOPPING_CART",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TOTAL_QUANTITY",
                table: "SHOPPING_CART",
                type: "int",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TOTAL_PRICE",
                table: "SHOPPING_CART",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ENTERPRISE_ID",
                table: "PRODUCT",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "WEIGHT",
                table: "PRODUCT",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "ADDRESS_DESCRIPTION",
                table: "ORDER",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ADDRESS_ID",
                table: "ORDER",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TOTAL_PRICE",
                table: "ORDER",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TOTAL_WEIGHT",
                table: "ORDER",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "CURRENCY",
                table: "CART_DETAIL",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ORDER_PRODUCT",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ORDER_ID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PRODUCT_ID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    QUANTITY = table.Column<int>(type: "int", nullable: false),
                    PRICE = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    WEIGHT = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CURRENCY = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_ORDER_PRODUCT", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ORDER_PRODUCT_ORDER_ORDER_ID",
                        column: x => x.ORDER_ID,
                        principalTable: "ORDER",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ORDER_PRODUCT_PRODUCT_PRODUCT_ID",
                        column: x => x.PRODUCT_ID,
                        principalTable: "PRODUCT",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SHOPPING_CART_USER_ID",
                table: "SHOPPING_CART",
                column: "USER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCT_ENTERPRISE_ID",
                table: "PRODUCT",
                column: "ENTERPRISE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCT_PACKAGE_ID",
                table: "PRODUCT",
                column: "PACKAGE_ID",
                unique: true,
                filter: "[PACKAGE_ID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ORDER_ADDRESS_ID",
                table: "ORDER",
                column: "ADDRESS_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ORDER_PRODUCT_ORDER_ID",
                table: "ORDER_PRODUCT",
                column: "ORDER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ORDER_PRODUCT_PRODUCT_ID",
                table: "ORDER_PRODUCT",
                column: "PRODUCT_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ORDER_SELLER_ENTERPRISE_ENTERPRISE_ID",
                table: "ORDER",
                column: "ENTERPRISE_ID",
                principalTable: "SELLER_ENTERPRISE",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ORDER_USER_ADDRESS_ADDRESS_ID",
                table: "ORDER",
                column: "ADDRESS_ID",
                principalTable: "USER_ADDRESS",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_PRODUCT_SELLER_ENTERPRISE_ENTERPRISE_ID",
                table: "PRODUCT",
                column: "ENTERPRISE_ID",
                principalTable: "SELLER_ENTERPRISE",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_SHOPPING_CART_USER_USER_ID",
                table: "SHOPPING_CART",
                column: "USER_ID",
                principalTable: "USER",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ORDER_SELLER_ENTERPRISE_ENTERPRISE_ID",
                table: "ORDER");

            migrationBuilder.DropForeignKey(
                name: "FK_ORDER_USER_ADDRESS_ADDRESS_ID",
                table: "ORDER");

            migrationBuilder.DropForeignKey(
                name: "FK_PRODUCT_SELLER_ENTERPRISE_ENTERPRISE_ID",
                table: "PRODUCT");

            migrationBuilder.DropForeignKey(
                name: "FK_SHOPPING_CART_USER_USER_ID",
                table: "SHOPPING_CART");

            migrationBuilder.DropTable(
                name: "ORDER_PRODUCT");

            migrationBuilder.DropIndex(
                name: "IX_SHOPPING_CART_USER_ID",
                table: "SHOPPING_CART");

            migrationBuilder.DropIndex(
                name: "IX_PRODUCT_ENTERPRISE_ID",
                table: "PRODUCT");

            migrationBuilder.DropIndex(
                name: "IX_PRODUCT_PACKAGE_ID",
                table: "PRODUCT");

            migrationBuilder.DropIndex(
                name: "IX_ORDER_ADDRESS_ID",
                table: "ORDER");

            migrationBuilder.DropColumn(
                name: "ENTERPRISE_ID",
                table: "PRODUCT");

            migrationBuilder.DropColumn(
                name: "WEIGHT",
                table: "PRODUCT");

            migrationBuilder.DropColumn(
                name: "ADDRESS_DESCRIPTION",
                table: "ORDER");

            migrationBuilder.DropColumn(
                name: "ADDRESS_ID",
                table: "ORDER");

            migrationBuilder.DropColumn(
                name: "TOTAL_PRICE",
                table: "ORDER");

            migrationBuilder.DropColumn(
                name: "TOTAL_WEIGHT",
                table: "ORDER");

            migrationBuilder.DropColumn(
                name: "CURRENCY",
                table: "CART_DETAIL");

            migrationBuilder.RenameColumn(
                name: "IS_ORDERED",
                table: "SHOPPING_CART",
                newName: "IS_PURCHASED");

            migrationBuilder.RenameColumn(
                name: "TOTAL_QUANTITY",
                table: "ORDER",
                newName: "SERVICE_TYPE");

            migrationBuilder.RenameColumn(
                name: "PAYMENT_METHOD",
                table: "ORDER",
                newName: "QUANTITY");

            migrationBuilder.RenameColumn(
                name: "ENTERPRISE_ID",
                table: "ORDER",
                newName: "PACKAGE_ID");

            migrationBuilder.RenameIndex(
                name: "IX_ORDER_ENTERPRISE_ID",
                table: "ORDER",
                newName: "IX_ORDER_PACKAGE_ID");

            migrationBuilder.AlterColumn<string>(
                name: "USER_ID",
                table: "SHOPPING_CART",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "TOTAL_QUANTITY",
                table: "SHOPPING_CART",
                type: "float",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "TOTAL_PRICE",
                table: "SHOPPING_CART",
                type: "float",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCT_PACKAGE_ID",
                table: "PRODUCT",
                column: "PACKAGE_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ORDER_FARMING_PACKAGE_PACKAGE_ID",
                table: "ORDER",
                column: "PACKAGE_ID",
                principalTable: "FARMING_PACKAGE",
                principalColumn: "ID");
        }
    }
}
