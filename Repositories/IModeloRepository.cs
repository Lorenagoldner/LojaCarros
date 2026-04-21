using LojaCarros.Models;

namespace LojaCarros.Repositories
{
    public interface IModeloRepository
    {
        IEnumerable<Modelo> ListarTodos();
        List<Modelo> ListarPorMarca(int marcaId);
        void Adicionar(Modelo modelo); //metodo void para adicionar um modelo, usei void porque não precisa retornar nada, apenas adicionar o modelo ao banco de dados
        void Atualizar(Modelo modelo); //metodo void para atualizar um modelo, usei void porque não precisa retornar nada, apenas atualizar o modelo no banco de dados
        void Deletar(int id); //metodo void para excluir um modelo, usei void porque não precisa retornar nada, apenas excluir o modelo do banco de dados
    }
}
