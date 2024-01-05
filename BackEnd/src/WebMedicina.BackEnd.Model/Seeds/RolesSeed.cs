using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebMedicina.BackEnd.Model.Seeds {
    public class RolesSeed : IEntityTypeConfiguration<AspnetroleModel> {
        public void Configure(EntityTypeBuilder<AspnetroleModel> builder) {
            builder.HasData(
                new AspnetroleModel { Id= "1", Name="superAdmin",NormalizedName="SUPERADMIN",  ConcurrencyStamp = Guid.NewGuid().ToString() },
                new AspnetroleModel { Id = "2", Name ="admin", NormalizedName = "ADMIN",ConcurrencyStamp = Guid.NewGuid().ToString() },
                new AspnetroleModel { Id = "3", Name ="medico", NormalizedName = "MEDICO",ConcurrencyStamp = Guid.NewGuid().ToString() }
            );
        }
    }
}
