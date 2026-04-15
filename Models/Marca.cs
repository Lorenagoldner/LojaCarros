using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace LojaCarros.Models
{
    public class Marca
    {
        [Key]
        public int MarcaID { get; set; }
        public string NomeMarca { get; set; }
        //public ICollection<Modelo> Modelos { get; set; }
    }
}
