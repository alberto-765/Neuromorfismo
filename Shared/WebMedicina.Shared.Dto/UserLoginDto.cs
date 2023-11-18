using System.ComponentModel.DataAnnotations;


namespace WebMedicina.Shared.Dto {
	public class UserLoginDto {

		[Required(ErrorMessage = "El nombre de usuario es obligatorio")]
		[MaxLength(12 , ErrorMessage = "El usuario debe contener 12 dígitos")]
		[MinLength(12 , ErrorMessage = "El usuario debe contener 12 dígitos")]
		public string UserName { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        public string Password { get; set; }
	}
}
