using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LojaCarros.Models

{
    public class Modelo
    {
       
        [Key]
        public int ModeloID { get; set; }
        public string NomeModelo { get; set; }
        public int MarcaID { get; set; }
        public Marca Marca { get; set; }
        //public ICollection<Carro> Carros { get; set; } //ICollection é uma interface para ter uma coleção de modelos, tbm poderia usar List<Modelo> que é mais simples, mas o ICollection é padrão pra API

    }
}
