using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using MudBlazor;
using System.Net.Http.Json;
using WebMedicina.FrontEnd.Service;
using WebMedicina.FrontEnd.ServiceDependencies;
using WebMedicina.Shared.Dto.Pacientes;
using WebMedicina.Shared.Dto.Tipos;

namespace WebMedicina.FrontEnd.WebApp.Pages.Pacientes
{
    public partial class CrearPaciente {
        [CascadingParameter] MudDialogInstance MudDialog { get; set; } = null!;
        [Inject] private IPacientesService _pacientesService { get; set; } = null!;
        [Inject] private IJSRuntime _js { get; set; } = null!;
        [Inject] private ISnackbar _snackbar { get; set; } = null!;



        // Campos formulario
        private EditForm form { get; set; } = new();
        private CrearPacienteDto nuevoPaciente { get; set; } = new(); 
        [Parameter] public IEnumerable<EpilepsiasDto>? ListaEpilepsias { get; set; } = null;
        [Parameter] public IEnumerable<MutacionesDto>? ListaMutaciones { get; set; } = null;
        private bool _creandoPaciente { get; set; } = false;
        public bool PacienteValido { get; set; } = true;

        // Campos dialogo
        private const string idDialogo = "dialogoCrear";
        private string IdDialogo { get => $".{idDialogo}";}

        protected override void OnInitialized() {
            try {

                // Configuracion default snackbar
                _snackbar.Configuration.PreventDuplicates = true;
                _snackbar.Configuration.ShowTransitionDuration = 400;
                _snackbar.Configuration.HideTransitionDuration = 400;
                _snackbar.Configuration.PositionClass = Defaults.Classes.Position.TopLeft;
                _snackbar.Configuration.ClearAfterNavigation = true;
            } catch (Exception) {
                throw;
            }
        }

        // Boton crear
        private async Task Crear() {
            try {
                if (form.EditContext is not null && form.EditContext.Validate()) { 
                    _creandoPaciente = true;
                    HttpResponseMessage respuesta = await _pacientesService.CrearPaciente(nuevoPaciente);

                    // Mensaje para mostrar el usuario
                    bool pacienteCreado = true;
                    Severity tipoSnacbar = Severity.Success;
                    string mensaje = "Nuevo paciente creado exitosamente.";
                    int idPaciente = 0;

                    if (respuesta.IsSuccessStatusCode) {

                        // Validamos si el paciente ha podido ser creado
                        idPaciente = await respuesta.Content.ReadFromJsonAsync<int>();
                        if (idPaciente == 0) {
                            pacienteCreado = false;
                            mensaje = "El nuevo paciente no ha podido ser creado. Inténtelo de nuevo o conteacte con un administrador.";
                            tipoSnacbar = Severity.Error;
                        }
                    } else {
                        pacienteCreado = false;
                        mensaje = await respuesta.Content.ReadAsStringAsync();
                        tipoSnacbar = Severity.Error;
                    }

                    _creandoPaciente = false;
                    _snackbar.Add(mensaje, tipoSnacbar);

                    // Cerramos el dialogo
                    if (pacienteCreado) {
                        MudDialog.Close(DialogResult.Ok(idPaciente));
                    }
                }
            } catch (Exception) {
                throw;
            }
        }

        // Boton cancelar
        private void Cancel() => MudDialog.Cancel();
    }
}
