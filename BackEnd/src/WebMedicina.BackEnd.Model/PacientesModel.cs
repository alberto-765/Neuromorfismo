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

    public int? IdFarmaco { get; set; } = null;

    public int? IdEpilepsia { get; set; } = null;

    public int? IdMutacion { get; set; } = null;

    public string EnfermRaras { get; set; } = null!;

    public string DescripEnferRaras { get; set; } = null!;

    public DateOnly FechaCreac { get; set; }

    public DateOnly FechaUltMod { get; set; }

    public string? MedicoUltMod { get; set; } = null;

    public string? MedicoCreador { get; set; } = null;

    public virtual MedicosModel MedicoCreadorNavigation { get; set; } = null!;

    public virtual MedicosModel MedicoUltModNavigation { get; set; } = null!;

    public virtual ICollection<MedicospacienteModel> Medicospacientes { get; set; } = new List<MedicospacienteModel>();
}
