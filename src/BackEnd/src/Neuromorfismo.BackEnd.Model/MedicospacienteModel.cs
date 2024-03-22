namespace Neuromorfismo.BackEnd.Model;

/// <summary>
/// Relacion de que medicos pueden editar que pacientes
/// </summary>
public partial class MedicospacienteModel
{
    public int IdMedPac { get; set; }

    public int IdMedico { get; set; }

    public int IdPaciente { get; set; }

    public MedicosModel IdMedicoNavigation { get; set; } = null!;

    public PacientesModel IdPacienteNavigation { get; set; } = null!;
}
