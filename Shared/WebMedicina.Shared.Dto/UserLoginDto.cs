using System.ComponentModel.DataAnnotations;


namespace WebMedicina.Shared.Dto {
	public class UserLoginDto {

		[Required(ErrorMessage = "El nombre de usuario es obligatorio")]
		[MaxLength(50 , ErrorMessage = "Máximo de caracteres sobrepasado.")]
		public string UserName { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        public string Password { get; set; }
	}
}
