using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebMedicina.BackEnd.Model.Migrations
{
    /// <inheritdoc />
    public partial class IdMedicoForeignKey4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "EtapaLT",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "IdMedicoCreador", "IdMedicoUltModif" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "EtapaLT",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaUltMod", "IdMedicoCreador", "IdMedicoUltModif" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null });

            migrationBuilder.UpdateData(
                table: "EtapaLT",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "IdMedicoCreador", "IdMedicoUltModif" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "3a60d352-fdb2-47d0-9ffd-60d85066df1d");

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "7693e3f5-7d41-465b-9043-f9b3f74b468a");

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "fbf51c11-2da4-4383-85ca-0d45c706f9c0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "EtapaLT",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "IdMedicoCreador", "IdMedicoUltModif" },
                values: new object[] { 1, 1 });

            migrationBuilder.UpdateData(
                table: "EtapaLT",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaUltMod", "IdMedicoCreador", "IdMedicoUltModif" },
                values: new object[] { new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Local), 1, 1 });

            migrationBuilder.UpdateData(
                table: "EtapaLT",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "IdMedicoCreador", "IdMedicoUltModif" },
                values: new object[] { 1, 1 });

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "3f9826b0-24e8-4a05-bdd0-9cdc31c35316");

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "cdb697b4-7fb8-47c4-8b0e-d1ca8896bd19");

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "721df0ad-a544-4cd6-84cb-e569d46007da");
        }
    }
}
