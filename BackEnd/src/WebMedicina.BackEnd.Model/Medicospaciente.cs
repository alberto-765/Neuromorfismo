using System;
using System.Collections.Generic;

namespace WebMedicina.BackEnd.Model;

/// <summary>
/// Relacion de que medicos pueden editar que pacientes
/// </summary>
public partial class Medicospaciente
{
    public int IdMedPac { get; set; }

    public int IdMedico { get; set; }

    public int IdPaciente { get; set; }

    public virtual Medico IdMedicoNavigation { get; set; } = null!;

    public virtual Paciente IdPacienteNavigation { get; set; } = null!;
}
