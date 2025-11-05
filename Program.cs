using RestauranteAPI.Controllers;
using RestauranteAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IPlatoPrincipalRepository, PlatoPrincipalRepository>();
builder.Services.AddScoped<IComboRepository, ComboRepository>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

    app.UseSwagger();
    app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();



//PlatoPrincipalController.InicializarDatos();
app.Run();
