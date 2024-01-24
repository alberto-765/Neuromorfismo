using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebMedicina.BackEnd.Model.Migrations
{
    /// <inheritdoc />
    public partial class TriggersIniciales : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("CREATE TRIGGER `pacientes_after_insert` AFTER INSERT ON `pacientes` FOR EACH ROW BEGIN\r\n\tINSERT INTO medicospacientes(IdMedico, IdPaciente) VALUES (NEW.medicoCreador, NEW.idPaciente);\r\nEND;\r\n");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP TRIGGER pacientes_after_insert");
        }
    }
}
