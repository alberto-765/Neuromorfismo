using Microsoft.AspNetCore.Components;
using WebMedicina.FrontEnd.Service;
using WebMedicina.Shared.Dto;

namespace WebMedicina.FrontEnd.WebApp.Pages.Pacientes.CompFormCrearPac {
    public partial class Talla{
        [CascadingParameter(Name = "excepcionPersonalizada")] ExcepcionPersonalizada excepcionPersonalizada { get; set; }


        // Parametros
        [Parameter] public int TallaSel { get; set; }

        // Callback para devolver el valor actualizado
        [Parameter] public EventCallback<int> TallaSelChanged { get; set; }

    }
}
