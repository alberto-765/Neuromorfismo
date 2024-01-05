using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebMedicina.BackEnd.Model.Migrations
{
    /// <inheritdoc />
    public partial class SeedEtapasLineaTemporal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EtapaLT_Medicos_MedicoCreadorIdMedico",
                table: "EtapaLT");

            migrationBuilder.DropForeignKey(
                name: "FK_EtapaLT_Medicos_MedicoUltModifIdMedico",
                table: "EtapaLT");

            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "EtapaLT");

            migrationBuilder.AlterColumn<int>(
                name: "MedicoUltModifIdMedico",
                table: "EtapaLT",
                type: "int(11)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int(11)");

            migrationBuilder.AlterColumn<int>(
                name: "MedicoCreadorIdMedico",
                table: "EtapaLT",
                type: "int(11)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int(11)");

            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "EtapaLT",
                type: "varchar(250)",
                maxLength: 250,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Epilepsias",
                keyColumn: "idEpilepsia",
                keyValue: 1,
                columns: new[] { "FechaCreac", "FechaUltMod" },
                values: new object[] { new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Epilepsias",
                keyColumn: "idEpilepsia",
                keyValue: 2,
                columns: new[] { "FechaCreac", "FechaUltMod" },
                values: new object[] { new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.InsertData(
                table: "EtapaLT",
                columns: new[] { "Id", "Descripcion", "FechaCreac", "FechaUltMod", "Label", "MedicoCreadorIdMedico", "MedicoUltModifIdMedico", "Titulo" },
                values: new object[,]
                {
                    { 1, "", new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Local), "¿Ha dado su consentimiento el paciente?", null, null, "Consentimiento Informado" },
                    { 2, "Descripcion", new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Local), "¿Ha dado su consentimiento el paciente?", null, null, "Paciente Acude a Extracción Analítica" },
                    { 3, "", new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Local), "¿Ha dado su consentimiento el paciente?", null, null, "Muestra de Genética" }
                });

            migrationBuilder.UpdateData(
                table: "Mutaciones",
                keyColumn: "idMutacion",
                keyValue: 1,
                columns: new[] { "FechaCreac", "FechaUltMod" },
                values: new object[] { new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Mutaciones",
                keyColumn: "idMutacion",
                keyValue: 2,
                columns: new[] { "FechaCreac", "FechaUltMod" },
                values: new object[] { new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "d08cf85d-88c7-4694-980d-175f322ecbf8");

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "9452eda7-3f4d-41de-8aae-d5f5e6a97643");

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "215ddcdf-ccce-409b-b6c9-d3ba875cbbde");

            migrationBuilder.AddForeignKey(
                name: "FK_EtapaLT_Medicos_MedicoCreadorIdMedico",
                table: "EtapaLT",
                column: "MedicoCreadorIdMedico",
                principalTable: "Medicos",
                principalColumn: "idMedico");

            migrationBuilder.AddForeignKey(
                name: "FK_EtapaLT_Medicos_MedicoUltModifIdMedico",
                table: "EtapaLT",
                column: "MedicoUltModifIdMedico",
                principalTable: "Medicos",
                principalColumn: "idMedico");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EtapaLT_Medicos_MedicoCreadorIdMedico",
                table: "EtapaLT");

            migrationBuilder.DropForeignKey(
                name: "FK_EtapaLT_Medicos_MedicoUltModifIdMedico",
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

            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "EtapaLT");

            migrationBuilder.AlterColumn<int>(
                name: "MedicoUltModifIdMedico",
                table: "EtapaLT",
                type: "int(11)",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int(11)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MedicoCreadorIdMedico",
                table: "EtapaLT",
                type: "int(11)",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int(11)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "EtapaLT",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Epilepsias",
                keyColumn: "idEpilepsia",
                keyValue: 1,
                columns: new[] { "FechaCreac", "FechaUltMod" },
                values: new object[] { new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Epilepsias",
                keyColumn: "idEpilepsia",
                keyValue: 2,
                columns: new[] { "FechaCreac", "FechaUltMod" },
                values: new object[] { new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Mutaciones",
                keyColumn: "idMutacion",
                keyValue: 1,
                columns: new[] { "FechaCreac", "FechaUltMod" },
                values: new object[] { new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Mutaciones",
                keyColumn: "idMutacion",
                keyValue: 2,
                columns: new[] { "FechaCreac", "FechaUltMod" },
                values: new object[] { new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "fc7d6ce0-c792-480e-a577-e08b7ebcb723");

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "152f9d7f-3d79-4fc8-a860-5262f7cee601");

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "30c9c83b-950b-4269-9d4c-c6037a33d888");

            migrationBuilder.AddForeignKey(
                name: "FK_EtapaLT_Medicos_MedicoCreadorIdMedico",
                table: "EtapaLT",
                column: "MedicoCreadorIdMedico",
                principalTable: "Medicos",
                principalColumn: "idMedico",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EtapaLT_Medicos_MedicoUltModifIdMedico",
                table: "EtapaLT",
                column: "MedicoUltModifIdMedico",
                principalTable: "Medicos",
                principalColumn: "idMedico",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
