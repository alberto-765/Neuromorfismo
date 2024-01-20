using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebMedicina.BackEnd.Model.Migrations
{
    /// <inheritdoc />
    public partial class QuitarColumnaUltimaEtapa2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pacientes_EtapaLT_EtapaLTModelId",
                table: "Pacientes");

            migrationBuilder.DropIndex(
                name: "IX_Pacientes_EtapaLTModelId",
                table: "Pacientes");

            migrationBuilder.DropColumn(
                name: "EtapaLTModelId",
                table: "Pacientes");

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "218c3cd7-748b-4c60-839c-b1cd592f32bc");

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "843b194b-b9db-46eb-8955-0d6a4c1dfb2e");

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "6e8c66f1-3bc5-4312-92b3-6df10a58c1e6");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EtapaLTModelId",
                table: "Pacientes",
                type: "int",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Pacientes_EtapaLTModelId",
                table: "Pacientes",
                column: "EtapaLTModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pacientes_EtapaLT_EtapaLTModelId",
                table: "Pacientes",
                column: "EtapaLTModelId",
                principalTable: "EtapaLT",
                principalColumn: "Id");
        }
    }
}
