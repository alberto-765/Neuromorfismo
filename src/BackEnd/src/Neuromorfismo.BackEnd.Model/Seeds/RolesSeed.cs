using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Neuromorfismo.BackEnd.Model.Seeds {
    public class RolesSeed : IEntityTypeConfiguration<RoleModel> {
        public void Configure(EntityTypeBuilder<RoleModel> builder) {
            builder.HasData(
                new RoleModel { Id= "1", Name="superAdmin",NormalizedName="SUPERADMIN",  ConcurrencyStamp = Guid.NewGuid().ToString() },
                new RoleModel { Id = "2", Name ="admin", NormalizedName = "ADMIN",ConcurrencyStamp = Guid.NewGuid().ToString() },
                new RoleModel { Id = "3", Name ="medico", NormalizedName = "MEDICO",ConcurrencyStamp = Guid.NewGuid().ToString() }
            );
        }
    }
}
