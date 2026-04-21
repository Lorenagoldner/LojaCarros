namespace LojaCarros.DTOs
{
    public class VeiculoCreateDTO
    {
        public string Placa { get; set; }
        public int ModeloID { get; set; }
        public int Ano { get; set; }
        public DateTime UltimaInspecao { get; set; }
        public bool Vendido { get; set; }
    }
}
