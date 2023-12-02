using System;
using System.Collections.Generic;

namespace WebMedicina.BackEnd.Model;

public partial class PacientesModel
{
    public int IdPaciente { get; set; }

    public string NumHistoria { get; set; } = null!;

    public DateTime FechaNac { get; set; }

    public string Sexo { get; set; } = null!;

    public decimal Talla { get; set; }

    public DateTime FechaDiagnostico { get; set; }

    public DateTime FechaFractalidad { get; set; }

    public string Farmaco { get; set; }

    public int? IdEpilepsia { get; set; }

    public int? IdMutacion { get; set; }

    public string EnfermRaras { get; set; } = null!;

    public string DescripEnferRaras { get; set; } = null!;

    public DateOnly FechaCreac { get; set; }

    public DateOnly FechaUltMod { get; set; }

    public int MedicoUltMod { get; set; }

    public int MedicoCreador { get; set; }

    public virtual EpilepsiaModel? IdEpilepsiaNavigation { get; set; }

    public virtual FarmacosModel? IdFarmacoNavigation { get; set; }

    public virtual MutacionesModel? IdMutacionNavigation { get; set; }

    public virtual MedicosModel MedicoCreadorNavigation { get; set; } = null!;

    public virtual MedicosModel MedicoUltModNavigation { get; set; } = null!;

    public virtual ICollection<MedicospacienteModel> Medicospacientes { get; set; } = new List<MedicospacienteModel>();
}
