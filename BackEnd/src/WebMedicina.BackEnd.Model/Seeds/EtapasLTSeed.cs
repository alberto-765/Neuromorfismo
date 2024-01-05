using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebMedicina.BackEnd.Model.Seeds {

    public class EtapasLTSeed : IEntityTypeConfiguration<EtapaLTModel> {

        public void Configure(EntityTypeBuilder<EtapaLTModel> builder) {
			try {
                builder.HasData(
                    new EtapaLTModel { Id = 1, Label = "¿Ha dado su consentimiento el paciente?", Descripcion = "", Titulo = "Consentimiento Informado", FechaCreac = DateTime.Today, FechaUltMod = DateTime.Today },
                    new EtapaLTModel { Id = 2, Label = "¿Ha dado su consentimiento el paciente?", Descripcion = "Descripcion", Titulo = "Paciente Acude a Extracción Analítica", FechaCreac = DateTime.Today, FechaUltMod = DateTime.Today },
                    new EtapaLTModel { Id = 3, Label = "¿Ha dado su consentimiento el paciente?", Descripcion = "", Titulo = "Muestra de Genética", FechaCreac = DateTime.Today, FechaUltMod = DateTime.Today }
                );
			} catch (Exception) {
				throw;
			}
        }
    }
}
