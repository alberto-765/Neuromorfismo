using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WebMedicina.FrontEnd.Service;
using WebMedicina.FrontEnd.ServiceDependencies;
using WebMedicina.FrontEnd.WebApp;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// CONFIGURACION APISETTINGSº

// CONFIGURACION HTTCLIENT
builder.Services.AddHttpClient("HttpAPI", client => {
    client.BaseAddress = new Uri(builder.Configuration["ApiSettings:BaseUrl"]);
});


//DEPENDENCIAS

builder.Services.AddSingleton<IConfigurationBuilder>(builder.Configuration); // para la configuracion
builder.Services.AddScoped<ICrearHttpClient, CrearHttpClient>(); // para crear Httpclient
builder.Services.AddScoped<ExcepcionPersonalizada>(); // excepciones
builder.Services.AddScoped<EstilosBase>(); // Base de estilos mudblazor
builder.Services.AddScoped<IRedirigirManager, RedirigirManager>(); // Redirigir 
builder.Services.AddScoped<IAdminsService, AdminsService>(); // Service de admins
builder.Services.AddScoped<IPerfilService, PerfilService>(); // Service para control del perfil
builder.Services.AddScoped<IPacientesService, PacientesService>(); // Service para pacientes

// Dependencias autenticacion
builder.Services.AddScoped<JWTAuthenticationProvider>();
builder.Services.AddScoped<AuthenticationStateProvider, JWTAuthenticationProvider>(provider => provider.GetRequiredService<JWTAuthenticationProvider>());
builder.Services.AddScoped<ILoginService, JWTAuthenticationProvider>(provider => provider.GetRequiredService<JWTAuthenticationProvider>()); 
builder.Services.AddAuthorizationCore();

// Mud blazor
builder.Services.AddMudServices();


var app = builder.Build();

await app.RunAsync();
