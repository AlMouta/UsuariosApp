using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using UsuariosApp.API.Contexts;
using UsuariosApp.API.Helpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddRouting(map => map.LowercaseQueryStrings = true);
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Injeção de dependência para o DataContext (Entity Framework)
builder.Services.AddDbContext<DataContext>(
    options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("UsuariosApp"))
    );

//Injeção de dependência para o JwtTokenHelper
builder.Services.AddSingleton<JwtTokenHelper>();

var app = builder.Build();


app.MapOpenApi();
app.UseSwagger();
app.UseSwaggerUI();
app.MapScalarApiReference(s => s.WithTheme(ScalarTheme.BluePlanet)); //Scalar

app.UseAuthorization();

app.MapControllers();

app.Run();
