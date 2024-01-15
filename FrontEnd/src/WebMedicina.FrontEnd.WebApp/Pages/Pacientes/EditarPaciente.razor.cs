using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using MudBlazor;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;
using WebMedicina.FrontEnd.Service;
using WebMedicina.FrontEnd.ServiceDependencies;
using WebMedicina.Shared.Dto.Pacientes;
using WebMedicina.Shared.Dto.Tipos;

namespace WebMedicina.FrontEnd.WebApp.Pages.Pacientes
{
    public partial class EditarPaciente {
        [CascadingParameter] MudDialogInstance MudDialog { get; set; } = null!;
        [Inject] private IPacientesService _pacientesService { get; set; } = null!;
        [Inject] private IJSRuntime _js { get; set; } = null!;
        [Inject] private ISnackbar _snackbar { get; set; } = null!;



        // Campos formulario
        private EditForm form { get; set; } = new();
        [Parameter] public CrearPacienteDto nuevoPaciente { get; set; } = null!;// Debe pasarse por parametro
        [Parameter] public IEnumerable<EpilepsiasDto>? ListaEpilepsias { get; set; } = null;
        [Parameter] public IEnumerable<MutacionesDto>? ListaMutaciones { get; set; } = null;
        private bool _creandoPaciente { get; set; } = false; 
        private bool PacienteValido { get; set; } = true;

        // Campos dialogo
        private const string idDialogo = "dialogoCrear";
        private string IdDialogo { get => $".{idDialogo}"; }

        protected override void OnInitialized() { 
            // Configuracion default snackbar
            _snackbar.Configuration.PreventDuplicates = true;
            _snackbar.Configuration.ShowTransitionDuration = 300;
            _snackbar.Configuration.HideTransitionDuration = 300;
            _snackbar.Configuration.PositionClass = Defaults.Classes.Position.TopLeft;
            _snackbar.Configuration.ShowCloseIcon = false; 
        }

        // Boton crear
        private async Task Editar() {
            try {
                if (form.EditContext is not null && form.EditContext.Validate()) {
                    _creandoPaciente = true;
                    HttpResponseMessage respuesta =  await _pacientesService.EditarPaciente(nuevoPaciente);

                    // Mensaje para mostrar el usuario
                    bool pacienteEditado = true;
                    Severity tipoSnacbar = Severity.Success;
                    string mensaje = "Paciente editado exitosamente.";

                    if (respuesta.IsSuccessStatusCode) {

                        // Validamos si el paciente ha podido ser creado
                        if(await respuesta.Content.ReadFromJsonAsync<bool>() == false) {
                            pacienteEditado = false;
                            mensaje = "El paciente no ha podido ser editado. Inténtelo de nuevo o conteacte con un administrador.";
                            tipoSnacbar = Severity.Error;
                        }
                    } else {
                        pacienteEditado = false;
                        mensaje = await respuesta.Content.ReadAsStringAsync();
                        tipoSnacbar = Severity.Error;
                    }

                    _creandoPaciente = false;
                    _snackbar.Add(mensaje, tipoSnacbar);

                    // Cerramos el dialogo
                    if(pacienteEditado) {
                        MudDialog.Close(nuevoPaciente);
                    } else {
                        MudDialog.Close();
                    }
                }
            } catch (Exception) {
                Cancel();
            }
        }

        // Boton cancelar
        private void Cancel() => MudDialog.Cancel();

    }
}
