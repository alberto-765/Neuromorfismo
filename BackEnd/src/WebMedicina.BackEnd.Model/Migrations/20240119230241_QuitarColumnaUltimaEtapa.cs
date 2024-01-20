using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebMedicina.BackEnd.Model.Migrations
{
    /// <inheritdoc />
    public partial class QuitarColumnaUltimaEtapa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pacientes_EtapaLT_IdUltimaEtapa",
                table: "Pacientes");

            migrationBuilder.RenameColumn(
                name: "IdUltimaEtapa",
                table: "Pacientes",
                newName: "EtapaLTModelId");

            migrationBuilder.RenameIndex(
                name: "IX_Pacientes_IdUltimaEtapa",
                table: "Pacientes",
                newName: "IX_Pacientes_EtapaLTModelId");

            migrationBuilder.UpdateData(
                table: "Epilepsias",
                keyColumn: "idEpilepsia",
                keyValue: 1,
                columns: new[] { "FechaCreac", "FechaUltMod" },
                values: new object[] { new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Epilepsias",
                keyColumn: "idEpilepsia",
                keyValue: 2,
                columns: new[] { "FechaCreac", "FechaUltMod" },
                values: new object[] { new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "EtapaLT",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaCreac", "FechaUltMod" },
                values: new object[] { new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "EtapaLT",
                keyColumn: "Id",
                keyValue: 2,
                column: "FechaCreac",
                value: new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "EtapaLT",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "FechaCreac", "FechaUltMod" },
                values: new object[] { new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Mutaciones",
                keyColumn: "idMutacion",
                keyValue: 1,
                columns: new[] { "FechaCreac", "FechaUltMod" },
                values: new object[] { new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Mutaciones",
                keyColumn: "idMutacion",
                keyValue: 2,
                columns: new[] { "FechaCreac", "FechaUltMod" },
                values: new object[] { new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "771a8378-34a6-4136-879f-368279b0acac");

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "0e286082-ef2a-4b5d-a247-a4d192f06d43");

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "2b5b024f-98f1-434d-8ca5-047b8f8d75be");

            migrationBuilder.AddForeignKey(
                name: "FK_Pacientes_EtapaLT_EtapaLTModelId",
                table: "Pacientes",
                column: "EtapaLTModelId",
                principalTable: "EtapaLT",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pacientes_EtapaLT_EtapaLTModelId",
                table: "Pacientes");

            migrationBuilder.RenameColumn(
                name: "EtapaLTModelId",
                table: "Pacientes",
                newName: "IdUltimaEtapa");

            migrationBuilder.RenameIndex(
                name: "IX_Pacientes_EtapaLTModelId",
                table: "Pacientes",
                newName: "IX_Pacientes_IdUltimaEtapa");

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
                columns: new[] { "FechaCreac", "FechaUltMod" },
                values: new object[] { new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "EtapaLT",
                keyColumn: "Id",
                keyValue: 2,
                column: "FechaCreac",
                value: new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "EtapaLT",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "FechaCreac", "FechaUltMod" },
                values: new object[] { new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Local) });

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
                name: "FK_Pacientes_EtapaLT_IdUltimaEtapa",
                table: "Pacientes",
                column: "IdUltimaEtapa",
                principalTable: "EtapaLT",
                principalColumn: "Id");
        }
    }
}
