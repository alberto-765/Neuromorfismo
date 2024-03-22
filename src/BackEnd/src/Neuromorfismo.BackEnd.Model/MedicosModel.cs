using System.ComponentModel.DataAnnotations.Schema;

namespace Neuromorfismo.BackEnd.Model;

public partial class MedicosModel : BaseModel
{
    public int IdMedico { get; set; }

    public string UserLogin { get; set; } = default!;

    public string Nombre { get; set; } = default!;

    public string Apellidos { get; set; } = default!;

    public DateTime FechaNac { get; set; }

    public string Sexo { get; set; } = default!;

    public string NetuserId { get; set; } = default!;

    [ForeignKey(nameof(NetuserId))]
    public UserModel Netuser { get; set; } = default!;
    

    public ICollection<MedicospacienteModel> Medicospacientes { get; set; } = new List<MedicospacienteModel>();


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
