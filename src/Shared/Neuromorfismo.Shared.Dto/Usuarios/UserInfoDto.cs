namespace Neuromorfismo.Shared.Dto.Usuarios { 
    public record UserInfoDto {
        public int IdMedico { get; init; }
        public string UserLogin { get; init; } = default!;
        public string? Nombre { get; init; } 
        public string? Apellidos { get; init; } 
        public DateTime FechaNac { get; init; } 
        public DateTime FechaCreac { get; init; } 
        public DateTime FechaUltMod { get; init; }
        public string? Rol { get; init; } 
        public string? Sexo { get; init; } 
    }
}
