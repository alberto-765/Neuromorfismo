using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.ComponentModel.DataAnnotations;
using WebMedicina.FrontEnd.Service;
using WebMedicina.FrontEnd.ServiceDependencies;
using WebMedicina.Shared.Dto.Pacientes;

namespace WebMedicina.FrontEnd.WebApp.Pages.Pacientes.CompFormCrearPac
{
    public partial class NumHistoria <T> where T : BasePaciente {
        [Inject] private IPacientesService _pacientesService { get; set; } = null!;
        [Inject] private ISnackbar _snackbar { get; set; } = null!;


        // Parametros
        [Parameter] public T Paciente { get; set; } = null!;

        // Callback para devolver el valor actualizado
        [Parameter] public EventCallback<T> PacienteChanged { get; set; }

        [Parameter] public bool PacienteValido { get; set; } = true;
        [Parameter] public EventCallback<bool>PacienteValidoChanged { get; set; }

        // LLamada a backend para validar el numero de historia del paciente
        private async Task ValidarNumHistoria() {
            // Validamos que el campo del Numero Historia cumpla las validaciones del dto
            CrearPacienteDto nuevoPaciente = new();
            var validationErrors = new List<ValidationResult>();
            PacienteValido = Validator.TryValidateProperty(Paciente.NumHistoria,
                                            new ValidationContext(nuevoPaciente) { MemberName = nameof(nuevoPaciente.NumHistoria) },
                                            validationErrors);
            if (PacienteValido) {
                if (await _pacientesService.ValidarNumHistoria(Paciente.NumHistoria)) {
                    _snackbar.Add("Ya existe un paciente con el Número de Historia insertado.", Severity.Warning, config => { config.VisibleStateDuration = 2000; }) ;
                    PacienteValido = false;
                } else {
                    _snackbar.Add("Número de Historia válido", Severity.Success, config => { config.VisibleStateDuration = 2000; });
                    PacienteValido = true;
                }
            }
            await PacienteValidoChanged.InvokeAsync(PacienteValido);
        }
    }
}
