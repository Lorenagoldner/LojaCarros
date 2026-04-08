using LojaCarros.Models;

namespace LojaCarros.Repositories
{
    public interface IMarcaRepository
    {
        // Contrato: quem usar este repositório terá de implementar estes métodos, nesse caso de listar e adicionar
        IEnumerable<Marca> ListarTodas();
        void Adicionar(Marca marca);
    }
}