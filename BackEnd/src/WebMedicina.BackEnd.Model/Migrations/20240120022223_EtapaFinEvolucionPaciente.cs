using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebMedicina.BackEnd.Model.Migrations
{
    /// <inheritdoc />
    public partial class EtapaFinEvolucionPaciente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "EtapaLT",
                columns: new[] { "Id", "Descripcion", "FechaCreac", "FechaUltMod", "IdMedicoCreador", "IdMedicoUltModif", "Label", "Titulo" },
                values: new object[] { 999, "", new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Local), null, null, "", "Fin de la evolución del paciente" });

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "bba3c48f-4c62-427d-93e0-87009d4cee03");

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "a6e1b727-8913-4300-aa4d-266b7b89862c");

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "05131c3a-7e98-4327-8791-7be004a716ad");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EtapaLT",
                keyColumn: "Id",
                keyValue: 999);

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "218c3cd7-748b-4c60-839c-b1cd592f32bc");

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "843b194b-b9db-46eb-8955-0d6a4c1dfb2e");

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "6e8c66f1-3bc5-4312-92b3-6df10a58c1e6");
        }
    }
}
