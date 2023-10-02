using System;
using System.Collections.Generic;

namespace WebMedicina.BackEnd.Model;

/// <summary>
/// Relacion de que medicos pueden editar que pacientes
/// </summary>
public partial class Medicospaciente
{
    public string Id { get; set; } = null!;

    public string IdUsuario { get; set; } = null!;

    public int IdPaciente { get; set; }

    public virtual Paciente IdPacienteNavigation { get; set; } = null!;

    public virtual Aspnetuser IdUsuarioNavigation { get; set; } = null!;
}
