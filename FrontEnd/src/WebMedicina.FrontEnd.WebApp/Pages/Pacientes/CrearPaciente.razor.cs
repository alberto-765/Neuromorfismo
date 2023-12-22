using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using MudBlazor;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;
using WebMedicina.FrontEnd.Service;
using WebMedicina.FrontEnd.ServiceDependencies;
using WebMedicina.Shared.Dto;
namespace WebMedicina.FrontEnd.WebApp.Pages.Pacientes {
    public partial class CrearPaciente {
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        [CascadingParameter(Name = "excepcionPersonalizada")] ExcepcionPersonalizada excepcionPersonalizada { get; set; }
        [Inject] private IPacientesService _pacientesService { get; set; }
        [Inject] private IJSRuntime _js { get; set; }
        [Inject] private ISnackbar _snackbar { get; set; }



        // Campos formulario
        private EditForm form { get; set; } = new();
        public bool OrdenarTalla { get; set; } // Mostrar un icono u otro en ordenar por talla
        private CrearPacienteDto nuevoPaciente { get; set; } = new(); 
        [Parameter] public IEnumerable<EpilepsiasDto>? ListaEpilepsias { get; set; } = null;
        [Parameter] public IEnumerable<MutacionesDto>? ListaMutaciones { get; set; } = null;
        private bool _creandoPaciente { get; set; } = false; 
        private bool _crearDisabled { get; set; } = false;

        // Campos dialogo
        private const string idDialogo = "dialogoCrear";

        protected override void OnInitialized() {
            try {

                // Configuracion default snackbar
                _snackbar.Configuration.PreventDuplicates = true;
                _snackbar.Configuration.ShowTransitionDuration = 300;
                _snackbar.Configuration.HideTransitionDuration = 300;
                _snackbar.Configuration.PositionClass = Defaults.Classes.Position.TopLeft;
                _snackbar.Configuration.ShowCloseIcon = false;
            } catch (Exception ex) {
                excepcionPersonalizada.ConstruirPintarExcepcion(ex);
                throw;
            }
        }

        // Boton crear
        private async Task Crear() {
            try {
                if (form.EditContext.Validate()) {
                    _creandoPaciente = true;
                    HttpResponseMessage respuesta =  await _pacientesService.CrearPaciente(nuevoPaciente);

                    // Mensaje para mostrar el usuario
                    bool pacienteCreado = true;
                    Severity tipoSnacbar = Severity.Success;
                    string mensaje = "Nuevo paciente creado exitosamente.";

                    if (respuesta.IsSuccessStatusCode) {

                        // Validamos si el paciente ha podido ser creado
                        if(await respuesta.Content.ReadFromJsonAsync<bool>() == false) {
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
                    if(pacienteCreado) {
                        MudDialog.Close(DialogResult.Ok(nuevoPaciente));
                    }
                }
            } catch (Exception ex) {
                excepcionPersonalizada.ConstruirPintarExcepcion(ex);
                throw;
            }
        }

        // Boton cancelar
        private void Cancel() => MudDialog.Cancel();

    }
}
