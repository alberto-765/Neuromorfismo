using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebMedicina.BackEnd.Model.Migrations
{
    /// <inheritdoc />
    public partial class QuitarSeedEtapasParaAñadirlosDeNuevo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EtapaLT",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "EtapaLT",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "EtapaLT",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "5fae0c04-56d2-4da7-9301-33d9a2652e76");

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "17ca9f99-a4b2-4eef-9aad-9bd2f32a2a46");

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "36a082d0-173f-40e2-8ff8-313448055457");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "EtapaLT",
                columns: new[] { "Id", "Descripcion", "FechaCreac", "FechaUltMod", "IdMedicoCreador", "IdMedicoUltModif", "Label", "Titulo" },
                values: new object[,]
                {
                    { 1, "", new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Local), 1, 1, "¿Ha dado su consentimiento el paciente?", "Consentimiento Informado" },
                    { 2, "Descripcion", new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, "¿Ha dado su consentimiento el paciente?", "Paciente Acude a Extracción Analítica" },
                    { 3, "", new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Local), 1, 1, "¿Ha dado su consentimiento el paciente?", "Muestra de Genética" }
                });

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "9bdeaf59-4a0d-45b0-b737-f3edd052d3b4");

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "02aface8-a82a-4313-941c-397d6d0f3c8c");

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "b4f710b0-b39c-42e5-8bb1-13f77fae3da8");
        }
    }
}
