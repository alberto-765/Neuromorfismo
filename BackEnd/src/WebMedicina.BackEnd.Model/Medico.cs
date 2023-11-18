using System;
using System.Collections.Generic;

namespace WebMedicina.BackEnd.Model;

public partial class Medico
{
    public int IdMedico { get; set; }

    public string? UserLogin { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public DateTime FechaNac { get; set; }

    public string Sexo { get; set; } = null!;

    public DateOnly FechaCreac { get; set; }

    public string NetuserId { get; set; } = null!;

    public DateOnly FechaUltMod { get; set; }

    public virtual ICollection<Medicospaciente> Medicospacientes { get; set; } = new List<Medicospaciente>();

    public virtual Aspnetuser Netuser { get; set; } = null!;

    public virtual ICollection<Paciente> PacienteMedicoCreadorNavigations { get; set; } = new List<Paciente>();

    public virtual ICollection<Paciente> PacienteMedicoUltModNavigations { get; set; } = new List<Paciente>();
}
