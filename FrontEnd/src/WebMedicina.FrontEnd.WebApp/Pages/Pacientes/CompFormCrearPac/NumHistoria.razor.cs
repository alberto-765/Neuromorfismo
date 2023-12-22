using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.ComponentModel.DataAnnotations;
using WebMedicina.FrontEnd.Service;
using WebMedicina.FrontEnd.ServiceDependencies;
using WebMedicina.Shared.Dto;

namespace WebMedicina.FrontEnd.WebApp.Pages.Pacientes.CompFormCrearPac {
    public partial class NumHistoria {
        [CascadingParameter(Name = "excepcionPersonalizada")] ExcepcionPersonalizada excepcionPersonalizada { get; set; }
        [Inject] private IPacientesService _pacientesService { get; set; }
        [Inject] private ISnackbar _snackbar { get; set; }


        // Parametros
        [Parameter] public string NumHistoriaSel { get; set; }
        [Parameter] public bool NumHistoriaVal { get; set; }

        // Callback para devolver el valor actualizado
        [Parameter] public EventCallback<string> NumHistoriaSelChanged { get; set; }
        [Parameter] public EventCallback<bool> NumHistoriaValChanged { get; set; }


        // LLamada para validar el numero de historia
        private async Task ActualizarValor() {
            try {
                // Validamos numero historia si ha sido modificado
                NumHistoriaVal = await ValidarNumHistoria();
            } catch (Exception ex) {
                excepcionPersonalizada.ConstruirPintarExcepcion(ex);
                throw;
            }
        }


        // LLamada a backend para validar el numero de historia del paciente
        private async Task<bool> ValidarNumHistoria() {
            try {
                // Validamos que el campo del Numero Historia cumpla las validaciones del dto
                CrearPacienteDto nuevoPaciente = new();
                var validationErrors = new List<ValidationResult>();
                bool esValido = Validator.TryValidateProperty(NumHistoriaSel,
                                                new ValidationContext(nuevoPaciente) { MemberName = nameof(nuevoPaciente.NumHistoria) },
                                                validationErrors);
                if (esValido) {
                    if (await _pacientesService.ValidarNumHistoria(NumHistoriaSel)) {
                        _snackbar.Add("Ya existe un paciente con el Número de Historia insertado.", Severity.Warning);
                        return false;
                    } else {
                        _snackbar.Add("Número de Historia válido", Severity.Success);
                        return true;
                    }
                } else {
                    return false;
                }
            } catch (Exception ex) {
                excepcionPersonalizada.ConstruirPintarExcepcion(ex);
                throw;
            }
        }
    }
}
