using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebMedicina.BackEnd.Model.Migrations
{
    /// <inheritdoc />
    public partial class EtapasForeignKey3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "7fb3b6d4-eb26-4c3a-8edf-c3f10aa8401d");

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "e4f255be-23be-486f-a1bc-677f975b59ca");

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "c233a4d5-12c0-4956-82f7-6f2bee91c495");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "eae10135-49fb-4c99-9968-c5dad74559b1");

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "59879106-0209-42f9-969e-5774b5761f6e");

            migrationBuilder.UpdateData(
                table: "aspnetroles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "e6cc37d4-e92b-4994-b276-e266bc8df86a");
        }
    }
}
