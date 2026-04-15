using Microsoft.Data.SqlClient;
using LojaCarros.Models;
using System.Data;
using Biblioteca.ADONet;

namespace LojaCarros.Repositories;

public class ModeloRepository : IModeloRepository
{
    public IEnumerable<Modelo> ListarTodos()
    {
        return DALPro.Query<Modelo>("SELECT ModeloID, NomeModelo, MarcaID FROM Modelos");
    }

    public void Adicionar(Modelo modelo)
    {
        
            var sql = "INSERT INTO Modelos (NomeModelo, MarcaID) VALUES (@nome, @marcaId); SELECT SCOPE_IDENTITY();";
            var parametros = new Dictionary<string, object>
            {
                //em c# quando criamos um dicionário ele irá funcionar em pares, por isso precisa ser dessa forma que fiz.
                { "@nome",  modelo.NomeModelo },
                { "@marcaId",  modelo.MarcaID }
            };

            var resultado = DALPro.ExecuteScalar(sql,parametros); 
            modelo.ModeloID = Convert.ToInt32(resultado);
    }
    public void Atualizar(Modelo modelo)
    {
        var sql = "UPDATE Modelos SET NomeModelo = @nome, MarcaID = @marcaId WHERE ModeloID = @modeloId";
        var parametros = new Dictionary<string, object>
        {
            { "@nome", modelo.NomeModelo },
            { "@marcaId", modelo.MarcaID },
            { "@modeloId", modelo.ModeloID }
        };
        DALPro.Execute(sql, parametros);
    }

    public void Deletar(int id)
    {

        var sqlCheck = "SELECT COUNT(*) FROM Carros WHERE ModeloID = @id";
        var parametros = new Dictionary<string, object> { { "@id", id } };

        var quantidadeCarros = Convert.ToInt32(DALPro.ExecuteScalar(sqlCheck, parametros));

        if ( quantidadeCarros > 0) {             
            throw new InvalidOperationException("Não é possível deletar o modelo, existem carros associados a ele.");
        }

        var sqlDelete = "DELETE FROM Modelos WHERE ModeloID = @modeloId";
        DALPro.Execute(sqlDelete, parametros);
    }
    public List<Modelo> ListarPorMarca(int marcaId)
    {
        var sql = "SELECT * FROM Modelos WHERE MarcaID = @marcaId";
        var p = new Dictionary<string, object> { { "@marcaId", marcaId } };

        return DALPro.Query<Modelo>(sql, p);
    }
}