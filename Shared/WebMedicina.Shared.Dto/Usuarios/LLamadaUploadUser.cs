namespace WebMedicina.Shared.Dto.Usuarios
{
    public class LLamadaUploadUser
    {
        public UserUploadDto usuario { get; set; } = new();
        public bool rolModificado { get; set; }
    }
}
