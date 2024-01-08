using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebMedicina.BackEnd.Model.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModelosLT2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdMedicoCreador",
                table: "EtapaLT",
                type: "int(11)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdMedicoUltModif",
                table: "EtapaLT",
                type: "int(11)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "EtapaLT",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "IdMedicoCreador", "IdMedicoUltModif" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "EtapaLT",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "IdMedicoCreador", "IdMedicoUltModif" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "EtapaLT",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "IdMedicoCreador", "IdMedicoUltModif" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "050c3101-f22a-45f8-aefd-61e34daf96a2");

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "3b3c081d-d1ba-4560-9013-9782a4c33642");

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "1ae4e64d-df94-4b9f-8c6f-885291d633dd");

            migrationBuilder.CreateIndex(
                name: "IX_EtapaLT_IdMedicoCreador",
                table: "EtapaLT",
                column: "IdMedicoCreador");

            migrationBuilder.CreateIndex(
                name: "IX_EtapaLT_IdMedicoUltModif",
                table: "EtapaLT",
                column: "IdMedicoUltModif");

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

            migrationBuilder.DropIndex(
                name: "IX_EtapaLT_IdMedicoCreador",
                table: "EtapaLT");

            migrationBuilder.DropIndex(
                name: "IX_EtapaLT_IdMedicoUltModif",
                table: "EtapaLT");

            migrationBuilder.DropColumn(
                name: "IdMedicoCreador",
                table: "EtapaLT");

            migrationBuilder.DropColumn(
                name: "IdMedicoUltModif",
                table: "EtapaLT");

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
        }
    }
}
