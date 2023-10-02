using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WebMedicina.FrontEnd.WebApp;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//DEPENDENCIAS
builder.Services.AddHttpClient("HttpAPI", client => {
	client.BaseAddress = new Uri(builder.Configuration[$"ApiSettings:{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}Url"]);
});
builder.Services.AddSingleton<IConfiguration>(builder.Configuration); // para la configuracion



var app = builder.Build();

await app.RunAsync();
