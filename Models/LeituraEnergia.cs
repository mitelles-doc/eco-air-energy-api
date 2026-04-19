namespace EcoAir.EnergyApi.Models
{
    public class LeituraEnergia
    {
        public int Id { get; set; }
        public int UnidadeConsumidoraId { get; set; }
        public DateTime DataHora { get; set; }
        public decimal ConsumoKwh { get; set; }
        public decimal CustoEstimado { get; set; }
        public string FonteEnergia { get; set; } = string.Empty; // Rede, Solar...

        public UnidadeConsumidora Unidade { get; set; } = null!;
    }
}
