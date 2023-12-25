using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.ComponentModel.DataAnnotations;
using WebMedicina.FrontEnd.Service;
using WebMedicina.FrontEnd.ServiceDependencies;
using WebMedicina.Shared.Dto;

namespace WebMedicina.FrontEnd.WebApp.Pages.Pacientes.CompFormCrearPac {
    public partial class NumHistoria <T> where T : BasePaciente {
        [CascadingParameter(Name = "excepcionPersonalizada")] ExcepcionPersonalizada excepcionPersonalizada { get; set; }
        [Inject] private IPacientesService _pacientesService { get; set; }
        [Inject] private ISnackbar _snackbar { get; set; }


        // Parametros
        [Parameter] public T Paciente { get; set; }

        // Callback para devolver el valor actualizado
        [Parameter] public EventCallback<T> PacienteChanged { get; set; }

        [Parameter] public bool PacienteValido { get; set; } = true;
        [Parameter] public EventCallback<bool>PacienteValidoChanged { get; set; }

        // LLamada a backend para validar el numero de historia del paciente
        private async Task ValidarNumHistoria() {
            try {
                // Validamos que el campo del Numero Historia cumpla las validaciones del dto
                CrearPacienteDto nuevoPaciente = new();
                var validationErrors = new List<ValidationResult>();
                PacienteValido = Validator.TryValidateProperty(Paciente.NumHistoria,
                                                new ValidationContext(nuevoPaciente) { MemberName = nameof(nuevoPaciente.NumHistoria) },
                                                validationErrors);
                if (PacienteValido) {
                    if (await _pacientesService.ValidarNumHistoria(Paciente.NumHistoria)) {
                        _snackbar.Add("Ya existe un paciente con el Número de Historia insertado.", Severity.Warning);
                        PacienteValido = false;
                    } else {
                        _snackbar.Add("Número de Historia válido", Severity.Success);
                        PacienteValido = true;
                    }
                }
                await PacienteValidoChanged.InvokeAsync(PacienteValido);
            } catch (Exception ex) {
                excepcionPersonalizada.ConstruirPintarExcepcion(ex);
                throw;
            }
        }
    }
}
