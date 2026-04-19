namespace EcoAir.EnergyApi.Models
{
    public class UnidadeConsumidora
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty; // Escritório, Fábrica etc.
        public bool Ativa { get; set; } = true;

        public ICollection<LeituraEnergia> Leituras { get; set; } = new List<LeituraEnergia>();
        public ICollection<LimiteConsumo> Limites { get; set; } = new List<LimiteConsumo>();
        public ICollection<AlertaConsumo> Alertas { get; set; } = new List<AlertaConsumo>();
    }
}
