using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using WebMedicina.FrontEnd.Service;
using WebMedicina.FrontEnd.ServiceDependencies;
using WebMedicina.FrontEnd.WebApp;
using WebMedicina.Shared.Dto;
using MudBlazor.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var apiSettingsSection = builder.Configuration.GetSection("ApiSettings");
String developmentUrl = String.Empty;
if (apiSettingsSection != null) {
   developmentUrl =  apiSettingsSection[$"{builder.HostEnvironment.Environment}Url"];
}

//DEPENDENCIAS
builder.Services.AddHttpClient("HttpAPI", client => {
    client.BaseAddress = new Uri(developmentUrl);
});
builder.Services.AddSingleton<IConfigurationBuilder>(builder.Configuration); // para la configuracion
builder.Services.AddSingleton<ICrearHttpClient, CrearHttpClient>(); // para crear Httpclient
builder.Services.AddSingleton<ExcepcionDto>(); // excepciones
builder.Services.AddSingleton<EstilosBase>(); // Base de estilos mudblazor
builder.Services.AddSingleton<IRedirigirManager, RedirigirManager>(); // Redirigir 
builder.Services.AddScoped<IAdminsService, AdminsService>(); // Service de admins

// Dependencias autenticacion
builder.Services.AddScoped<JWTAuthenticationProvider>();
builder.Services.AddScoped<AuthenticationStateProvider, JWTAuthenticationProvider>(provider => provider.GetRequiredService<JWTAuthenticationProvider>());
builder.Services.AddScoped<ILoginService, JWTAuthenticationProvider>(provider => provider.GetRequiredService<JWTAuthenticationProvider>()); 
builder.Services.AddAuthorizationCore();

// Mud blazor
builder.Services.AddMudServices();


var app = builder.Build();

await app.RunAsync();
