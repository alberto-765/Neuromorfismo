using Microsoft.AspNetCore.Components;
using WebMedicina.FrontEnd.Service;
using WebMedicina.Shared.Dto;

namespace WebMedicina.FrontEnd.WebApp.Pages.Pacientes.CompFormCrearPac {
    public partial class Talla <T> where T : BasePaciente {
        [Parameter] public T Paciente { get; set; } // Parametros
        [Parameter] public EventCallback<T> PacienteChanged { get; set; } // Callback para devolver el valor actualizado
        [Parameter] public string Label { get; set; } = "Talla*";
    }
}
