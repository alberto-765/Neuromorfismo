using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebMedicina.BackEnd.Model.Migrations
{
    /// <inheritdoc />
    public partial class SeedEtapasActualizados : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "EtapaLT",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "IdMedicoCreador", "IdMedicoUltModif" },
                values: new object[] { 1, 1 });

            migrationBuilder.UpdateData(
                table: "EtapaLT",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "IdMedicoCreador", "IdMedicoUltModif" },
                values: new object[] { 1, 1 });

            migrationBuilder.UpdateData(
                table: "EtapaLT",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "IdMedicoCreador", "IdMedicoUltModif" },
                values: new object[] { 1, 1 });

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "9bdeaf59-4a0d-45b0-b737-f3edd052d3b4");

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "02aface8-a82a-4313-941c-397d6d0f3c8c");

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "b4f710b0-b39c-42e5-8bb1-13f77fae3da8");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
