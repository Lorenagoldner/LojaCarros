namespace LojaCarros.Models
{
    public class Carro
    {
        public int CarroID { get; set; }
        public int Ano { get; set; }
        public string Placa { get; set; }
        public DateTime? UltimaInspecao { get; set; }
        public bool Vendido { get; set; }
        public int ModeloID { get; set; }
        public Modelo Modelo { get; set; }
    }
}
