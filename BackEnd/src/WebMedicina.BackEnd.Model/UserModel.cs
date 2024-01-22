
using Microsoft.AspNetCore.Identity;

namespace WebMedicina.BackEnd.Model {
    public class UserModel : IdentityUser {
        public MedicosModel Medicos { get; set; } = new ();

        public ICollection<IdentityRole> Roles { get; set; } = new List<IdentityRole>();
        public ICollection<Aspnetuserclaim> Aspnetuserclaims { get; set; } = new List<Aspnetuserclaim>();

        public ICollection<Aspnetuserlogin> Aspnetuserlogins { get; set; } = new List<Aspnetuserlogin>();

        public ICollection<AspnetusertokenModel> Aspnetusertokens { get; set; } = new List<AspnetusertokenModel>();

    }
}
