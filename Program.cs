using LojaCarros.Models;
using LojaCarros.Repositories;

var builder = WebApplication.CreateBuilder(args);

// 1. Configurações de Serviços
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IMarcaRepository, MarcaRepository>();

var app = builder.Build();

// 2. Configurações do Pipeline (Swagger)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();


}

// 3. Tuas Rotas (Minimal API)
app.MapGet("/marcas", (IMarcaRepository repo) => Results.Ok(repo.ListarTodas()));

app.MapPost("/marcas", (Marca marca, IMarcaRepository repo) => {
    repo.Adicionar(marca);
    return Results.Created($"/marcas", marca);
});

app.Run();