using WebMedicina.Shared.Dto.Usuarios;

namespace WebMedicina.Shared.Dto.UserAccount {
    public class RestartPasswordDto {
        public UserInfoDto? Medico { get; set; } = null; 

        public string Password { get; set; } = string.Empty;
    }
}
