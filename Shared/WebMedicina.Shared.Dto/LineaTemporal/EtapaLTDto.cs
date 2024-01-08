using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using WebMedicina.Shared.Dto.Usuarios;

namespace WebMedicina.Shared.Dto.LineaTemporal {
    public record class EtapaLTDto {
        [JsonRequired]
        public int Id { get; set; }

        [JsonRequired]
        public string Titulo { get; set; } = null!;

        [JsonRequired]
        public string Label { get; set; } = null!;

        [JsonRequired]
        public string? Descripcion { get; set; }

        public UserInfoDto? MedicoCreador { get; set; }
        public UserInfoDto? MedicoUltModif { get; set; }
    }
}
