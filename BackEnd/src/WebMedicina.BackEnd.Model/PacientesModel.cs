using System;
using System.Collections.Generic;

namespace WebMedicina.BackEnd.Model;

public partial class PacientesModel
{
    public int IdPaciente { get; set; }

    public DateOnly FechaNac { get; set; }

    public string Sexo { get; set; } = null!;

    public decimal Talla { get; set; }

    public DateTime FechaDiagnostico { get; set; }

    public DateTime FechaFractalidad { get; set; }

    public int? IdFarmaco { get; set; }

    public int? IdEpilepsia { get; set; }

    public int? IdMutacion { get; set; }

    public string EnfermRaras { get; set; } = null!;

    public string DescripEnferRaras { get; set; } = null!;

    public DateOnly FechaCreac { get; set; }

    public DateOnly FechaUltMod { get; set; }

    public string? MedicoUltMod { get; set; }

    public string? MedicoCreador { get; set; }

    public virtual EpilepsiaModel? IdEpilepsiaNavigation { get; set; }

    public virtual FarmacosModel? IdFarmacoNavigation { get; set; }

    public virtual MutacionesModel? IdMutacionNavigation { get; set; }

    public virtual MedicosModel? MedicoCreadorNavigation { get; set; }

    public virtual MedicosModel? MedicoUltModNavigation { get; set; }

    public virtual ICollection<MedicospacienteModel> Medicospacientes { get; set; } = new List<MedicospacienteModel>();
}
