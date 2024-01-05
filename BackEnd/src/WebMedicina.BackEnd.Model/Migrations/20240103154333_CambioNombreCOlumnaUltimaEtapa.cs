using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebMedicina.BackEnd.Model.Migrations
{
    /// <inheritdoc />
    public partial class CambioNombreCOlumnaUltimaEtapa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pacientes_EtapaLT_EvolucionPacienteId",
                table: "Pacientes");

            migrationBuilder.RenameColumn(
                name: "EvolucionPacienteId",
                table: "Pacientes",
                newName: "UltimaEtapaId");

            migrationBuilder.RenameIndex(
                name: "IX_Pacientes_EvolucionPacienteId",
                table: "Pacientes",
                newName: "IX_Pacientes_UltimaEtapaId");

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
                name: "FK_Pacientes_EtapaLT_UltimaEtapaId",
                table: "Pacientes",
                column: "UltimaEtapaId",
                principalTable: "EtapaLT",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pacientes_EtapaLT_UltimaEtapaId",
                table: "Pacientes");

            migrationBuilder.RenameColumn(
                name: "UltimaEtapaId",
                table: "Pacientes",
                newName: "EvolucionPacienteId");

            migrationBuilder.RenameIndex(
                name: "IX_Pacientes_UltimaEtapaId",
                table: "Pacientes",
                newName: "IX_Pacientes_EvolucionPacienteId");

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "36db9e35-0333-4b50-bf7a-c467e8f04fcf");

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "255eaa25-7633-44a7-9fc9-3168d2520d13");

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "a4d4d1b8-d797-4e3e-b3ce-ff698e0f195a");

            migrationBuilder.AddForeignKey(
                name: "FK_Pacientes_EtapaLT_EvolucionPacienteId",
                table: "Pacientes",
                column: "EvolucionPacienteId",
                principalTable: "EtapaLT",
                principalColumn: "Id");
        }
    }
}
