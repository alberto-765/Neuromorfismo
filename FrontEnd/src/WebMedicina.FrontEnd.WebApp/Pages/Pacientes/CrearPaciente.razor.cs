using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using WebMedicina.FrontEnd.Service;
using WebMedicina.FrontEnd.ServiceDependencies;
using WebMedicina.Shared.Dto;

namespace WebMedicina.FrontEnd.WebApp.Pages.Pacientes {
    public partial class CrearPaciente {
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        [CascadingParameter(Name = "excepcionPersonalizada")] ExcepcionPersonalizada excepcionPersonalizada { get; set; }
        [Inject] private IPacientesService _pacientesService { get; set; }


        // Campos formulario
        public bool OrdenarTalla { get; set; } // Mostrar un icono u otro en ordenar por talla
        CrearPacienteDto nuevoPaciente { get; set; } = new();
        private IEnumerable<EpilepsiasDto>? ListaEpilepsias { get; set; } = null;
        private IEnumerable<MutacionesDto>? ListaMutaciones { get; set; } = null;
        private string epilepsiaFiltrado { get; set; }
        private string mutacionFiltrado { get; set; }

        protected override async Task OnInitializedAsync() {
            try {
                await ObtenerFiltrosSelects();
            } catch (Exception ex) {
                excepcionPersonalizada.ConstruirPintarExcepcion(ex);
                throw;
            }
        }

        // Actualizar fecha de un datepicker
        private void ActFecha((DateTime? nuevaFecha, string nombrePicker) tupla) {
            try {
                if (!string.IsNullOrEmpty(tupla.nombrePicker)) {
                    switch (tupla.nombrePicker) {
                        case "fechaNac":
                            nuevoPaciente.FechaNac = tupla.nuevaFecha;
                        break;
                        case "fechaFrac":
                            nuevoPaciente.FechaFractalidad = tupla.nuevaFecha;
                        break;
                        case "fechaDiag":
                        nuevoPaciente.FechaDiagnostico = tupla.nuevaFecha;
                        break;
                    }
                }
            } catch (Exception ex) {
                excepcionPersonalizada.ConstruirPintarExcepcion(ex);
                throw;
            }
        }



        // Obtener listas de filtros
        private async Task ObtenerFiltrosSelects() {
            var opcionesSelects = await _pacientesService.ObtenerFiltros();

            // Asignamos la lista de epilepsias
            ListaEpilepsias = opcionesSelects.ListaEpilepsias;
            // Asignamos la lista de mutaciones
            ListaMutaciones = opcionesSelects.ListaMutaciones;
            // Asignamos la lista de farmacos
            //ListaFarmacos = opcionesSelects.ListaFarmacos;
        }


        // EVENTOS DIALOGO
        private void Crear() {
            try {
                MudDialog.Close(DialogResult.Ok(true));
            } catch (Exception ex) {
                excepcionPersonalizada.ConstruirPintarExcepcion(ex);
                throw;
            }
        }
        private void Cancel() => MudDialog.Cancel();

    }
}
