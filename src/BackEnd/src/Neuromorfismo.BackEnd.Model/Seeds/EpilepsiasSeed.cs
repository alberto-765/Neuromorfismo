using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Neuromorfismo.BackEnd.Model.Seeds {
    public class EpilepsiasSeed : IEntityTypeConfiguration<EpilepsiaModel> {
        public void Configure(EntityTypeBuilder<EpilepsiaModel> builder) {
            builder.HasData(
                new EpilepsiaModel { IdEpilepsia = 1, Nombre = "Epilepsia1", FechaCreac = DateTime.Today, FechaUltMod = DateTime.Today },
                new EpilepsiaModel { IdEpilepsia = 2, Nombre = "Epilepsia2", FechaCreac = DateTime.Today, FechaUltMod = DateTime.Today }
            );
        }
    }
}
