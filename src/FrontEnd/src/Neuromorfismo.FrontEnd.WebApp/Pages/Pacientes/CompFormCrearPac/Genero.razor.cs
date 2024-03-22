using Microsoft.AspNetCore.Components;
using Neuromorfismo.FrontEnd.ServiceDependencies;
using Neuromorfismo.Shared.Dto.Pacientes;

namespace Neuromorfismo.FrontEnd.WebApp.Pages.Pacientes.CompFormCrearPac
{
    public partial class Genero <T> 
        where T : BasePaciente  {
        [Inject] private IComun _comun { get; set; } = null!;


        [Parameter] public T Paciente { get; set; } = null!; // Parametros
        [Parameter] public EventCallback<T> PacienteChanged { get; set; } // Callback para devolver el valor actualizado
        [Parameter] public string IdDialogo { get; set; } = string.Empty; // ID dialogo para bloquear/desbloquear scroll
        [Parameter] public string Label { get; set; } = "Género*";
    }
}
