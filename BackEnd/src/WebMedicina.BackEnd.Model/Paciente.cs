using System;
using System.Collections.Generic;

namespace WebMedicina.BackEnd.Model;

public partial class Paciente
{
    public int IdPaciente { get; set; }

    public DateOnly FechaNac { get; set; }

    public string Sexo { get; set; } = null!;

    public decimal Talla { get; set; }

    public DateTime FechaDiagnostico { get; set; }

    public DateTime FechaFractalidad { get; set; }

    public int IdFarmaco { get; set; }

    public int IdEpilepsia { get; set; }

    public int IdMutacion { get; set; }

    public string EnfermRaras { get; set; } = null!;

    public string DescripEnferRaras { get; set; } = null!;

    public DateOnly FechaCreac { get; set; }

    public DateOnly FechaUltMod { get; set; }

    public int MedicoUltMod { get; set; }

    public virtual Epilepsia IdEpilepsiaNavigation { get; set; } = null!;

    public virtual Farmaco IdFarmacoNavigation { get; set; } = null!;

    public virtual Mutacione IdMutacionNavigation { get; set; } = null!;

    public virtual ICollection<Medicospaciente> Medicospacientes { get; set; } = new List<Medicospaciente>();
}
