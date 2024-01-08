using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebMedicina.BackEnd.Model.Migrations
{
    /// <inheritdoc />
    public partial class ForeignKeyEvolucionLT2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EvolucionLT_Medicos_MedicoUltModifIdMedico",
                table: "EvolucionLT");

            migrationBuilder.DropIndex(
                name: "IX_EvolucionLT_MedicoUltModifIdMedico",
                table: "EvolucionLT");

            migrationBuilder.DropColumn(
                name: "MedicoUltModifIdMedico",
                table: "EvolucionLT");

            migrationBuilder.AlterColumn<int>(
                name: "IdMedicoUltModif",
                table: "EvolucionLT",
                type: "int(11)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "f3838c6d-c9bc-4296-9e00-6c75f7ed77da");

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "ed005d9f-a959-4350-8489-f86633b67b85");

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "4214f545-256c-489a-a80c-1730fb2b2f7a");

            migrationBuilder.CreateIndex(
                name: "IX_EvolucionLT_IdMedicoUltModif",
                table: "EvolucionLT",
                column: "IdMedicoUltModif");

            migrationBuilder.AddForeignKey(
                name: "FK_EvolucionLT_Medicos_IdMedicoUltModif",
                table: "EvolucionLT",
                column: "IdMedicoUltModif",
                principalTable: "Medicos",
                principalColumn: "idMedico",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EvolucionLT_Medicos_IdMedicoUltModif",
                table: "EvolucionLT");

            migrationBuilder.DropIndex(
                name: "IX_EvolucionLT_IdMedicoUltModif",
                table: "EvolucionLT");

            migrationBuilder.AlterColumn<int>(
                name: "IdMedicoUltModif",
                table: "EvolucionLT",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int(11)");

            migrationBuilder.AddColumn<int>(
                name: "MedicoUltModifIdMedico",
                table: "EvolucionLT",
                type: "int(11)",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "f2f6cc29-9ae1-4e8d-8509-04a1b659fd4f");

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "091bd551-c995-4e6e-bc7a-2f6f036ba6d3");

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "6cd199e8-ae59-433c-994b-75b0232a747f");

            migrationBuilder.CreateIndex(
                name: "IX_EvolucionLT_MedicoUltModifIdMedico",
                table: "EvolucionLT",
                column: "MedicoUltModifIdMedico");

            migrationBuilder.AddForeignKey(
                name: "FK_EvolucionLT_Medicos_MedicoUltModifIdMedico",
                table: "EvolucionLT",
                column: "MedicoUltModifIdMedico",
                principalTable: "Medicos",
                principalColumn: "idMedico",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
