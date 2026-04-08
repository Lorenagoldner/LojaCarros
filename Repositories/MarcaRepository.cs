using Microsoft.Data.SqlClient;
using LojaCarros.Models; 
using System.Data;

namespace LojaCarros.Repositories
{
    public class MarcaRepository : IMarcaRepository
    {
        private readonly string _connectionString; //usa readonly para garantir que a string de conexão não seja alterada após a inicialização do repositório.

        // O construtor recebe a configuração do banco que definimos no appsettings. Ele quem busca as informações para depois o IMarca agir.
        public MarcaRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IEnumerable<Marca> ListarTodas()
        {
            var marcas = new List<Marca>();

            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("SELECT MarcaID, NomeMarca FROM Marcas", conn);
                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        marcas.Add(new Marca
                        {
                            MarcaID = (int)reader["MarcaID"],
                            NomeMarca = reader["NomeMarca"].ToString()
                        });
                    }
                }
            }
            return marcas;
        }

        public void Adicionar(Marca marca)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("INSERT INTO Marcas (NomeMarca) VALUES (@nome); SELECT SCOPE_IDENTITY();", conn);
                conn.Open();
                marca.MarcaID = Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
    }
}