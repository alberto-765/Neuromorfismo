using Microsoft.AspNetCore.Identity;


namespace WebMedicina.BackEnd.Model {
    public class RoleModel :IdentityRole {
        public virtual ICollection<UserModel> Users { get; set; } = new List<UserModel>();
        public virtual ICollection<AspnetroleclaimModel> Aspnetroleclaims { get; set; } = new List<AspnetroleclaimModel>();

    }
}
