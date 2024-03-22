namespace Neuromorfismo.Shared.Dto.Usuarios
{
    public class LLamadaUploadUserDto
    {
        public UserUploadDto usuario { get; set; } = new();
        public bool rolModificado { get; set; }
    }
}
