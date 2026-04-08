using Microsoft.Data.SqlClient;
using LojaCarros.Models;
using System.Data;

namespace LojaCarros.Repositories
{
    public class CarroRepository : ICarroRepository
    {
        private readonly string _connectionString;

        public CarroRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IEnumerable<Carro> ListarTodos()
        {
            var carros = new List<Carro>();

            using (var conn = new SqlConnection(_connectionString))
            {

                var sql = "SELECT CarroID, Ano, Placa, UltimaInspecao, vendido, ModeloID FROM Carros";
                var cmd = new SqlCommand(sql, conn);
                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        carros.Add(new Carro
                        {
                            CarroID = (int)reader["CarroID"],
                            Ano = (int)reader["Ano"],
                            Placa = reader["Placa"].ToString(),
                            UltimaInspecao = (DateTime)reader["UltimaInspecao"],
                            Vendido = (bool)reader["Vendido"],
                            ModeloID = (int)reader["ModeloID"]
                         });
               
                    }
                }
            }
            return carros;
        }

        public void Adicionar(Carro carro)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var sql = "INSERT INTO Carros (Ano, Placa, UltimaInspecao,vendido, ModeloID) VALUES (@ano, @placa, @ultimaInspecao, @vendido, @modeloID); SELECT SCOPE_IDENTITY();";
                var cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@ano", carro.Ano);
                cmd.Parameters.AddWithValue("@placa", carro.Placa);
                cmd.Parameters.AddWithValue("@ultimaInspecao", carro.UltimaInspecao);
                cmd.Parameters.AddWithValue("@vendido", carro.Vendido);
                cmd.Parameters.AddWithValue("@modeloID", carro.ModeloID);


                conn.Open();
                // Usando ExecuteScalar para atualizar o ID no objeto
                carro.CarroID = Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
    }


}

