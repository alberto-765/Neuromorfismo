using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Collections.Immutable;
using WebMedicina.FrontEnd.ServiceDependencies;
using WebMedicina.Shared.Dto.Estadisticas;

namespace WebMedicina.FrontEnd.WebApp.Pages;
public partial class Index {
    //DEPENDENCIAS
    [Inject] private IEstadisticasService _estadisticasService { get; set; } = null!;

    // Estadísticas, si es null es porque no han sido obtenidas
    private EstadisticasDto? Estadisticas { get; set; }

    // Gráfica totales
    private ChartOptions OptionsTotal { get; set; } = new() { 
        YAxisLines = true, 
        XAxisLines = false,
        ChartPalette = new string[] { "#387ADF", "#FBA834" },
        InterpolationOption = InterpolationOption.NaturalSpline,
        LineStrokeWidth = 2
    };
    private ImmutableList<ChartSeries> DatosTotales = ImmutableList<ChartSeries>.Empty;

    /// <summary>
    /// Cargamos los charts con toda la info
    /// </summary>
    /// <returns></returns>
    protected override async Task OnInitializedAsync() {
        await base.OnInitializedAsync();
        await Task.Delay(10000);
        Estadisticas = await _estadisticasService.ObtenerEstadisitcas();
        CargarDatosTotales();
    }

    /// <summary>
    /// Cargar datos para la gráfica de totales
    /// </summary>
    private void CargarDatosTotales() {
        if(Estadisticas is not null) {
            DatosTotales = ImmutableList.Create(
                new ChartSeries() { Name = "Médicos", Data = Estadisticas.TotalMedicos },
                new ChartSeries() { Name = "Pacientes", Data = Estadisticas.TotalPacientes }
            );

            // Calculamos saltos para el eje Y
            double NumMaxTotal = Math.Max(Estadisticas.TotalMedicos.Max(), Estadisticas.TotalPacientes.Max());
            ushort SaltosY =  Convert.ToUInt16(NumMaxTotal / 11);
            if(SaltosY < 5) {
                SaltosY = 5;
            }

            OptionsTotal.YAxisTicks = SaltosY;
        }
    }
}
