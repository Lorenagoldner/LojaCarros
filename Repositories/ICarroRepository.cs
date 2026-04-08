using LojaCarros.Models;

namespace LojaCarros.Repositories
{
    public interface ICarroRepository
    {
        IEnumerable<Carro> ListarTodos();
        void Adicionar(Carro carro);
    }
}
