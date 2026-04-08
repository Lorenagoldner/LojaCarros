using LojaCarros.Models;

namespace LojaCarros.Repositories
{
    public interface IModeloRepository
    {
        IEnumerable<Modelo> ListarTodos();
        void Adicionar(Modelo modelo);
    }
}
