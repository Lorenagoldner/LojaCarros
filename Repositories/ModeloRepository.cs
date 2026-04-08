using Microsoft.Data.SqlClient;
using LojaCarros.Models;
using System.Data;

namespace LojaCarros.Repositories;

public class ModeloRepository : IModeloRepository
{
    private readonly string _connectionString;

    public ModeloRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public IEnumerable<Modelo> ListarTodos()
    {
        var modelos = new List<Modelo>();

        using (var conn = new SqlConnection(_connectionString))
        {
           
            var sql = "SELECT ModeloID, NomeModelo, MarcaID FROM Modelos";
            var cmd = new SqlCommand(sql, conn);
            conn.Open();

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    modelos.Add(new Modelo
                    {
                        ModeloID = (int)reader["ModeloID"],
                        NomeModelo = reader["NomeModelo"].ToString(),
                        MarcaID = (int)reader["MarcaID"]
                    });
                }
            }
        }
        return modelos;
    }

    public void Adicionar(Modelo modelo)
    {
        using (var conn = new SqlConnection(_connectionString))
        {
            var sql = "INSERT INTO Modelos (NomeModelo, MarcaID) VALUES (@nome, @marcaId); SELECT SCOPE_IDENTITY();";
            var cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@nome", modelo.NomeModelo);
            cmd.Parameters.AddWithValue("@marcaId", modelo.MarcaID);

            conn.Open();
            // Usando ExecuteScalar para atualizar o ID no objeto
            modelo.ModeloID = Convert.ToInt32(cmd.ExecuteScalar());
        }
    }
}