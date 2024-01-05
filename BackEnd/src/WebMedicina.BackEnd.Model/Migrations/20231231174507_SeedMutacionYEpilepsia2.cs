using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebMedicina.BackEnd.Model.Migrations
{
    /// <inheritdoc />
    public partial class SeedMutacionYEpilepsia2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Epilepsias",
                columns: new[] { "idEpilepsia", "FechaCreac", "FechaUltMod", "nombre" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 12, 31, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2023, 12, 31, 0, 0, 0, 0, DateTimeKind.Local), "Epilepsia1" },
                    { 2, new DateTime(2023, 12, 31, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2023, 12, 31, 0, 0, 0, 0, DateTimeKind.Local), "Epilepsia2" }
                });

            migrationBuilder.InsertData(
                table: "Mutaciones",
                columns: new[] { "idMutacion", "FechaCreac", "FechaUltMod", "nombre" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 12, 31, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2023, 12, 31, 0, 0, 0, 0, DateTimeKind.Local), "Mutacion1" },
                    { 2, new DateTime(2023, 12, 31, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2023, 12, 31, 0, 0, 0, 0, DateTimeKind.Local), "Mutacion2" }
                });

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "7d627f02-ae76-49f1-b0e4-36d46b8a51a0");

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "ccc2c28c-365d-4159-8f47-f2cce78d415c");

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "273e5717-1493-46f6-9f5c-82f87a0fc3ef");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Epilepsias",
                keyColumn: "idEpilepsia",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Epilepsias",
                keyColumn: "idEpilepsia",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Mutaciones",
                keyColumn: "idMutacion",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Mutaciones",
                keyColumn: "idMutacion",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "f384680b-1711-48ea-a3bf-6e8a4002478a");

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "2d2f3afc-7c00-4b8f-9187-933c1db69ec6");

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "06aa630a-eeb7-4176-aef7-a029bd6dc448");
        }
    }
}
