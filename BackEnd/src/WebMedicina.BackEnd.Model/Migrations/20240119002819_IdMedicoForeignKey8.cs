using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebMedicina.BackEnd.Model.Migrations
{
    /// <inheritdoc />
    public partial class IdMedicoForeignKey8 : Migration
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
                table: "EtapaLT",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "IdMedicoCreador", "IdMedicoUltModif" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "EtapaLT",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "IdMedicoCreador", "IdMedicoUltModif" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "EtapaLT",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "IdMedicoCreador", "IdMedicoUltModif" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "bc2b71d3-569e-45cc-81a4-75b6a0a01083");

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "a33e42b3-5701-483d-a02e-3b722f9dc3f9");

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "a949706b-4117-4820-95a7-e7da04ac0464");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                value: "c60ad24a-b192-4afa-ab93-180cd2fea140");

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "4f893d94-a0cb-4e23-afd1-08f8755006a1");

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "8663cfe4-0836-40d2-962d-9dae6de9afe9");

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
    }
}
