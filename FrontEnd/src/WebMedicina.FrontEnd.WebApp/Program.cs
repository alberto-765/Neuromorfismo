using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WebMedicina.FrontEnd.Service;
using WebMedicina.FrontEnd.ServiceDependencies;
using WebMedicina.FrontEnd.WebApp;
using MudBlazor.Services;
using WebMedicina.FrontEnd.Dto;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


// CONFIGURACION HTTCLIENT
builder.Services.AddHttpClient("HttpAPI", client => {
    client.BaseAddress = new Uri(builder.Configuration.GetSection("ApiSettings")["BaseUrl"] ?? throw new InvalidOperationException("No se ha podido obtener la url de la api."));

}).AddHttpMessageHandler<CreateHttpHandler>(); // asignacion de permisos en el header


//DEPENDENCIAS
builder.Services.AddSingleton<IConfigurationBuilder>(builder.Configuration); // para la configuracion
builder.Services.AddSingleton<EstilosBase>(); // Base de estilos mudblazor
builder.Services.AddScoped<ICrearHttpClient, CrearHttpClient>(); // para crear Httpclient
builder.Services.AddScoped<CreateHttpHandler>(); // Service para asingar a cada httpclient creado con httclientfactory el header con permisos
builder.Services.AddScoped<IRedirigirManager, RedirigirManager>(); // Redirigir 
builder.Services.AddScoped<IAdminsService, AdminsService>(); // Service de admins
builder.Services.AddScoped<IPerfilService, PerfilService>(); // Service para control del perfil
builder.Services.AddScoped<IPacientesService, PacientesService>(); // Service para pacientes
builder.Services.AddScoped<IComun, Comun>(); // Service para funciones comunes y reutilizables
builder.Services.AddScoped<ILineaTemporalService, LineaTemporalService>(); // Service para linea temporal
builder.Services.AddScoped<IDocumentacionService, DocumentacionService>(); // Service para descarga del excel e envio del email al avanzar en evolucion
builder.Services.AddScoped<IEstadisticasService, EstadisticasService>(); // Service para las estadisticas del inicio de la app

// Configuracion imagenes
builder.Services.Configure<ImagenesServerDto>(options => builder.Configuration.GetSection("ImagenesServer").Bind(options));


// Dependencias autenticacion
builder.Services.AddScoped<JWTAuthenticationProvider>();
builder.Services.AddScoped<AuthenticationStateProvider, JWTAuthenticationProvider>(provider => provider.GetRequiredService<JWTAuthenticationProvider>());
builder.Services.AddScoped<ILoginService, JWTAuthenticationProvider>(provider => provider.GetRequiredService<JWTAuthenticationProvider>());
builder.Services.AddAuthorizationCore();

// MudBlazor
builder.Services.AddMudServices();


var app = builder.Build();

await app.RunAsync();
