using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebMedicina.BackEnd.Model.Migrations
{
    /// <inheritdoc />
    public partial class EtapasForeignKey2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Epilepsias",
                keyColumn: "idEpilepsia",
                keyValue: 1,
                columns: new[] { "FechaCreac", "FechaUltMod" },
                values: new object[] { new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Epilepsias",
                keyColumn: "idEpilepsia",
                keyValue: 2,
                columns: new[] { "FechaCreac", "FechaUltMod" },
                values: new object[] { new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "EtapaLT",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaCreac", "FechaUltMod", "IdMedicoCreador", "IdMedicoUltModif" },
                values: new object[] { new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Local), 1, 1 });

            migrationBuilder.UpdateData(
                table: "EtapaLT",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaCreac", "FechaUltMod", "IdMedicoCreador", "IdMedicoUltModif" },
                values: new object[] { new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Local), 1, 1 });

            migrationBuilder.UpdateData(
                table: "EtapaLT",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "FechaCreac", "FechaUltMod", "IdMedicoCreador", "IdMedicoUltModif" },
                values: new object[] { new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Local), 1, 1 });

            migrationBuilder.UpdateData(
                table: "Mutaciones",
                keyColumn: "idMutacion",
                keyValue: 1,
                columns: new[] { "FechaCreac", "FechaUltMod" },
                values: new object[] { new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Mutaciones",
                keyColumn: "idMutacion",
                keyValue: 2,
                columns: new[] { "FechaCreac", "FechaUltMod" },
                values: new object[] { new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "eae10135-49fb-4c99-9968-c5dad74559b1");

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "59879106-0209-42f9-969e-5774b5761f6e");

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "e6cc37d4-e92b-4994-b276-e266bc8df86a");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Epilepsias",
                keyColumn: "idEpilepsia",
                keyValue: 1,
                columns: new[] { "FechaCreac", "FechaUltMod" },
                values: new object[] { new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Epilepsias",
                keyColumn: "idEpilepsia",
                keyValue: 2,
                columns: new[] { "FechaCreac", "FechaUltMod" },
                values: new object[] { new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "EtapaLT",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaCreac", "FechaUltMod", "IdMedicoCreador", "IdMedicoUltModif" },
                values: new object[] { new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Local), null, null });

            migrationBuilder.UpdateData(
                table: "EtapaLT",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaCreac", "FechaUltMod", "IdMedicoCreador", "IdMedicoUltModif" },
                values: new object[] { new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Local), null, null });

            migrationBuilder.UpdateData(
                table: "EtapaLT",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "FechaCreac", "FechaUltMod", "IdMedicoCreador", "IdMedicoUltModif" },
                values: new object[] { new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Local), null, null });

            migrationBuilder.UpdateData(
                table: "Mutaciones",
                keyColumn: "idMutacion",
                keyValue: 1,
                columns: new[] { "FechaCreac", "FechaUltMod" },
                values: new object[] { new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Mutaciones",
                keyColumn: "idMutacion",
                keyValue: 2,
                columns: new[] { "FechaCreac", "FechaUltMod" },
                values: new object[] { new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "f3838c6d-c9bc-4296-9e00-6c75f7ed77da");

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "ed005d9f-a959-4350-8489-f86633b67b85");

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "4214f545-256c-489a-a80c-1730fb2b2f7a");
        }
    }
}
