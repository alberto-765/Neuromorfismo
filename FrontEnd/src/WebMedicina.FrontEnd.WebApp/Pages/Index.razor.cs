using Microsoft.AspNetCore.Components;
using WebMedicina.FrontEnd.ServiceDependencies;
using WebMedicina.Shared.Dto.Estadisticas;

namespace WebMedicina.FrontEnd.WebApp.Pages;
public partial class Index {
    //DEPENDENCIAS
    [Inject] private IEstadisticasService _estadisticasService { get; set; } = null!;


    // Estadísticas, si es null es porque no han sido obtenidas
    private EstadisticasDto? estadisticas { get; set; }

    /// <summary>
    /// Cargamos los charts con toda la info
    /// </summary>
    /// <returns></returns>
    protected override async Task OnInitializedAsync() {
        await base.OnInitializedAsync();
        await Task.Delay(10000);
        //estadisticas = await _estadisticasService.ObtenerEstadisitcas();
    }
}
