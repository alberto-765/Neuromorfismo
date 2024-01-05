using Microsoft.AspNetCore.Components;
using WebMedicina.FrontEnd.ServiceDependencies;
using WebMedicina.Shared.Dto.Pacientes;
using WebMedicina.Shared.Dto.Tipos;

namespace WebMedicina.FrontEnd.WebApp.Pages.Pacientes.CompFormCrearPac
{
    public partial class TipoEpilepsia <T> where T : BasePaciente {
        [Inject] private IComun _comun { get; set; } = null!;

        // Parametros
        [Parameter] public T Paciente { get; set; } = null!;

        // Callback para devolver el valor actualizado
        [Parameter] public EventCallback<T> PacienteChanged { get; set; }

        // Lista Epilepsias
        [Parameter] public IEnumerable<EpilepsiasDto>? ListaEpilepsias { get; set; } = null;

        // ID dialogo para bloquear/desbloquear scroll
        [Parameter] public string IdDialogo { get; set; } = string.Empty;

        /// <summary>
        /// Convertir Epilepsia en String
        /// </summary>
        private Func<EpilepsiasDto, string> ConvertirEpi = tipo => tipo.Nombre;

    }
}
