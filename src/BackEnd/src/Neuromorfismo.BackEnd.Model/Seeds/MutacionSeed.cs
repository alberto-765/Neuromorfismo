using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Neuromorfismo.BackEnd.Model.Seeds {
    public class MutacionSeed : IEntityTypeConfiguration<MutacionesModel> {
        public void Configure(EntityTypeBuilder<MutacionesModel> builder) {
            builder.HasData(
                new MutacionesModel { IdMutacion = 1, Nombre = "Mutacion1", FechaCreac = DateTime.Today, FechaUltMod = DateTime.Today},
                new MutacionesModel { IdMutacion = 2, Nombre = "Mutacion2", FechaCreac = DateTime.Today, FechaUltMod = DateTime.Today}
            ) ;        
        }
    }
}
