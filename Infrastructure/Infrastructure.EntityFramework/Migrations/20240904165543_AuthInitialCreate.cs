using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class AuthInitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "Role_Id", "User_Id", "CreateDate", "Deleted", "UpDate" },
                values: new object[,]
                {
                    { 1L, 3L, 1L, new DateTime(2022, 3, 11, 21, 0, 0, 0, DateTimeKind.Utc), false, new DateTime(1969, 12, 31, 21, 0, 0, 0, DateTimeKind.Utc) },
                    { 2L, 2L, 2L, new DateTime(2022, 6, 9, 21, 0, 0, 0, DateTimeKind.Utc), false, new DateTime(1969, 12, 31, 21, 0, 0, 0, DateTimeKind.Utc) },
                    { 3L, 1L, 3L, new DateTime(2022, 7, 16, 21, 0, 0, 0, DateTimeKind.Utc), false, new DateTime(1969, 12, 31, 21, 0, 0, 0, DateTimeKind.Utc) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "Id", "Role_Id", "User_Id" },
                keyValues: new object[] { 1L, 3L, 1L });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "Id", "Role_Id", "User_Id" },
                keyValues: new object[] { 2L, 2L, 2L });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "Id", "Role_Id", "User_Id" },
                keyValues: new object[] { 3L, 1L, 3L });
        }
    }
}
