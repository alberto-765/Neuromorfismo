using System.ComponentModel.DataAnnotations.Schema;

namespace WebMedicina.BackEnd.Model;

public partial class MedicosModel : BaseModel
{
    public int IdMedico { get; set; }

    public string UserLogin { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public DateTime FechaNac { get; set; }

    public string Sexo { get; set; } = null!;

    public string NetuserId { get; set; } = null!;

    public ICollection<MedicospacienteModel> Medicospacientes { get; set; } = new List<MedicospacienteModel>();

    public UserModel Netuser { get; set; } = null!;

    public ICollection<PacientesModel> PacienteMedicoCreadorNavigations { get; set; } = new List<PacientesModel>();

    public ICollection<PacientesModel> PacienteMedicoUltModNavigations { get; set; } = new List<PacientesModel>();


    // RELACIONES ETAPAS LINEA TEMPORAL
    [InverseProperty("MedicoCreador")]
    public ICollection<EtapaLTModel> EtapaMedicoCreador { get; set; } = new List<EtapaLTModel>();

    [InverseProperty("MedicoUltModif")]
    public ICollection<EtapaLTModel> EtapaMedicoUltModif { get; set; } = new List<EtapaLTModel>();

    // RELACIONES EVOLUCION LINEA TEMPORAL
    public ICollection<EvolucionLTModel> EvolucionMedicoUltModif { get; set; } = new List<EvolucionLTModel>();

    public UserRefreshTokens? RefreshToken { get; set; }

    // Propiedad la cual no se mapea porque no existe en la BBDD
    [NotMapped]
    public string Rol { get; set; } = null!;
}
