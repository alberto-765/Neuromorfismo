using Microsoft.EntityFrameworkCore;
using WebMedicina.BackEnd.Data;
using WebMedicina.BackEnd.Model;

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllers();

// para encriptar
builder.Services.AddDataProtection();

// Para swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// conexion para BBDD
builder.Services.Configure<DbConnectionSettings>(configuration.GetSection("database"));
string connectionString = DBSettings.DBConnectionString(configuration);
builder.Services.AddDbContext<WebmedicinaContext>(options =>
	   options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

//Añadimos servicio mapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseDeveloperExceptionPage();
	app.UseSwagger();
	app.UseSwaggerUI();
} 

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
