using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using UsuariosApp.API.Contexts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddRouting(map => map.LowercaseQueryStrings = true);
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Inje��o de depend�ncia para o DataContext (Entity Framework)
builder.Services.AddDbContext<DataContext>(
    options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("UsuariosApp"))
    );

var app = builder.Build();


app.MapOpenApi();
app.UseSwagger();
app.UseSwaggerUI();
app.MapScalarApiReference(s => s.WithTheme(ScalarTheme.BluePlanet)); //Scalar

app.UseAuthorization();

app.MapControllers();

app.Run();
