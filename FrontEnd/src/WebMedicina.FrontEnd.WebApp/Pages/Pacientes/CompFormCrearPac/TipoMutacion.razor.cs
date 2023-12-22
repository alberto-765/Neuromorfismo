using Microsoft.AspNetCore.Components;
using WebMedicina.FrontEnd.Service;
using WebMedicina.FrontEnd.ServiceDependencies;
using WebMedicina.Shared.Dto;
namespace WebMedicina.FrontEnd.WebApp.Pages.Pacientes.CompFormCrearPac {
    public partial class TipoMutacion  {
        [CascadingParameter(Name = "excepcionPersonalizada")] ExcepcionPersonalizada excepcionPersonalizada { get; set; }
        [Inject] private IPacientesService _pacientesService { get; set; }


        // Parametros
        [Parameter] public string? TipoMutacionSel { get; set; }

        // Callback para devolver el valor actualizado
        [Parameter] public EventCallback<string?> TipoMutacionSelChanged { get; set; }

        // Lista Mutaciones
        [CascadingParameter(Name = "ListaMutaciones")] public IEnumerable<MutacionesDto>? ListaMutaciones { get; set; } = null;

        // ID dialogo para bloquear/desbloquear scroll
        [Parameter] public string IdDialogo { get; set; } = string.Empty;

        // Bloquear scroll al abrir desplegable
        private async Task BloquearScroll() {
            try {
                if (!string.IsNullOrEmpty(IdDialogo)) {
                    await _pacientesService.BloquearScroll(IdDialogo);
                }
            } catch (Exception) {
                throw;
            }
        }

        // Desbloquear scroll al cerrar desblegable
        private async Task DesbloquearScroll() {
            try {
                if (!string.IsNullOrEmpty(IdDialogo)) {
                    await _pacientesService.DesbloquearScroll(IdDialogo);
                }
            } catch (Exception) {
                throw;
            }
        }
    }
}
