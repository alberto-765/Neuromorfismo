using Microsoft.AspNetCore.Components;
using Neuromorfismo.FrontEnd.Service;
using Neuromorfismo.FrontEnd.ServiceDependencies;
using Neuromorfismo.Shared.Dto.Pacientes;
using Neuromorfismo.Shared.Dto.Tipos;

namespace Neuromorfismo.FrontEnd.WebApp.Pages.Pacientes.CompFormCrearPac
{
    public partial class TipoMutacion <T> where T : BasePaciente {
        [Inject] private IComun _comun { get; set; } = null!;


        [Parameter] public T Paciente { get; set; } = null!; // Parametros
        [Parameter] public EventCallback<T> PacienteChanged { get; set; } // Callback para devolver el valor actualizado
        [Parameter] public IEnumerable<MutacionesDto>? ListaMutaciones { get; set; } = null; // Lista Mutaciones
        [Parameter] public string IdDialogo { get; set; } = string.Empty; // ID dialogo para bloquear/desbloquear scroll
        [Parameter] public string Label { get; set; } = "Tipo Mutación*"; // Label 

        /// <summary>
        /// Convertir Mutacion en String
        /// </summary>
        private Func<MutacionesDto, string> ConvertirMut = tipo => tipo.Nombre;
    }
}
