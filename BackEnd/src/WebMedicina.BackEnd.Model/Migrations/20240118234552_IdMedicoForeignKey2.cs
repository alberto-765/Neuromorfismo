using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebMedicina.BackEnd.Model.Migrations
{
    /// <inheritdoc />
    public partial class IdMedicoForeignKey2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "3f9826b0-24e8-4a05-bdd0-9cdc31c35316");

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "cdb697b4-7fb8-47c4-8b0e-d1ca8896bd19");

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "721df0ad-a544-4cd6-84cb-e569d46007da");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "12c8bb76-197e-4ee9-a4e5-2ae575962daa");

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "8bbd4f41-1c90-47d8-ab56-920ab1337004");

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "be82ec16-8699-4d95-9e39-6d467314c928");
        }
    }
}
