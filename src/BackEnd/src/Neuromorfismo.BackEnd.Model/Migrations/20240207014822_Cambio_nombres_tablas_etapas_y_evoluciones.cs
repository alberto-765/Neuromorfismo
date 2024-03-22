using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Neuromorfismo.BackEnd.Model.Migrations
{
    /// <inheritdoc />
    public partial class Cambio_nombres_tablas_etapas_y_evoluciones : Migration
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

            migrationBuilder.DropForeignKey(
                name: "FK_EvolucionLT_EtapaLT_IdEtapa",
                table: "EvolucionLT");

            migrationBuilder.DropForeignKey(
                name: "FK_EvolucionLT_Medicos_IdMedicoUltModif",
                table: "EvolucionLT");

            migrationBuilder.DropForeignKey(
                name: "FK_EvolucionLT_Pacientes_IdPaciente",
                table: "EvolucionLT");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EvolucionLT",
                table: "EvolucionLT");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EtapaLT",
                table: "EtapaLT");

            migrationBuilder.RenameTable(
                name: "EvolucionLT",
                newName: "Evoluciones");

            migrationBuilder.RenameTable(
                name: "EtapaLT",
                newName: "Etapas");

            migrationBuilder.RenameIndex(
                name: "IX_EvolucionLT_IdPaciente",
                table: "Evoluciones",
                newName: "IX_Evoluciones_IdPaciente");

            migrationBuilder.RenameIndex(
                name: "IX_EvolucionLT_IdMedicoUltModif",
                table: "Evoluciones",
                newName: "IX_Evoluciones_IdMedicoUltModif");

            migrationBuilder.RenameIndex(
                name: "IX_EvolucionLT_IdEtapa",
                table: "Evoluciones",
                newName: "IX_Evoluciones_IdEtapa");

            migrationBuilder.RenameIndex(
                name: "IX_EtapaLT_IdMedicoUltModif",
                table: "Etapas",
                newName: "IX_Etapas_IdMedicoUltModif");

            migrationBuilder.RenameIndex(
                name: "IX_EtapaLT_IdMedicoCreador",
                table: "Etapas",
                newName: "IX_Etapas_IdMedicoCreador");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Evoluciones",
                table: "Evoluciones",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Etapas",
                table: "Etapas",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "9627a373-923f-4b5b-81d9-6d9ea33d59b9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "1cba0e1c-f112-4247-992d-77af7576782c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "7d9b9182-c87f-4d6e-b453-949175c9253c");

            migrationBuilder.AddForeignKey(
                name: "FK_Etapas_Medicos_IdMedicoCreador",
                table: "Etapas",
                column: "IdMedicoCreador",
                principalTable: "Medicos",
                principalColumn: "idMedico");

            migrationBuilder.AddForeignKey(
                name: "FK_Etapas_Medicos_IdMedicoUltModif",
                table: "Etapas",
                column: "IdMedicoUltModif",
                principalTable: "Medicos",
                principalColumn: "idMedico");

            migrationBuilder.AddForeignKey(
                name: "FK_Evoluciones_Etapas_IdEtapa",
                table: "Evoluciones",
                column: "IdEtapa",
                principalTable: "Etapas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Evoluciones_Medicos_IdMedicoUltModif",
                table: "Evoluciones",
                column: "IdMedicoUltModif",
                principalTable: "Medicos",
                principalColumn: "idMedico",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Evoluciones_Pacientes_IdPaciente",
                table: "Evoluciones",
                column: "IdPaciente",
                principalTable: "Pacientes",
                principalColumn: "idPaciente",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Etapas_Medicos_IdMedicoCreador",
                table: "Etapas");

            migrationBuilder.DropForeignKey(
                name: "FK_Etapas_Medicos_IdMedicoUltModif",
                table: "Etapas");

            migrationBuilder.DropForeignKey(
                name: "FK_Evoluciones_Etapas_IdEtapa",
                table: "Evoluciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Evoluciones_Medicos_IdMedicoUltModif",
                table: "Evoluciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Evoluciones_Pacientes_IdPaciente",
                table: "Evoluciones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Evoluciones",
                table: "Evoluciones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Etapas",
                table: "Etapas");

            migrationBuilder.RenameTable(
                name: "Evoluciones",
                newName: "EvolucionLT");

            migrationBuilder.RenameTable(
                name: "Etapas",
                newName: "EtapaLT");

            migrationBuilder.RenameIndex(
                name: "IX_Evoluciones_IdPaciente",
                table: "EvolucionLT",
                newName: "IX_EvolucionLT_IdPaciente");

            migrationBuilder.RenameIndex(
                name: "IX_Evoluciones_IdMedicoUltModif",
                table: "EvolucionLT",
                newName: "IX_EvolucionLT_IdMedicoUltModif");

            migrationBuilder.RenameIndex(
                name: "IX_Evoluciones_IdEtapa",
                table: "EvolucionLT",
                newName: "IX_EvolucionLT_IdEtapa");

            migrationBuilder.RenameIndex(
                name: "IX_Etapas_IdMedicoUltModif",
                table: "EtapaLT",
                newName: "IX_EtapaLT_IdMedicoUltModif");

            migrationBuilder.RenameIndex(
                name: "IX_Etapas_IdMedicoCreador",
                table: "EtapaLT",
                newName: "IX_EtapaLT_IdMedicoCreador");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EvolucionLT",
                table: "EvolucionLT",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EtapaLT",
                table: "EtapaLT",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "5829dbde-63aa-436c-9198-9f36997a73fe");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "7c546c12-ab44-4f7b-af40-67cce46799e1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "fdb141e2-9579-44c1-9c29-c613242f061d");

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

            migrationBuilder.AddForeignKey(
                name: "FK_EvolucionLT_EtapaLT_IdEtapa",
                table: "EvolucionLT",
                column: "IdEtapa",
                principalTable: "EtapaLT",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EvolucionLT_Medicos_IdMedicoUltModif",
                table: "EvolucionLT",
                column: "IdMedicoUltModif",
                principalTable: "Medicos",
                principalColumn: "idMedico",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EvolucionLT_Pacientes_IdPaciente",
                table: "EvolucionLT",
                column: "IdPaciente",
                principalTable: "Pacientes",
                principalColumn: "idPaciente",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
