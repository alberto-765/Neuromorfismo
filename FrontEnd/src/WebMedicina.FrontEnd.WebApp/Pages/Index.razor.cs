namespace WebMedicina.FrontEnd.WebApp.Pages;
public partial class Index {


    /// <summary>
    /// Cargamos los charts con toda la info
    /// </summary>
    /// <returns></returns>
    protected override async Task OnInitializedAsync() {
        await _redirigirManager.RedirigirDefault();
    }
}
