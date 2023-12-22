using Microsoft.AspNetCore.Components;
using WebMedicina.FrontEnd.Service;
using WebMedicina.FrontEnd.ServiceDependencies;
using WebMedicina.Shared.Dto;
namespace WebMedicina.FrontEnd.WebApp.Pages.Pacientes.CompFormCrearPac {
    public partial class EnfermedadRara { 
        [CascadingParameter(Name = "excepcionPersonalizada")] ExcepcionPersonalizada excepcionPersonalizada { get; set; }

        // Parametros
        [Parameter] public bool EnfermedadRaraCheck { get; set; }
        [Parameter] public EventCallback<bool> EnfermedadRaraCheckChanged { get; set; }

        [Parameter] public string Descripcion { get; set; }
        [Parameter] public EventCallback<string> DescripcionChanged { get; set; }

        [Parameter] public bool MostrarDescripcion { get; set; } = false;
    }
}
