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
}