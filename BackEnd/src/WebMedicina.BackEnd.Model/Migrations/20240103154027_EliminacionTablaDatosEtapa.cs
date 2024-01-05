using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebMedicina.BackEnd.Model.Migrations
{
    /// <inheritdoc />
    public partial class EliminacionTablaDatosEtapa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DatosEvoluEtapas");

            migrationBuilder.AddColumn<int>(
                name: "EvolucionPacienteId",
                table: "Pacientes",
                type: "int",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Pacientes_EvolucionPacienteId",
                table: "Pacientes",
                column: "EvolucionPacienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pacientes_EtapaLT_EvolucionPacienteId",
                table: "Pacientes",
                column: "EvolucionPacienteId",
                principalTable: "EtapaLT",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pacientes_EtapaLT_EvolucionPacienteId",
                table: "Pacientes");

            migrationBuilder.DropIndex(
                name: "IX_Pacientes_EvolucionPacienteId",
                table: "Pacientes");

            migrationBuilder.DropColumn(
                name: "EvolucionPacienteId",
                table: "Pacientes");

            migrationBuilder.CreateTable(
                name: "DatosEvoluEtapas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EtapaLTId = table.Column<int>(type: "int", nullable: false),
                    PacienteIdPaciente = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatosEvoluEtapas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DatosEvoluEtapas_EtapaLT_EtapaLTId",
                        column: x => x.EtapaLTId,
                        principalTable: "EtapaLT",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DatosEvoluEtapas_Pacientes_PacienteIdPaciente",
                        column: x => x.PacienteIdPaciente,
                        principalTable: "Pacientes",
                        principalColumn: "idPaciente",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "7cd8eadc-bd00-4a08-bf00-5e53c80a55f3");

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "27a602df-9efd-409c-9600-18eb21c48126");

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "c2d479bf-e6ce-42fb-9ecc-51d3c6308fa7");

            migrationBuilder.CreateIndex(
                name: "IX_DatosEvoluEtapas_EtapaLTId",
                table: "DatosEvoluEtapas",
                column: "EtapaLTId");

            migrationBuilder.CreateIndex(
                name: "IX_DatosEvoluEtapas_PacienteIdPaciente",
                table: "DatosEvoluEtapas",
                column: "PacienteIdPaciente");
        }
    }
}
