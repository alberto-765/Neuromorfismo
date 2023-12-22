using Microsoft.AspNetCore.Components;
using WebMedicina.FrontEnd.Service;
using WebMedicina.FrontEnd.ServiceDependencies;
using WebMedicina.Shared.Dto;

namespace WebMedicina.FrontEnd.WebApp.Pages.Pacientes {
    public partial class FiltrosPaciente {
        [CascadingParameter(Name = "excepcionPersonalizada")] ExcepcionPersonalizada excepcionPersonalizada { get; set; }
        [CascadingParameter(Name = "modoOscuro")] bool IsDarkMode { get; set; } // Modo oscuro
        [Inject] private IPacientesService _pacientesService { get; set; }


        // Para identificar si el panel debe estar abierto o no
        [Parameter] public bool FiltrosAbierto { get; set; }
        [Parameter] public EventCallback<bool> FiltrosAbiertoChanged { get; set; }

        // Lista de pacientes bindeada bidireccinalmente
        [Parameter] public List<CrearPacienteDto>? ListaPacientes { get; set; }
        [Parameter] public EventCallback<List<CrearPacienteDto>?> ListaPacientesChanged { get; set; }

        // Listas no bindeadas 
        [CascadingParameter(Name = "ListaEpilepsias")] public IEnumerable<EpilepsiasDto>? ListaEpilepsias { get; set; } = null;
        [CascadingParameter(Name = "ListaMutaciones")] public IEnumerable<MutacionesDto>? ListaMutaciones { get; set; } = null;

        // Filtros seleccionados
        private FiltroPacienteDto FiltrosPacientes { get; set; } = new();

        // Lista de medicos para filtrar
        private IEnumerable<string>? ListaMedicos { get; set; } = null;

        // Mostrar un icono u otro en ordenar por talla
        public bool OrdenarTalla { get; set; }

        // String que se puestra en el select
        private string? EpilepsiaFiltrado { get; set; }
        private string? MutacionFiltrado { get; set; }

        // Filtrar Pacientes 
        private async Task ObtenerPacientesFiltrados() {
            try {
                ListaPacientes = await _pacientesService.FiltrarPacientes(FiltrosPacientes, ListaPacientes);
            } catch (Exception ex) {
                excepcionPersonalizada.ConstruirPintarExcepcion(ex);
                throw;
            }
        }

        // Buscador para autocomplete de medicos
        private async Task<IEnumerable<string>> BuscarMedPac(string medico) {
            try {
                // Si la lista es null se obtiene por primera vez de BD
                ListaMedicos ??= await _pacientesService.ObtenerAllMed();

                // Si hay medicos en la lista se realiza la busqueda
                if (ListaMedicos != null && ListaMedicos.Any()) {
                    return ListaMedicos.Where(q => q.Contains(medico, StringComparison.InvariantCultureIgnoreCase));
                } else {
                    return Enumerable.Empty<string>();
                }
            } catch (Exception ex) {
                excepcionPersonalizada.ConstruirPintarExcepcion(ex);
                throw;
            }
        }

        // Evento callback para acutualizar uno de los campos del paciente
        public async Task ActualizarFiltros(FiltroPacienteDto paciente) {
            try {
                FiltrosPacientes = paciente;
            } catch (Exception ex) {
                excepcionPersonalizada.ConstruirPintarExcepcion(ex);
                throw;
            }
        }

        private void ResetearFiltrado() {
            try {
                FiltrosPacientes = new();
            } catch (Exception) {
                throw;
            }
        }

    }
}
