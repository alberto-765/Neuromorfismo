using System;
using System.Collections.Generic;
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

    public virtual ICollection<MedicospacienteModel> Medicospacientes { get; set; } = new List<MedicospacienteModel>();

    public virtual AspnetuserModel Netuser { get; set; } = null!;

    public virtual ICollection<PacientesModel> PacienteMedicoCreadorNavigations { get; set; } = new List<PacientesModel>();

    public virtual ICollection<PacientesModel> PacienteMedicoUltModNavigations { get; set; } = new List<PacientesModel>();

    // Propiedad la cual no se mapea porque no existe en la BBDD
    [NotMapped]
    public string Rol { get; set; } = null!;
}
