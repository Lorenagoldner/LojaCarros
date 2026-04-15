using Microsoft.Data.SqlClient;
using LojaCarros.Models; 
using System.Data;
using Biblioteca.ADONet;

namespace LojaCarros.Repositories
{
    public class MarcaRepository : IMarcaRepository
    {

        // O construtor recebe a configuração do banco que definimos no appsettings. Ele quem busca as informações para depois o IMarca agir.


        public IEnumerable<Marca> ListarTodas()
        {
            return DALPro.Query<Marca>("SELECT MarcaID, NomeMarca FROM Marcas");
        }

        public void Adicionar(Marca marca)
        {
            var sql = "INSERT INTO Marcas (NomeMarca) VALUES (@nome); SELECT SCOPE_IDENTITY();";
            var parametros = new Dictionary<string, object> { 
                { "@nome", marca.NomeMarca } 
            };

            var idGerado = DALPro.ExecuteScalar(sql, parametros);
            marca.MarcaID = Convert.ToInt32(idGerado);
            
        }

        public void Atualizar(Marca marca)
        {
            var sql = "UPDATE Marcas SET NomeMarca = @nome WHERE MarcaID = @id";
            var parametros = new Dictionary<string, object>
            {
                { "@nome", marca.NomeMarca },
                { "@id", marca.MarcaID }
            };
            DALPro.Execute(sql, parametros);
        }

        public void Deletar(int id)
        {
            var sql = "DELETE FROM Marcas WHERE MarcaID = @id";
            var parametros = new Dictionary<string, object>
            {
                { "@id", id }
            };
            DALPro.Execute(sql, parametros);
        }
    }
}