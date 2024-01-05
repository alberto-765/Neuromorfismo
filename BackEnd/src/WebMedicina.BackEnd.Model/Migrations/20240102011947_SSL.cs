using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebMedicina.BackEnd.Model.Migrations
{
    /// <inheritdoc />
    public partial class SSL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Epilepsias",
                keyColumn: "idEpilepsia",
                keyValue: 1,
                columns: new[] { "FechaCreac", "FechaUltMod" },
                values: new object[] { new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Epilepsias",
                keyColumn: "idEpilepsia",
                keyValue: 2,
                columns: new[] { "FechaCreac", "FechaUltMod" },
                values: new object[] { new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Mutaciones",
                keyColumn: "idMutacion",
                keyValue: 1,
                columns: new[] { "FechaCreac", "FechaUltMod" },
                values: new object[] { new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Mutaciones",
                keyColumn: "idMutacion",
                keyValue: 2,
                columns: new[] { "FechaCreac", "FechaUltMod" },
                values: new object[] { new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "9b9746d7-572f-4051-9482-4bfa3b54a2d0");

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "d495689a-8361-4dd4-a6ed-341abd17ac44");

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "e32a9aed-fbe4-4a61-8115-4f47b02b38e9");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Epilepsias",
                keyColumn: "idEpilepsia",
                keyValue: 1,
                columns: new[] { "FechaCreac", "FechaUltMod" },
                values: new object[] { new DateTime(2023, 12, 31, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2023, 12, 31, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Epilepsias",
                keyColumn: "idEpilepsia",
                keyValue: 2,
                columns: new[] { "FechaCreac", "FechaUltMod" },
                values: new object[] { new DateTime(2023, 12, 31, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2023, 12, 31, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Mutaciones",
                keyColumn: "idMutacion",
                keyValue: 1,
                columns: new[] { "FechaCreac", "FechaUltMod" },
                values: new object[] { new DateTime(2023, 12, 31, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2023, 12, 31, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Mutaciones",
                keyColumn: "idMutacion",
                keyValue: 2,
                columns: new[] { "FechaCreac", "FechaUltMod" },
                values: new object[] { new DateTime(2023, 12, 31, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2023, 12, 31, 0, 0, 0, 0, DateTimeKind.Local) });

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
    }
}
