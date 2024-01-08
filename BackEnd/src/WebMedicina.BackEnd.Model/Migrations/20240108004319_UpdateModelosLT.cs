using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebMedicina.BackEnd.Model.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModelosLT : Migration
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

            migrationBuilder.DropForeignKey(
                name: "FK_EvolucionLT_EtapaLT_EtapasLTId",
                table: "EvolucionLT");

            migrationBuilder.DropForeignKey(
                name: "FK_Pacientes_EtapaLT_UltimaEtapaId",
                table: "Pacientes");

            migrationBuilder.DropIndex(
                name: "IX_EvolucionLT_EtapasLTId",
                table: "EvolucionLT");

            migrationBuilder.DropIndex(
                name: "IX_EtapaLT_MedicoCreadorIdMedico",
                table: "EtapaLT");

            migrationBuilder.DropIndex(
                name: "IX_EtapaLT_MedicoUltModifIdMedico",
                table: "EtapaLT");

            migrationBuilder.DropColumn(
                name: "MedicoCreadorIdMedico",
                table: "EtapaLT");

            migrationBuilder.DropColumn(
                name: "MedicoUltModifIdMedico",
                table: "EtapaLT");

            migrationBuilder.RenameColumn(
                name: "UltimaEtapaId",
                table: "Pacientes",
                newName: "IdUltimaEtapa");

            migrationBuilder.RenameIndex(
                name: "IX_Pacientes_UltimaEtapaId",
                table: "Pacientes",
                newName: "IX_Pacientes_IdUltimaEtapa");

            migrationBuilder.RenameColumn(
                name: "EtapasLTId",
                table: "EvolucionLT",
                newName: "IdMedicoUltModif");

            migrationBuilder.AddColumn<int>(
                name: "IdEtapa",
                table: "EvolucionLT",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdPaciente",
                table: "EvolucionLT",
                type: "int(11)",
                nullable: false,
                defaultValue: 0);

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
                columns: new[] { "FechaCreac", "FechaUltMod" },
                values: new object[] { new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "EtapaLT",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaCreac", "FechaUltMod" },
                values: new object[] { new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "EtapaLT",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "FechaCreac", "FechaUltMod" },
                values: new object[] { new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Local) });

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
                value: "adf9bac9-2e4d-41ba-9008-f61eecf2cd88");

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "a77900ad-8f57-44c9-ba6d-4ad188d1f7d3");

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "c8d788a5-3ace-4dfd-b84b-ee173fe3a47f");

            migrationBuilder.CreateIndex(
                name: "IX_EvolucionLT_IdEtapa",
                table: "EvolucionLT",
                column: "IdEtapa");

            migrationBuilder.CreateIndex(
                name: "IX_EvolucionLT_IdPaciente",
                table: "EvolucionLT",
                column: "IdPaciente");

            migrationBuilder.AddForeignKey(
                name: "FK_EvolucionLT_EtapaLT_IdEtapa",
                table: "EvolucionLT",
                column: "IdEtapa",
                principalTable: "EtapaLT",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EvolucionLT_Pacientes_IdPaciente",
                table: "EvolucionLT",
                column: "IdPaciente",
                principalTable: "Pacientes",
                principalColumn: "idPaciente",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pacientes_EtapaLT_IdUltimaEtapa",
                table: "Pacientes",
                column: "IdUltimaEtapa",
                principalTable: "EtapaLT",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EvolucionLT_EtapaLT_IdEtapa",
                table: "EvolucionLT");

            migrationBuilder.DropForeignKey(
                name: "FK_EvolucionLT_Pacientes_IdPaciente",
                table: "EvolucionLT");

            migrationBuilder.DropForeignKey(
                name: "FK_Pacientes_EtapaLT_IdUltimaEtapa",
                table: "Pacientes");

            migrationBuilder.DropIndex(
                name: "IX_EvolucionLT_IdEtapa",
                table: "EvolucionLT");

            migrationBuilder.DropIndex(
                name: "IX_EvolucionLT_IdPaciente",
                table: "EvolucionLT");

            migrationBuilder.DropColumn(
                name: "IdEtapa",
                table: "EvolucionLT");

            migrationBuilder.DropColumn(
                name: "IdPaciente",
                table: "EvolucionLT");

            migrationBuilder.RenameColumn(
                name: "IdUltimaEtapa",
                table: "Pacientes",
                newName: "UltimaEtapaId");

            migrationBuilder.RenameIndex(
                name: "IX_Pacientes_IdUltimaEtapa",
                table: "Pacientes",
                newName: "IX_Pacientes_UltimaEtapaId");

            migrationBuilder.RenameColumn(
                name: "IdMedicoUltModif",
                table: "EvolucionLT",
                newName: "EtapasLTId");

            migrationBuilder.AddColumn<int>(
                name: "MedicoCreadorIdMedico",
                table: "EtapaLT",
                type: "int(11)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MedicoUltModifIdMedico",
                table: "EtapaLT",
                type: "int(11)",
                nullable: true);

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

            migrationBuilder.UpdateData(
                table: "EtapaLT",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaCreac", "FechaUltMod", "MedicoCreadorIdMedico", "MedicoUltModifIdMedico" },
                values: new object[] { new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Local), null, null });

            migrationBuilder.UpdateData(
                table: "EtapaLT",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaCreac", "FechaUltMod", "MedicoCreadorIdMedico", "MedicoUltModifIdMedico" },
                values: new object[] { new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Local), null, null });

            migrationBuilder.UpdateData(
                table: "EtapaLT",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "FechaCreac", "FechaUltMod", "MedicoCreadorIdMedico", "MedicoUltModifIdMedico" },
                values: new object[] { new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Local), null, null });

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

            migrationBuilder.CreateIndex(
                name: "IX_EvolucionLT_EtapasLTId",
                table: "EvolucionLT",
                column: "EtapasLTId");

            migrationBuilder.CreateIndex(
                name: "IX_EtapaLT_MedicoCreadorIdMedico",
                table: "EtapaLT",
                column: "MedicoCreadorIdMedico");

            migrationBuilder.CreateIndex(
                name: "IX_EtapaLT_MedicoUltModifIdMedico",
                table: "EtapaLT",
                column: "MedicoUltModifIdMedico");

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

            migrationBuilder.AddForeignKey(
                name: "FK_EvolucionLT_EtapaLT_EtapasLTId",
                table: "EvolucionLT",
                column: "EtapasLTId",
                principalTable: "EtapaLT",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pacientes_EtapaLT_UltimaEtapaId",
                table: "Pacientes",
                column: "UltimaEtapaId",
                principalTable: "EtapaLT",
                principalColumn: "Id");
        }
    }
}
