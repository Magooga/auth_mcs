using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class MyInitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Salt = table.Column<byte[]>(type: "bytea", nullable: false),
                    Hash = table.Column<byte[]>(type: "bytea", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<long>(name: "User_Id", type: "bigint", nullable: false),
                    RoleId = table.Column<long>(name: "Role_Id", type: "bigint", nullable: false),
                    UpDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId, x.Id });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_Role_Id",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_User_Id",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreateDate", "Deleted", "Name", "UpDate" },
                values: new object[,]
                {
                    { 1L, new DateTime(2022, 2, 28, 21, 0, 0, 0, DateTimeKind.Utc), false, "Student", new DateTime(1969, 12, 31, 21, 0, 0, 0, DateTimeKind.Utc) },
                    { 2L, new DateTime(2022, 1, 31, 21, 0, 0, 0, DateTimeKind.Utc), false, "Teacher", new DateTime(1969, 12, 31, 21, 0, 0, 0, DateTimeKind.Utc) },
                    { 3L, new DateTime(2021, 12, 31, 21, 0, 0, 0, DateTimeKind.Utc), false, "Administrator", new DateTime(1969, 12, 31, 21, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreateDate", "Deleted", "Email", "FirstName", "Hash", "LastName", "Salt", "UpDate" },
                values: new object[,]
                {
                    { 1L, new DateTime(2021, 12, 14, 21, 0, 0, 0, DateTimeKind.Utc), false, "root", "root", new byte[] { 21, 89, 190, 241, 252, 86, 45, 148, 227, 4, 190, 232, 124, 30, 77, 70, 43, 211, 151, 104 }, "root", new byte[] { 70, 218, 10, 125, 133, 170, 236, 193, 122, 147, 255, 100, 189, 170, 191, 243, 204, 199, 13, 118 }, new DateTime(1969, 12, 31, 21, 0, 0, 0, DateTimeKind.Utc) },
                    { 2L, new DateTime(2022, 1, 13, 21, 0, 0, 0, DateTimeKind.Utc), false, "kovJ11@gmail.com", "Jane", new byte[] { 240, 178, 9, 149, 136, 53, 111, 195, 46, 32, 194, 152, 17, 182, 139, 173, 6, 220, 130, 40 }, "Kovalski", new byte[] { 234, 110, 84, 92, 115, 249, 254, 245, 205, 76, 104, 67, 126, 38, 92, 180, 35, 178, 136, 44 }, new DateTime(1969, 12, 31, 21, 0, 0, 0, DateTimeKind.Utc) },
                    { 3L, new DateTime(2022, 12, 10, 21, 0, 0, 0, DateTimeKind.Utc), false, "sergVVV@gmail.com", "Sergey", new byte[] { 245, 127, 178, 126, 37, 21, 77, 253, 152, 28, 55, 128, 170, 79, 193, 83, 250, 92, 254, 234 }, "Vasiliev", new byte[] { 204, 182, 201, 37, 141, 244, 10, 109, 101, 37, 212, 145, 224, 11, 83, 49, 108, 50, 83, 31 }, new DateTime(1969, 12, 31, 21, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "Role_Id", "User_Id", "CreateDate", "Deleted", "UpDate" },
                values: new object[,]
                {
                    { 1L, 3L, 1L, new DateTime(2022, 3, 11, 21, 0, 0, 0, DateTimeKind.Utc), false, new DateTime(1969, 12, 31, 21, 0, 0, 0, DateTimeKind.Utc) },
                    { 2L, 2L, 2L, new DateTime(2022, 6, 9, 21, 0, 0, 0, DateTimeKind.Utc), false, new DateTime(1969, 12, 31, 21, 0, 0, 0, DateTimeKind.Utc) },
                    { 3L, 1L, 3L, new DateTime(2022, 7, 16, 21, 0, 0, 0, DateTimeKind.Utc), false, new DateTime(1969, 12, 31, 21, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_Role_Id",
                table: "UserRoles",
                column: "Role_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
