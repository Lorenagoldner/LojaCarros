using LojaCarros.Models;
using LojaCarros.Repositories;
using Microsoft.AspNetCore.Builder;
using LojaCarros.DTOs;

var builder = WebApplication.CreateBuilder(args);

//Cors
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin() // Permite que seu Front-end acesse
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


//Configurações de Serviços
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

Biblioteca.ADONet.DALPro.ConnectionString = builder.Configuration.GetConnectionString("LojaCarros");

builder.Services.AddScoped<IMarcaRepository, MarcaRepository>();
builder.Services.AddScoped<IModeloRepository, ModeloRepository>();
builder.Services.AddScoped<ICarroRepository, CarroRepository>();

var app = builder.Build();

app.UseCors();

//Swagger
if (app.Environment.IsDevelopment()) 
{ 
    app.UseSwagger();
    app.UseSwaggerUI();

}

//Rotas Marcas
app.MapGet("/marcas", (IMarcaRepository repo) =>
{
    var marcas = repo.ListarTodas();
    return Results.Ok(marcas);
});

app.MapGet("/modelos/marca/{id}", (int id, IModeloRepository repo) =>
{
    var modelos = repo.ListarPorMarca(id);
    return Results.Ok(modelos);
});

app.MapPost("/marcas", (Marca marca, IMarcaRepository repo) =>
{
    if (marca == null) return Results.BadRequest("Dados inválidos");

    repo.Adicionar(marca);

    // Retorna 201 Created e indica a URL de acesso (opcionalmente)
    return Results.Created($"/marcas/{marca.MarcaID}", marca);
});

app.MapPut("/marcas/{id}", (int id, Marca marca, IMarcaRepository repo) =>
{
    if (marca == null || marca.MarcaID != id) return Results.BadRequest("Dados inválidos");
    repo.Atualizar(marca);
    return Results.Ok(marca);
});

app.MapDelete("/marcas/{id}", (int id, IMarcaRepository repo) =>
{
    repo.Deletar(id);
    return Results.NoContent();
});



//Rotas Modelos
app.MapGet("/modelos", (IModeloRepository repo) =>
{
    return Results.Ok(repo.ListarTodos());
});

app.MapPost("/modelos", (Modelo modelo, IModeloRepository repo) =>
{
    repo.Adicionar(modelo);
    return Results.Created($"/modelos/{modelo.ModeloID}", modelo);
});

app.MapPut("/modelos/{id}", (int id, Modelo modelo, IModeloRepository repo) =>
{
    modelo.ModeloID = id; 
    repo.Atualizar(modelo);
    return Results.Ok(modelo);
});

app.MapDelete("/modelos/{id}", (int id, IModeloRepository repo) =>
{
    repo.Deletar(id);
    return Results.NoContent();
});

//Rotas Carros
app.MapGet("/carros", (ICarroRepository repo) =>
{
    return Results.Ok(repo.ListarParaTabela());
});

app.MapPost("/carros", (VeiculoCreateDTO carroDto, ICarroRepository repo) =>
{
    if (carroDto == null) return Results.BadRequest("Dados inválidos");

    repo.Adicionar(carroDto);
    return Results.Created("/carros", carroDto);
});

app.MapPut("/carros/{id}", (int id, VeiculoCreateDTO carroDto, ICarroRepository repo) =>
{
    if (carroDto == null) return Results.BadRequest("Dados inválidos");

    repo.Atualizar(id, carroDto);
    return Results.Ok("CArro atualizado com sucesso");
});

app.MapDelete("/carro/{id}", (int id, ICarroRepository repo) =>
{
    repo.Deletar(id);
    return Results.NoContent();
});
app.Run();