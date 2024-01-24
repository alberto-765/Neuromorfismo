using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebMedicina.BackEnd.Model.Seeds {
    public class NetUserSeed : IEntityTypeConfiguration<UserModel> {
        public void Configure(EntityTypeBuilder<UserModel> builder) {
            builder.HasData(new UserModel { Id = "212d1e1b-d876-4187-8e9a-faeb212a4853", UserName = "alberto", NormalizedUserName = "ALBERTO", PasswordHash = "AQAAAAIAAYagAAAAEHmX75b+BFQ/uawrWC7LvEpB4mcHHRBM9hSd3F9mT0CBJh3m/A1CBUkO0cmOmEEPlQ==", SecurityStamp = "DKP375TR5TRURFPMJQEU5OP7MFWZ3AVU", ConcurrencyStamp = "3686d3b1-b4d0-4121-a831-68f4b6ff6f1b", LockoutEnabled = true });
        }
    }

    public class MedicosSeed : IEntityTypeConfiguration<MedicosModel> {
        public void Configure(EntityTypeBuilder<MedicosModel> builder) {
            builder.HasData(new MedicosModel { IdMedico = 1, UserLogin = "alberto", Nombre = "Alberto", Apellidos = "Mimbrero Gu", FechaNac = new DateTime(2003, 02, 11), Sexo = "H", NetuserId = "212d1e1b-d876-4187-8e9a-faeb212a4853", FechaCreac = DateTime.Today, FechaUltMod = DateTime.Today});
        }
    }
}
