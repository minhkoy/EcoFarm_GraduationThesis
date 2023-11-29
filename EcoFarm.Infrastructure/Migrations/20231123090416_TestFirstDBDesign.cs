using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoFarm.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TestFirstDBDesign : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ACCOUNT",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    USERNAME = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    EMAIL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AVATAR_URL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SALT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IS_EMAIL_CONFIRMED = table.Column<bool>(type: "bit", nullable: false),
                    HASHED_PASSWORD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LAST_LOGGED_IN = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LAST_LOGGED_OUT = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ACCOUNT_TYPE = table.Column<int>(type: "int", nullable: false),
                    LOCKED_REASON = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_ACCOUNT", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ROLE",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DESCRIPTION = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VERSION = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CREATED_TIME = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CREATED_BY = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MODIFIED_TIME = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MODIFIED_BY = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IS_ACTIVE = table.Column<bool>(type: "bit", nullable: false),
                    IS_DELETE = table.Column<bool>(type: "bit", nullable: false),
                    CODE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NAME = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ROLE", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SHOPPING_CART",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    USER_ID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IS_PURCHASED = table.Column<bool>(type: "bit", nullable: false),
                    TOTAL_QUANTITY = table.Column<double>(type: "float", nullable: true),
                    TOTAL_PRICE = table.Column<double>(type: "float", nullable: true),
                    VERSION = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CREATED_TIME = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CREATED_BY = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MODIFIED_TIME = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MODIFIED_BY = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IS_ACTIVE = table.Column<bool>(type: "bit", nullable: false),
                    IS_DELETE = table.Column<bool>(type: "bit", nullable: false),
                    CODE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NAME = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SHOPPING_CART", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ACCOUNT_VERIFY",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ACCOUNT_ID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    VERIFY_CODE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VERIFY_REQUEST_TIME = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VERIFY_EXPIRE_TIME = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IS_VERIFIED = table.Column<bool>(type: "bit", nullable: false),
                    VERIFY_REASON = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_ACCOUNT_VERIFY", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ACCOUNT_VERIFY_ACCOUNT_ACCOUNT_ID",
                        column: x => x.ACCOUNT_ID,
                        principalTable: "ACCOUNT",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "SELLER_ENTERPRISE",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ACCOUNT_ID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DESCRIPTION = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TAX_CODE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IS_APPROVED = table.Column<bool>(type: "bit", nullable: true),
                    APPROVED_OR_REJECTED_BY = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    APPROVED_OR_REJECTED_TIME = table.Column<DateTime>(type: "datetime2", nullable: true),
                    REJECT_REASON = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HOTLINE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ADDRESS = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_SELLER_ENTERPRISE", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SELLER_ENTERPRISE_ACCOUNT_ACCOUNT_ID",
                        column: x => x.ACCOUNT_ID,
                        principalTable: "ACCOUNT",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "USER",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ACCOUNT_ID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DATE_OF_BIRTH = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PHONE_NUMBER = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GENDER = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_USER", x => x.ID);
                    table.ForeignKey(
                        name: "FK_USER_ACCOUNT_ACCOUNT_ID",
                        column: x => x.ACCOUNT_ID,
                        principalTable: "ACCOUNT",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "ROLE_USER",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ROLE_ID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    USER_ID = table.Column<string>(type: "nvarchar(450)", nullable: true),
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
                    table.PrimaryKey("PK_ROLE_USER", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ROLE_USER_ACCOUNT_USER_ID",
                        column: x => x.USER_ID,
                        principalTable: "ACCOUNT",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ROLE_USER_ROLE_ROLE_ID",
                        column: x => x.ROLE_ID,
                        principalTable: "ROLE",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "FARMING_PACKAGE",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DESCRIPTION = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SELLER_ENTERPRISE_ID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ESTIMATED_START_TIME = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ESTIMATED_END_TIME = table.Column<DateTime>(type: "datetime2", nullable: true),
                    START_TIME = table.Column<DateTime>(type: "datetime2", nullable: true),
                    END_TIME = table.Column<DateTime>(type: "datetime2", nullable: true),
                    QUANTITY_START = table.Column<int>(type: "int", nullable: true),
                    QUANTITY_REGISTERED = table.Column<int>(type: "int", nullable: false),
                    PRICE = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DISCOUNT_PRICE = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DISCOUNT_START = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DISCOUNT_END = table.Column<DateTime>(type: "datetime2", nullable: true),
                    STATUS = table.Column<int>(type: "int", nullable: false),
                    SERVICE_TYPE = table.Column<int>(type: "int", nullable: false),
                    REJECT_REASON = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TOTAL_RATING_POINTS = table.Column<long>(type: "bigint", nullable: false),
                    NUMBERS_OF_RATING = table.Column<int>(type: "int", nullable: false),
                    VERSION = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CREATED_TIME = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CREATED_BY = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MODIFIED_TIME = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MODIFIED_BY = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IS_ACTIVE = table.Column<bool>(type: "bit", nullable: false),
                    IS_DELETE = table.Column<bool>(type: "bit", nullable: false),
                    CODE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NAME = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FARMING_PACKAGE", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FARMING_PACKAGE_SELLER_ENTERPRISE_SELLER_ENTERPRISE_ID",
                        column: x => x.SELLER_ENTERPRISE_ID,
                        principalTable: "SELLER_ENTERPRISE",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "USER_ADDRESS",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    USER_ID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    RECEIVER_NAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ADDRESS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DESCRIPTION = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PHONE_NUMBER = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IS_MAIN = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_USER_ADDRESS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_USER_ADDRESS_USER_USER_ID",
                        column: x => x.USER_ID,
                        principalTable: "USER",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "FarmingPackageActivity",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PACKAGE_ID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DESCRIPTION = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VERSION = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CREATED_TIME = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CREATED_BY = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MODIFIED_TIME = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MODIFIED_BY = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IS_ACTIVE = table.Column<bool>(type: "bit", nullable: false),
                    IS_DELETE = table.Column<bool>(type: "bit", nullable: false),
                    CODE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NAME = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FarmingPackageActivity", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FarmingPackageActivity_FARMING_PACKAGE_PACKAGE_ID",
                        column: x => x.PACKAGE_ID,
                        principalTable: "FARMING_PACKAGE",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "ORDER",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PACKAGE_ID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    USER_ID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    NOTE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QUANTITY = table.Column<int>(type: "int", nullable: false),
                    STATUS = table.Column<int>(type: "int", nullable: false),
                    SERVICE_TYPE = table.Column<int>(type: "int", nullable: false),
                    VERSION = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CREATED_TIME = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CREATED_BY = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MODIFIED_TIME = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MODIFIED_BY = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IS_ACTIVE = table.Column<bool>(type: "bit", nullable: false),
                    IS_DELETE = table.Column<bool>(type: "bit", nullable: false),
                    CODE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NAME = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ORDER", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ORDER_FARMING_PACKAGE_PACKAGE_ID",
                        column: x => x.PACKAGE_ID,
                        principalTable: "FARMING_PACKAGE",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ORDER_USER_USER_ID",
                        column: x => x.USER_ID,
                        principalTable: "USER",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "PACKAGE_MEDIA",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PACKAGE_ID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DESCRIPTION = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MEDIA_TYPE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MEDIA_URL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VERSION = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CREATED_TIME = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CREATED_BY = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MODIFIED_TIME = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MODIFIED_BY = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IS_ACTIVE = table.Column<bool>(type: "bit", nullable: false),
                    IS_DELETE = table.Column<bool>(type: "bit", nullable: false),
                    CODE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NAME = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PACKAGE_MEDIA", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PACKAGE_MEDIA_FARMING_PACKAGE_PACKAGE_ID",
                        column: x => x.PACKAGE_ID,
                        principalTable: "FARMING_PACKAGE",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "PRODUCT",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DESCRIPTION = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PACKAGE_ID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TYPE = table.Column<int>(type: "int", nullable: true),
                    QUANTITY = table.Column<int>(type: "int", nullable: true),
                    SOLD = table.Column<int>(type: "int", nullable: true),
                    PRICE = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PRICE_FOR_REGISTERED = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CURRENCY = table.Column<int>(type: "int", nullable: true),
                    VERSION = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CREATED_TIME = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CREATED_BY = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MODIFIED_TIME = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MODIFIED_BY = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IS_ACTIVE = table.Column<bool>(type: "bit", nullable: false),
                    IS_DELETE = table.Column<bool>(type: "bit", nullable: false),
                    CODE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NAME = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUCT", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PRODUCT_FARMING_PACKAGE_PACKAGE_ID",
                        column: x => x.PACKAGE_ID,
                        principalTable: "FARMING_PACKAGE",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "USER_PACKAGE_REVIEW",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    USER_ID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PACKAGE_ID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    COMMENT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ANSWER = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RATING = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_USER_PACKAGE_REVIEW", x => x.ID);
                    table.ForeignKey(
                        name: "FK_USER_PACKAGE_REVIEW_FARMING_PACKAGE_PACKAGE_ID",
                        column: x => x.PACKAGE_ID,
                        principalTable: "FARMING_PACKAGE",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_USER_PACKAGE_REVIEW_USER_USER_ID",
                        column: x => x.USER_ID,
                        principalTable: "USER",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "USER_REGISTER_PACKAGE",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    USER_ID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PACKAGE_ID = table.Column<string>(type: "nvarchar(450)", nullable: true),
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
                    table.PrimaryKey("PK_USER_REGISTER_PACKAGE", x => x.ID);
                    table.ForeignKey(
                        name: "FK_USER_REGISTER_PACKAGE_FARMING_PACKAGE_PACKAGE_ID",
                        column: x => x.PACKAGE_ID,
                        principalTable: "FARMING_PACKAGE",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_USER_REGISTER_PACKAGE_USER_USER_ID",
                        column: x => x.USER_ID,
                        principalTable: "USER",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "ACTIVITY_MEDIA",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ACTIVITY_ID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    MEDIA_URL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IMAGE_DESCRIPTION = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IMAGE_TYPE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VERSION = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CREATED_TIME = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CREATED_BY = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MODIFIED_TIME = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MODIFIED_BY = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IS_ACTIVE = table.Column<bool>(type: "bit", nullable: false),
                    IS_DELETE = table.Column<bool>(type: "bit", nullable: false),
                    CODE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NAME = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ACTIVITY_MEDIA", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ACTIVITY_MEDIA_FarmingPackageActivity_ACTIVITY_ID",
                        column: x => x.ACTIVITY_ID,
                        principalTable: "FarmingPackageActivity",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "USER_ACTIVITY_COMMENT",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ACTIVITY_ID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    USER_ID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    COMMENT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IS_LIKE = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_USER_ACTIVITY_COMMENT", x => x.ID);
                    table.ForeignKey(
                        name: "FK_USER_ACTIVITY_COMMENT_FarmingPackageActivity_ACTIVITY_ID",
                        column: x => x.ACTIVITY_ID,
                        principalTable: "FarmingPackageActivity",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_USER_ACTIVITY_COMMENT_USER_USER_ID",
                        column: x => x.USER_ID,
                        principalTable: "USER",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "CART_DETAIL",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PRODUCT_ID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CART_ID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    QUANTITY = table.Column<int>(type: "int", nullable: true),
                    PRICE = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
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
                    table.PrimaryKey("PK_CART_DETAIL", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CART_DETAIL_PRODUCT_PRODUCT_ID",
                        column: x => x.PRODUCT_ID,
                        principalTable: "PRODUCT",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_CART_DETAIL_SHOPPING_CART_CART_ID",
                        column: x => x.CART_ID,
                        principalTable: "SHOPPING_CART",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "PRODUCT_MEDIA",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PRODUCT_ID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DESCRIPTION = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MEDIA_TYPE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MEDIA_URL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VERSION = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CREATED_TIME = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CREATED_BY = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MODIFIED_TIME = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MODIFIED_BY = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IS_ACTIVE = table.Column<bool>(type: "bit", nullable: false),
                    IS_DELETE = table.Column<bool>(type: "bit", nullable: false),
                    CODE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NAME = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUCT_MEDIA", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PRODUCT_MEDIA_PRODUCT_PRODUCT_ID",
                        column: x => x.PRODUCT_ID,
                        principalTable: "PRODUCT",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ACCOUNT_USERNAME",
                table: "ACCOUNT",
                column: "USERNAME",
                unique: true,
                filter: "[USERNAME] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ACCOUNT_VERIFY_ACCOUNT_ID",
                table: "ACCOUNT_VERIFY",
                column: "ACCOUNT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ACTIVITY_MEDIA_ACTIVITY_ID",
                table: "ACTIVITY_MEDIA",
                column: "ACTIVITY_ID");

            migrationBuilder.CreateIndex(
                name: "IX_CART_DETAIL_CART_ID",
                table: "CART_DETAIL",
                column: "CART_ID");

            migrationBuilder.CreateIndex(
                name: "IX_CART_DETAIL_PRODUCT_ID",
                table: "CART_DETAIL",
                column: "PRODUCT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_FARMING_PACKAGE_SELLER_ENTERPRISE_ID",
                table: "FARMING_PACKAGE",
                column: "SELLER_ENTERPRISE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_FarmingPackageActivity_PACKAGE_ID",
                table: "FarmingPackageActivity",
                column: "PACKAGE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ORDER_PACKAGE_ID",
                table: "ORDER",
                column: "PACKAGE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ORDER_USER_ID",
                table: "ORDER",
                column: "USER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PACKAGE_MEDIA_PACKAGE_ID",
                table: "PACKAGE_MEDIA",
                column: "PACKAGE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCT_PACKAGE_ID",
                table: "PRODUCT",
                column: "PACKAGE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCT_MEDIA_PRODUCT_ID",
                table: "PRODUCT_MEDIA",
                column: "PRODUCT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ROLE_USER_ROLE_ID",
                table: "ROLE_USER",
                column: "ROLE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ROLE_USER_USER_ID",
                table: "ROLE_USER",
                column: "USER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_SELLER_ENTERPRISE_ACCOUNT_ID",
                table: "SELLER_ENTERPRISE",
                column: "ACCOUNT_ID",
                unique: true,
                filter: "[ACCOUNT_ID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_USER_ACCOUNT_ID",
                table: "USER",
                column: "ACCOUNT_ID",
                unique: true,
                filter: "[ACCOUNT_ID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_USER_ACTIVITY_COMMENT_ACTIVITY_ID_USER_ID",
                table: "USER_ACTIVITY_COMMENT",
                columns: new[] { "ACTIVITY_ID", "USER_ID" },
                unique: true,
                filter: "[ACTIVITY_ID] IS NOT NULL AND [USER_ID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_USER_ACTIVITY_COMMENT_USER_ID",
                table: "USER_ACTIVITY_COMMENT",
                column: "USER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_USER_ADDRESS_USER_ID",
                table: "USER_ADDRESS",
                column: "USER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_USER_PACKAGE_REVIEW_PACKAGE_ID",
                table: "USER_PACKAGE_REVIEW",
                column: "PACKAGE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_USER_PACKAGE_REVIEW_USER_ID",
                table: "USER_PACKAGE_REVIEW",
                column: "USER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_USER_REGISTER_PACKAGE_PACKAGE_ID",
                table: "USER_REGISTER_PACKAGE",
                column: "PACKAGE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_USER_REGISTER_PACKAGE_USER_ID",
                table: "USER_REGISTER_PACKAGE",
                column: "USER_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ACCOUNT_VERIFY");

            migrationBuilder.DropTable(
                name: "ACTIVITY_MEDIA");

            migrationBuilder.DropTable(
                name: "CART_DETAIL");

            migrationBuilder.DropTable(
                name: "ORDER");

            migrationBuilder.DropTable(
                name: "PACKAGE_MEDIA");

            migrationBuilder.DropTable(
                name: "PRODUCT_MEDIA");

            migrationBuilder.DropTable(
                name: "ROLE_USER");

            migrationBuilder.DropTable(
                name: "USER_ACTIVITY_COMMENT");

            migrationBuilder.DropTable(
                name: "USER_ADDRESS");

            migrationBuilder.DropTable(
                name: "USER_PACKAGE_REVIEW");

            migrationBuilder.DropTable(
                name: "USER_REGISTER_PACKAGE");

            migrationBuilder.DropTable(
                name: "SHOPPING_CART");

            migrationBuilder.DropTable(
                name: "PRODUCT");

            migrationBuilder.DropTable(
                name: "ROLE");

            migrationBuilder.DropTable(
                name: "FarmingPackageActivity");

            migrationBuilder.DropTable(
                name: "USER");

            migrationBuilder.DropTable(
                name: "FARMING_PACKAGE");

            migrationBuilder.DropTable(
                name: "SELLER_ENTERPRISE");

            migrationBuilder.DropTable(
                name: "ACCOUNT");
        }
    }
}
