
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebMedicina.BackEnd.Model {
    public class UserModel : IdentityUser {

        public MedicosModel Medico { get; set; } = null!;

    }
}
