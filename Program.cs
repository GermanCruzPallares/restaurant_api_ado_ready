using RestauranteAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Registrar servicios y dependencias
builder.Services.AddScoped<IPlatoPrincipalRepository, PlatoPrincipalRepository>();
builder.Services.AddScoped<IComboRepository, ComboRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Construir la app
var app = builder.Build();

// Configurar middleware
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

// Iniciar la app
app.Run();
