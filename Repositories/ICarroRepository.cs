using LojaCarros.Models;
using LojaCarros.DTOs;

namespace LojaCarros.Repositories
{
    public interface ICarroRepository
    {
        List<VeiculoViewDTO> ListarParaTabela();
        void Adicionar(VeiculoCreateDTO carroDto);
        void Atualizar(int id, VeiculoCreateDTO carroDto);
        void Deletar(int id);

        //Essas abaixo usavam os modelos diretamente, mas agora estamos usando DTOs para separar as camadas e evitar acoplamento.
        //IEnumerable<Carro> ListarTodos();
        //void Adicionar(Carro carro);
        //void Atualizar(Carro carro);
    }
}
