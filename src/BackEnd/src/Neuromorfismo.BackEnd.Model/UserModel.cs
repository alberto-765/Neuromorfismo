using Microsoft.AspNetCore.Identity;

namespace Neuromorfismo.BackEnd.Model {
    public class UserModel : IdentityUser {

        public MedicosModel Medico { get; set; } = null!;

    }
}
