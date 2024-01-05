namespace WebMedicina.Shared.Dto.Usuarios { 
    public class UserInfoDto {
        public int IdMedico { get; set; }
        public string? UserLogin { get; set; }
        public string? Nombre { get; set; } 
        public string? Apellidos { get; set; } 
        public DateTime FechaNac { get; set; } 
        public DateTime FechaCreac { get; set; } 
        public DateTime FechaUltMod { get; set; }
        public string? Rol { get; set; } 
        public string? Sexo { get; set; } 
    }
}
