using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebMedicina.BackEnd.API;
using WebMedicina.BackEnd.Dal;
using WebMedicina.BackEnd.Model;
using WebMedicina.BackEnd.Service;
using WebMedicina.BackEnd.ServicesDependencies;
using WebMedicina.Shared.Dto;
using WebMedicina.Shared.Service;

var builder = WebApplication.CreateBuilder(args);

// Obtenemos el entorno de ejecucion para configurar al appsettings que debemos usar
var entornoEjecucion = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
builder.Configuration
	.AddJsonFile($"appsettings.json", optional: false, reloadOnChange: true)
	.AddJsonFile($"appsettings.{entornoEjecucion}.json", optional: true, reloadOnChange: true);


// Add services to the controladores
builder.Services.AddControllers(options =>
{
	// Configuramos que los valores no nulleables se consideren requeridos en los dto
	options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true; 
}
);

// para encriptar
builder.Services.AddDataProtection();

// Para swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// conexion para BBDD
builder.Services.Configure<DbConnectionSettings>(builder.Configuration.GetSection("database"));
string connectionString = DBSettings.DBConnectionString(builder.Configuration);
builder.Services.AddDbContext<IdentityContext>(options =>
	   options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
builder.Services.AddDbContext<WebmedicinaContext>(options =>
	   options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// IDENTITY
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => {
	// no requerir cuenta confirmada
	options.SignIn.RequireConfirmedAccount = false;
	// Ajustamos la cantidad de intentos fallidos para el bloqueo de 1 dia
	options.Lockout.MaxFailedAccessAttempts = 5; // 5 intentos fallidos para bloqueo 
	options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(1);


	// Persinalizacion politicas para contraseñas
	options.Password.RequiredLength = 8;
	options.Password.RequireNonAlphanumeric = true;
	options.Password.RequireLowercase = true;
	options.Password.RequireUppercase = true;
	options.Password.RequireDigit = true;

})
	.AddEntityFrameworkStores<IdentityContext>() // usar entityframework core para trabajar con la BBDD
.AddDefaultTokenProviders();  // para los tokens de inicio de sesion


// JWT TOKENS
builder.Services.AddAuthentication(x => {
	x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
	x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
	x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
})
                   .AddJwtBearer(options =>
        options.TokenValidationParameters = new TokenValidationParameters {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["JWT:key"])),
        });

builder.Services.AddAuthorization();
//Annadimos servicio mapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


// Activamos CORS para permitir llamadas a la api desde otras url
builder.Services.AddCors(option => {
	option.AddPolicy("MyPolitica", app => {
		app.WithOrigins(builder.Configuration["AppSettings:BaseUrl"])
		.AllowAnyHeader() 
		.AllowAnyMethod();
	});
});


//DEPENDENCIAS
builder.Services.AddSingleton<ExcepcionPersonalizada>(); // excepciones
builder.Services.AddSingleton<IEncriptador, Encriptador>(); // encriptador

// DAL - BASE DE DATOS
builder.Services.AddScoped<AdminDal>(); // Dal de administradores
builder.Services.AddScoped<MedicoDal>(); // Dal de medicos
builder.Services.AddScoped<PacientesDal>(); // Dal de pacientes
builder.Services.AddScoped<EpilepsiasDal>(); // Dal de epilepsias
builder.Services.AddScoped<FarmacosDal>(); // Dal de farmacos
builder.Services.AddScoped<MutacionesDal>(); // Dal de mutaciones

// SERVICES
builder.Services.AddScoped<IIdentityService, IdentityService>(); // Servicios que trabajan con identity
builder.Services.AddScoped<IAdminsService, AdminsService>(); // Servicios de administradores
builder.Services.AddScoped<IPacientesService, PacientesService>(); // Servicios de pacientes



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseDeveloperExceptionPage();
	app.UseSwagger();
	app.UseSwaggerUI();
} 

// Permitimos llamadas http
app.UseHttpsRedirection();
app.MapControllers();

// Usamos nuestra politica para cors
app.UseCors("MyPolitica");


// Usamos autentificacion y autorizacion
app.UseAuthentication();
app.UseAuthorization();

// Acceder a las imagenes de forma estatica
app.UseStaticFiles(new StaticFileOptions {
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Imagenes")),
    RequestPath = "/img" // La URL desde la que se servirán las imágenes
});
app.Run();
