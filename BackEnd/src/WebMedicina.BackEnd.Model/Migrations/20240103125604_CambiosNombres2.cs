using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebMedicina.BackEnd.Model.Migrations
{
    /// <inheritdoc />
    public partial class CambiosNombres2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DatosEvoluEtapasLTModel");

            migrationBuilder.DropTable(
                name: "EvolucionLTModels");

            migrationBuilder.DropTable(
                name: "EtapaLTModel");

            migrationBuilder.CreateTable(
                name: "EtapaLT",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Titulo = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Label = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MedicoCreadorIdMedico = table.Column<int>(type: "int(11)", nullable: false),
                    MedicoUltModifIdMedico = table.Column<int>(type: "int(11)", nullable: false),
                    FechaCreac = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    FechaUltMod = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EtapaLT", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EtapaLT_Medicos_MedicoCreadorIdMedico",
                        column: x => x.MedicoCreadorIdMedico,
                        principalTable: "Medicos",
                        principalColumn: "idMedico",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EtapaLT_Medicos_MedicoUltModifIdMedico",
                        column: x => x.MedicoUltModifIdMedico,
                        principalTable: "Medicos",
                        principalColumn: "idMedico",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DatosEvoluEtapas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PacienteIdPaciente = table.Column<int>(type: "int(11)", nullable: false),
                    EtapaLTId = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "EvolucionLT",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Confirmado = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    MedicoUltModifIdMedico = table.Column<int>(type: "int(11)", nullable: false),
                    EtapasLTId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvolucionLT", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EvolucionLT_EtapaLT_EtapasLTId",
                        column: x => x.EtapasLTId,
                        principalTable: "EtapaLT",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EvolucionLT_Medicos_MedicoUltModifIdMedico",
                        column: x => x.MedicoUltModifIdMedico,
                        principalTable: "Medicos",
                        principalColumn: "idMedico",
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

            migrationBuilder.CreateIndex(
                name: "IX_EtapaLT_MedicoCreadorIdMedico",
                table: "EtapaLT",
                column: "MedicoCreadorIdMedico");

            migrationBuilder.CreateIndex(
                name: "IX_EtapaLT_MedicoUltModifIdMedico",
                table: "EtapaLT",
                column: "MedicoUltModifIdMedico");

            migrationBuilder.CreateIndex(
                name: "IX_EvolucionLT_EtapasLTId",
                table: "EvolucionLT",
                column: "EtapasLTId");

            migrationBuilder.CreateIndex(
                name: "IX_EvolucionLT_MedicoUltModifIdMedico",
                table: "EvolucionLT",
                column: "MedicoUltModifIdMedico");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DatosEvoluEtapas");

            migrationBuilder.DropTable(
                name: "EvolucionLT");

            migrationBuilder.DropTable(
                name: "EtapaLT");

            migrationBuilder.CreateTable(
                name: "EtapaLTModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MedicoCreadorIdMedico = table.Column<int>(type: "int(11)", nullable: false),
                    MedicoUltModifIdMedico = table.Column<int>(type: "int(11)", nullable: false),
                    FechaCreac = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    FechaUltMod = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Label = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nombre = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Titulo = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EtapaLTModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EtapaLTModel_Medicos_MedicoCreadorIdMedico",
                        column: x => x.MedicoCreadorIdMedico,
                        principalTable: "Medicos",
                        principalColumn: "idMedico",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EtapaLTModel_Medicos_MedicoUltModifIdMedico",
                        column: x => x.MedicoUltModifIdMedico,
                        principalTable: "Medicos",
                        principalColumn: "idMedico",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DatosEvoluEtapasLTModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EtapaLTId = table.Column<int>(type: "int", nullable: false),
                    PacienteIdPaciente = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatosEvoluEtapasLTModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DatosEvoluEtapasLTModel_EtapaLTModel_EtapaLTId",
                        column: x => x.EtapaLTId,
                        principalTable: "EtapaLTModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DatosEvoluEtapasLTModel_Pacientes_PacienteIdPaciente",
                        column: x => x.PacienteIdPaciente,
                        principalTable: "Pacientes",
                        principalColumn: "idPaciente",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EvolucionLTModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EtapasLTId = table.Column<int>(type: "int", nullable: false),
                    MedicoUltModifIdMedico = table.Column<int>(type: "int(11)", nullable: false),
                    Confirmado = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvolucionLTModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EvolucionLTModels_EtapaLTModel_EtapasLTId",
                        column: x => x.EtapasLTId,
                        principalTable: "EtapaLTModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EvolucionLTModels_Medicos_MedicoUltModifIdMedico",
                        column: x => x.MedicoUltModifIdMedico,
                        principalTable: "Medicos",
                        principalColumn: "idMedico",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "f46e113c-4c09-4d5e-b38e-770544aaddce");

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "8b8bfd36-9d45-496a-bef5-eba9aca595fa");

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "646668a5-9c13-46fc-8d57-a7e104d4926d");

            migrationBuilder.CreateIndex(
                name: "IX_DatosEvoluEtapasLTModel_EtapaLTId",
                table: "DatosEvoluEtapasLTModel",
                column: "EtapaLTId");

            migrationBuilder.CreateIndex(
                name: "IX_DatosEvoluEtapasLTModel_PacienteIdPaciente",
                table: "DatosEvoluEtapasLTModel",
                column: "PacienteIdPaciente");

            migrationBuilder.CreateIndex(
                name: "IX_EtapaLTModel_MedicoCreadorIdMedico",
                table: "EtapaLTModel",
                column: "MedicoCreadorIdMedico");

            migrationBuilder.CreateIndex(
                name: "IX_EtapaLTModel_MedicoUltModifIdMedico",
                table: "EtapaLTModel",
                column: "MedicoUltModifIdMedico");

            migrationBuilder.CreateIndex(
                name: "IX_EvolucionLTModels_EtapasLTId",
                table: "EvolucionLTModels",
                column: "EtapasLTId");

            migrationBuilder.CreateIndex(
                name: "IX_EvolucionLTModels_MedicoUltModifIdMedico",
                table: "EvolucionLTModels",
                column: "MedicoUltModifIdMedico");
        }
    }
}
