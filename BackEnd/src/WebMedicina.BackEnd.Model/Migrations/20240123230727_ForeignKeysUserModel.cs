using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebMedicina.BackEnd.Model.Migrations
{
    /// <inheritdoc />
    public partial class ForeignKeysUserModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdMedico",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.UpdateData(
                table: "Epilepsias",
                keyColumn: "idEpilepsia",
                keyValue: 1,
                columns: new[] { "FechaCreac", "FechaUltMod" },
                values: new object[] { new DateTime(2024, 1, 24, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 1, 24, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Epilepsias",
                keyColumn: "idEpilepsia",
                keyValue: 2,
                columns: new[] { "FechaCreac", "FechaUltMod" },
                values: new object[] { new DateTime(2024, 1, 24, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 1, 24, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.InsertData(
                table: "EtapaLT",
                columns: new[] { "Id", "Descripcion", "FechaCreac", "FechaUltMod", "IdMedicoCreador", "IdMedicoUltModif", "Label", "Titulo" },
                values: new object[,]
                {
                    { 1, "", new DateTime(2024, 1, 24, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 1, 24, 0, 0, 0, 0, DateTimeKind.Local), null, null, "¿Ha dado su consentimiento el paciente?", "Consentimiento Informado" },
                    { 2, "Descripcion", new DateTime(2024, 1, 24, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "¿Ha dado su consentimiento el paciente?", "Paciente Acude a Extracción Analítica" },
                    { 3, "", new DateTime(2024, 1, 24, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 1, 24, 0, 0, 0, 0, DateTimeKind.Local), null, null, "¿Ha dado su consentimiento el paciente?", "Muestra de Genética" },
                    { 999, "", new DateTime(2024, 1, 24, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 1, 24, 0, 0, 0, 0, DateTimeKind.Local), null, null, "", "Fin de la evolución del paciente" }
                });

            migrationBuilder.UpdateData(
                table: "Mutaciones",
                keyColumn: "idMutacion",
                keyValue: 1,
                columns: new[] { "FechaCreac", "FechaUltMod" },
                values: new object[] { new DateTime(2024, 1, 24, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 1, 24, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Mutaciones",
                keyColumn: "idMutacion",
                keyValue: 2,
                columns: new[] { "FechaCreac", "FechaUltMod" },
                values: new object[] { new DateTime(2024, 1, 24, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 1, 24, 0, 0, 0, 0, DateTimeKind.Local) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DeleteData(
                table: "EtapaLT",
                keyColumn: "Id",
                keyValue: 999);

            migrationBuilder.DropColumn(
                name: "IdMedico",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "5b407351-1f59-4b4b-ad88-11e319d389f8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "bf9a1d23-5ff1-430b-b6e6-421bbc27610c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "62197bf2-744e-4cc7-9617-70f1b873c6cd");

            migrationBuilder.UpdateData(
                table: "Epilepsias",
                keyColumn: "idEpilepsia",
                keyValue: 1,
                columns: new[] { "FechaCreac", "FechaUltMod" },
                values: new object[] { new DateTime(2024, 1, 23, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 1, 23, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Epilepsias",
                keyColumn: "idEpilepsia",
                keyValue: 2,
                columns: new[] { "FechaCreac", "FechaUltMod" },
                values: new object[] { new DateTime(2024, 1, 23, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 1, 23, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Mutaciones",
                keyColumn: "idMutacion",
                keyValue: 1,
                columns: new[] { "FechaCreac", "FechaUltMod" },
                values: new object[] { new DateTime(2024, 1, 23, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 1, 23, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Mutaciones",
                keyColumn: "idMutacion",
                keyValue: 2,
                columns: new[] { "FechaCreac", "FechaUltMod" },
                values: new object[] { new DateTime(2024, 1, 23, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 1, 23, 0, 0, 0, 0, DateTimeKind.Local) });
        }
    }
}
