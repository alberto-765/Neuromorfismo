using System.ComponentModel;
using System.Text.Json.Serialization;
using WebMedicina.Shared.Dto.Usuarios;

namespace WebMedicina.Shared.Dto.LineaTemporal {
    public record EvolucionLTDto {
        [JsonRequired]
        public int Id { get; set; }

        [JsonRequired]
        public bool Confirmado { get; set; }

        [JsonRequired]
        [Description("Fecha de la ultima modificacion de la evolucion del paciente.")]
        public DateTime Fecha { get; set; } = DateTime.Today;

        [JsonRequired]
        public UserInfoDto MedicoUltModif { get; set; } = null!;

        [JsonRequired]
        public virtual int IdEtapa { get; set; }
    }
}
