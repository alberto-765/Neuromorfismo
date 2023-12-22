using Microsoft.AspNetCore.Components;
using WebMedicina.FrontEnd.Service;
using WebMedicina.FrontEnd.ServiceDependencies;
using WebMedicina.Shared.Dto;
namespace WebMedicina.FrontEnd.WebApp.Pages.Pacientes.CompFormCrearPac {
    public partial class Farmaco {
        [CascadingParameter(Name = "excepcionPersonalizada")] ExcepcionPersonalizada excepcionPersonalizada { get; set; }
        [Inject] private IPacientesService _pacientesService { get; set; }


        // Parametros
        [Parameter] public string FarmacoSel { get; set; }

        // Callback para devolver el valor actualizado
        [Parameter] public EventCallback<string> FarmacoSelChanged { get; set; }
    }
}
