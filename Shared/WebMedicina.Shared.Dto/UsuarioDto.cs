using System.ComponentModel.DataAnnotations;


namespace WebMedicina.Shared.Dto {
	public class UsuarioDto {
		public string Id { get; set; } = null!;

		[Required(ErrorMessage = "El Id del usuario es obligatorio")]
		public string? UserName { get; set; }

		[Required(ErrorMessage = "La contraseña es obligatoria")]
		public string? PasswordHash { get; set; }

		public DateTime? LockoutEnd { get; set; }

		public bool LockoutEnabled { get; set; }

		public int AccessFailedCount { get; set; }


	}
}
