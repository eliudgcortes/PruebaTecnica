using AppEstFin.Repository;
using AppEstFin.Services;
using AppEstFin.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Registrar DataBaseHelper y Logger
builder.Services.AddSingleton<DataBaseHelper>(); // O AddScoped, dependiendo de cómo se use DataBaseHelper
builder.Services.AddLogging(); // Esto agrega los servicios de logging necesarios

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registrar dependencias
builder.Services.AddScoped<IEstimacionRepository, EstimacionRepository>();
builder.Services.AddScoped<IEstimacionService, EstimacionService>();

// Configurar CORS para permitir conexiones desde cualquier origen
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policyBuilder =>
        {
            policyBuilder.AllowAnyOrigin()
                         .AllowAnyMethod()
                         .AllowAnyHeader();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Usar la política de CORS
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

// Configurar la aplicación para escuchar en el puerto 85
//app.Urls.Add("http://*:85");

app.Run();