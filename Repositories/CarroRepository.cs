using Biblioteca.ADONet;
using LojaCarros.DTOs;
using LojaCarros.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace LojaCarros.Repositories
{
    public class CarroRepository : ICarroRepository
    {

        //public IEnumerable<Carro> ListarTodos()
        //{
        //    return DALPro.Query<Carro>("SELECT CarroID, Ano, Placa, UltimaInspecao, vendido, ModeloID FROM Carros");
        //}

        public void Adicionar(VeiculoCreateDTO dto)
        {
            var sql = "INSERT INTO Carros (Ano, Placa, UltimaInspecao,vendido, ModeloID) VALUES (@ano, @placa, @ultimaInspecao, @vendido, @modeloID); SELECT SCOPE_IDENTITY()";
            var parametros = new Dictionary<string, object>
                {
                    { "@ano", dto.Ano},
                    { "@placa", dto.Placa},
                    { "@ultimaInspecao", dto.UltimaInspecao},
                    { "@vendido", dto.Vendido},
                    { "@modeloID", dto.ModeloID}

                };

            DALPro.Execute(sql, parametros);
        }

        public void Atualizar(int id, VeiculoCreateDTO dto)
        {
            var sql = "UPDATE Carros SET Ano = @ano, Placa = @placa, UltimaInspecao = @ultimaInspecao, vendido = @vendido, ModeloID = @modeloID WHERE CarroID = @carroId";
            var parametros = new Dictionary<string, object>
                {
                    { "@ano", dto.Ano},
                    { "@placa", dto.Placa},
                    { "@ultimaInspecao", dto.UltimaInspecao},
                    { "@vendido", dto.Vendido},
                    { "@modeloID", dto.ModeloID},
                    { "@carroId", id}
                };
            DALPro.Execute(sql, parametros);
        }

        public void Deletar(int id)
        {
            var sql = "DELETE FROM Carros WHERE CarroID = @carroId";
            var parametros = new Dictionary<string, object>
                {
                    { "@carroId", id }
                };
            DALPro.Execute(sql, parametros);
        }

        public List<VeiculoViewDTO> ListarParaTabela()
        {
            var sql = @"SELECT 
                    c.CarroID as Id, 
                    c.Placa
                    ma.NomeMarca as Marca, 
                    mo.NomeModelo as Modelo, 
                    c.Ano, 
                    c.UltimaInspecao, 
                    c.Vendido
                FROM Carros c
                INNER JOIN Modelos mo ON c.ModeloID = mo.ModeloID
                INNER JOIN Marcas ma ON mo.MarcaID = ma.MarcaID";

            return DALPro.Query<VeiculoViewDTO>(sql);
        }
    }
}




