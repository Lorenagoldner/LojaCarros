using LojaCarros.Models;
using LojaCarros.Repositories;

var builder = WebApplication.CreateBuilder(args);

// 1. Configurações de Serviços
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Biblioteca.ADONet.DALPro.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddScoped<IMarcaRepository, MarcaRepository>();
builder.Services.AddScoped<IModeloRepository, ModeloRepository>();
builder.Services.AddScoped<ICarroRepository, CarroRepository>();

var app = builder.Build();



// 2. Configurações do Pipeline (Swagger)
if (app.Environment.IsDevelopment()) // Apenas em desenvolvimento, para não expor a documentação em produção
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

// 3. Tuas Rotas (Minimal API)
app.MapGet("/marcas", (IMarcaRepository repo) =>
{
    var marcas = repo.ListarTodas();
    return Results.Ok(marcas);
});

app.MapPost("/marcas", (Marca marca, IMarcaRepository repo) =>
{
    if (marca == null) return Results.BadRequest("Dados inválidos");

    repo.Adicionar(marca);

    // Retorna 201 Created e indica a URL de acesso (opcionalmente)
    return Results.Created($"/marcas/{marca.Id}", marca);
});

app.MapGet("/modelos", (IModeloRepository repo) =>
{
    return Results.Ok(repo.ListarTodos());
});

app.MapPost("/modelos", (Modelo modelo, IModeloRepository repo) =>
{
    repo.Adicionar(modelo);
    return Results.Created($"/modelos/{modelo.ModeloID}", modelo);
});

// --- ROTAS DE CARROS ---
app.MapGet("/carros", (ICarroRepository repo) =>
{
    return Results.Ok(repo.ListarTodos());
});

app.MapPost("/carros", (Carro carro, ICarroRepository repo) =>
{
    repo.Adicionar(carro);
    return Results.Created($"/carros/{carro.CarroID}", carro);
});

app.Run();