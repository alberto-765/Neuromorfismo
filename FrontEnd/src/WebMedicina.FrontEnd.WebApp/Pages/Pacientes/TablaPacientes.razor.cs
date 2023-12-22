using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using WebMedicina.FrontEnd.Service;
using WebMedicina.FrontEnd.ServiceDependencies;
using WebMedicina.Shared.Dto;
using static MudBlazor.CategoryTypes;

namespace WebMedicina.FrontEnd.WebApp.Pages.Pacientes {
    public partial class TablaPacientes {
        [CascadingParameter(Name = "excepcionPersonalizada")] ExcepcionPersonalizada excepcionPersonalizada { get; set; }
        [Inject] private IPacientesService _pacientesService { get; set; }
        [Inject] private ISnackbar _snackbar { get; set; }
        [Inject] private IDialogService DialogService { get; set; }
        [Inject] private IComun _comun { get; set; }


        // Parametros
        [Parameter] public List<CrearPacienteDto>? ListaPacientes { get; set; }
        [CascadingParameter(Name = "ListaEpilepsias")] public IEnumerable<EpilepsiasDto>? ListaEpilepsias { get; set; } = null;
        [CascadingParameter(Name = "ListaMutaciones")] public IEnumerable<MutacionesDto>? ListaMutaciones { get; set; } = null;


        // Campos para la edicion de un paciente
        private bool PacienteValido { get; set; } = false;

        // Configuracion de dialogo de edicion
        private DialogOptions OpcionesDialogo { get; set; } = new DialogOptions { FullWidth = true, CloseButton = true, MaxWidth= MaxWidth.Small, 
            DisableBackdropClick = true, Position = DialogPosition.Center};

        // Realizar busqueda por search
        private string _searchString { get; set; } = string.Empty;


        // Evento para eliminar paciente
        void EliminarPaciente(CrearPacienteDto paciente) {
            try {
                Console.WriteLine(JsonSerializer.Serialize(paciente));
            } catch (Exception) {
                throw;
            }
        }

        // Filtrar por search
        private bool Search(CrearPacienteDto paciente) {
            try {
                bool valido = false;
                
                // Filtramos la busqueda por el campo search
                if (string.IsNullOrWhiteSpace(_searchString)) {
                    valido = true;
                } else if (paciente.NumHistoria.Contains(_searchString, StringComparison.OrdinalIgnoreCase)) {
                    valido = true;
                } else if (paciente.FechaNac is not null && paciente.FechaNac.ToString().Contains(_searchString, StringComparison.OrdinalIgnoreCase)) {
                    valido = true;
                } else if (paciente.FechaDiagnostico is not null && paciente.FechaDiagnostico.ToString().Contains(_searchString, StringComparison.OrdinalIgnoreCase)) {
                    valido = true;
                } else if (paciente.FechaFractalidad is not null && paciente.FechaFractalidad.ToString().Contains(_searchString, StringComparison.OrdinalIgnoreCase)) {
                    valido = true;
                } else if (paciente.Farmaco.Contains(_searchString, StringComparison.OrdinalIgnoreCase)) {
                    valido = true;
                } else if (paciente.NombreEpilepsia?.IndexOf(_searchString, StringComparison.OrdinalIgnoreCase) >= 0) {
                    valido = true;
                } else if (paciente.NombreMutacion?.IndexOf(_searchString, StringComparison.OrdinalIgnoreCase) >= 0) {
                    valido = true;
                } else if (paciente.Talla.ToString().Equals(_searchString, StringComparison.OrdinalIgnoreCase)) {
                    valido = true;
                }

                return valido;
            } catch (Exception ex) {
                excepcionPersonalizada.ConstruirPintarExcepcion(ex);
                throw;
            }
        }

        private async void MostrarMensajeError(List<ValidationResult> errores) {
            await DialogService.ShowMessageBox(
                "Campos no válidos",
                new MarkupString(_comun.GenerarHtmlErrores(errores)),
                yesText: "Entendido");
        }


        // Mostrar dialogo para editar paciente
        private async Task MostrarEditarPac(CrearPacienteDto nuevoPaciente) {
            try {
                DialogParameters parametros = new() {
                    { "ListaEpilepsias", ListaEpilepsias },
                    { "ListaMutaciones", ListaMutaciones },
                    { "nuevoPaciente", nuevoPaciente },
                };
                var dialog = DialogService.Show<CrearPaciente>("Nuevo Paciente", parametros, OpcionesDialogo);
                var result = await dialog.Result;

                // Validamos que el dialogo haya devuelto el nuevo paciente creado y actualizamos la lista
                if (result.Cancelled == false && result.Data is CrearPacienteDto) {
                    ListaPacientes = await _pacientesService.AnadirPacienteALista((CrearPacienteDto)result.Data);
                }
            } catch (Exception ex) {
                excepcionPersonalizada.ConstruirPintarExcepcion(ex);
                throw;
            }
        }
    }
}
