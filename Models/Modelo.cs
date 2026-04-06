using System.ComponentModel.DataAnnotations.Schema;

namespace LojaCarros.Models

{
    public class Modelo
    {
        public int ModeloID { get; set; }
        [Column("Modelo")] // mapeia com o nome da coluna no banco
        public string Nome { get; set; }
        public int MarcaID { get; set; }
        public Marca Marca { get; set; }
        //public ICollection<Carro> Carros { get; set; } //ICollection é uma interface para ter uma coleção de modelos, tbm poderia usar List<Modelo> que é mais simples, mas o ICollection é padrão pra API

    }
}
