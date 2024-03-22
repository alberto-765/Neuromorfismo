namespace Neuromorfismo.BackEnd.Model;

public partial class MutacionesModel : BaseModel
{
    public int IdMutacion { get; set; }

    public string Nombre { get; set; } = null!;

    public ICollection<PacientesModel> Pacientes { get; set; } = new List<PacientesModel>();
}
