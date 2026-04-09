using Microsoft.Data.SqlClient;
using LojaCarros.Models;
using System.Data;
using Biblioteca.ADONet;

namespace LojaCarros.Repositories
{
    public class CarroRepository : ICarroRepository
    {
        
        public IEnumerable<Carro> ListarTodos()
        {
            return DALPro.Query<Carro>("SELECT CarroID, Ano, Placa, UltimaInspecao, vendido, ModeloID FROM Carros");
        }

        public void Adicionar(Carro carro)
        {
            var sql = "INSERT INTO Carros (Ano, Placa, UltimaInspecao,vendido, ModeloID) VALUES (@ano, @placa, @ultimaInspecao, @vendido, @modeloID); SELECT SCOPE_IDENTITY()";
            var parametros = new Dictionary<string, object>
                {
                    { "@ano", carro.Ano},
                    { "@placa", carro.Placa},
                    { "@ultimaInspecao", carro.UltimaInspecao},
                    { "@vendido", carro.Vendido},
                    { "@modeloID", carro.ModeloID}

                };

                var resultado = DALPro.ExecuteScalar(sql, parametros);
                carro.CarroID = Convert.ToInt32(resultado);
            }
        }
    }




