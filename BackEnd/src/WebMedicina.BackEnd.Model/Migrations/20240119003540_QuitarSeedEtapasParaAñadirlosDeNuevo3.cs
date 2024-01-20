using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebMedicina.BackEnd.Model.Migrations
{
    /// <inheritdoc />
    public partial class QuitarSeedEtapasParaAñadirlosDeNuevo3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EtapaLT_Medicos_IdMedicoCreador",
                table: "EtapaLT");

            migrationBuilder.DropForeignKey(
                name: "FK_EtapaLT_Medicos_IdMedicoUltModif",
                table: "EtapaLT");

            migrationBuilder.AlterColumn<int>(
                name: "IdMedicoUltModif",
                table: "EtapaLT",
                type: "int(11)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int(11)");

            migrationBuilder.AlterColumn<int>(
                name: "IdMedicoCreador",
                table: "EtapaLT",
                type: "int(11)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int(11)");

            migrationBuilder.InsertData(
                table: "EtapaLT",
                columns: new[] { "Id", "Descripcion", "FechaCreac", "FechaUltMod", "IdMedicoCreador", "IdMedicoUltModif", "Label", "Titulo" },
                values: new object[,]
                {
                    { 1, "", new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Local), null, null, "¿Ha dado su consentimiento el paciente?", "Consentimiento Informado" },
                    { 2, "Descripcion", new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "¿Ha dado su consentimiento el paciente?", "Paciente Acude a Extracción Analítica" },
                    { 3, "", new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Local), null, null, "¿Ha dado su consentimiento el paciente?", "Muestra de Genética" }
                });

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "fa3c5081-42d6-4746-9758-c4a18d1ff320");

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "2efb0383-561f-4def-bc3f-3a4cc8d3b6d3");

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "f5d862e4-325d-4148-93c6-6f31278f2da6");

            migrationBuilder.AddForeignKey(
                name: "FK_EtapaLT_Medicos_IdMedicoCreador",
                table: "EtapaLT",
                column: "IdMedicoCreador",
                principalTable: "Medicos",
                principalColumn: "idMedico");

            migrationBuilder.AddForeignKey(
                name: "FK_EtapaLT_Medicos_IdMedicoUltModif",
                table: "EtapaLT",
                column: "IdMedicoUltModif",
                principalTable: "Medicos",
                principalColumn: "idMedico");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EtapaLT_Medicos_IdMedicoCreador",
                table: "EtapaLT");

            migrationBuilder.DropForeignKey(
                name: "FK_EtapaLT_Medicos_IdMedicoUltModif",
                table: "EtapaLT");

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

            migrationBuilder.AlterColumn<int>(
                name: "IdMedicoUltModif",
                table: "EtapaLT",
                type: "int(11)",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int(11)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IdMedicoCreador",
                table: "EtapaLT",
                type: "int(11)",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int(11)",
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_EtapaLT_Medicos_IdMedicoCreador",
                table: "EtapaLT",
                column: "IdMedicoCreador",
                principalTable: "Medicos",
                principalColumn: "idMedico",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EtapaLT_Medicos_IdMedicoUltModif",
                table: "EtapaLT",
                column: "IdMedicoUltModif",
                principalTable: "Medicos",
                principalColumn: "idMedico",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
