using System.Reflection;

namespace LojaCarros.Models
{
    public class Marca
    {
        public int MarcaID { get; set; }
        public string NomeMarca { get; set; }
        public ICollection<Modelo> Modelos { get; set; }
        public object Id { get; internal set; }
    }
}
