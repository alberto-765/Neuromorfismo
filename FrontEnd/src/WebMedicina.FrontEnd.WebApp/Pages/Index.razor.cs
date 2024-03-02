using Microsoft.AspNetCore.Components;
using WebMedicina.FrontEnd.ServiceDependencies;

namespace WebMedicina.FrontEnd.WebApp.Pages;
public partial class Index {
    //DEPENDENCIAS
    [Inject] private IEstadisticasService _estadisticasService { get; set; } = null!;


    /// <summary>
    /// Cargamos los charts con toda la info
    /// </summary>
    /// <returns></returns>
    protected override async Task OnInitializedAsync() {
        await _estadisticasService.ObtenerEstadisitcas();
    }
}
