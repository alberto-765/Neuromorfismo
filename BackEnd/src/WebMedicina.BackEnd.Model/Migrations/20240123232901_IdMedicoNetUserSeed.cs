using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebMedicina.BackEnd.Model.Migrations
{
    /// <inheritdoc />
    public partial class IdMedicoNetUserSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "60d3b5be-7e00-41e0-84e7-44ecd3a4965b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "e1b6efff-994a-42bc-b760-b91eb2d1b27a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "e81bb18e-498f-4754-a2d6-efbc69a8ead3");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "IdMedico", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "a4d628bb-4986-4b5c-adda-6e7e2c2991b7", 0, "d2bb3fa6-734d-4ead-9e12-b95657e2fb4f", null, false, 1, false, null, null, "ALBERTO", "AQAAAAIAAYagAAAAECWeQIf5nj4O5nN0TbkE1sYL6aSI1dXhrPE1KD++LMN9hMaR4ZAW7CEr3ld73O6AkQ==", null, false, "4F6ACO5QK3P5XBDISEIFH27CXIFNKOJ5", false, "alberto" });

            migrationBuilder.InsertData(
                table: "Medicos",
                columns: new[] { "idMedico", "apellidos", "FechaCreac", "fechaNac", "FechaUltMod", "netuserId", "nombre", "sexo", "userLogin" },
                values: new object[] { 1, "Mimbrero Gu", new DateTime(2024, 1, 24, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2003, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 24, 0, 0, 0, 0, DateTimeKind.Local), "a4d628bb-4986-4b5c-adda-6e7e2c2991b7", "Alberto", "H", "alberto" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Medicos",
                keyColumn: "idMedico",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a4d628bb-4986-4b5c-adda-6e7e2c2991b7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "b1e41ef7-5dba-4fd5-8114-c7832773d3f4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "237d4a4f-bd4b-4b88-a9d5-da1ebab9cfaf");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "e9f227d7-e8eb-4b0e-9f8c-c7fbb22dd709");
        }
    }
}
