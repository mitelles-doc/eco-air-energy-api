namespace EcoAir.EnergyApi.Models
{
    public class LimiteConsumo
    {
        public int Id { get; set; }
        public int UnidadeConsumidoraId { get; set; }
        public decimal? LimiteDiarioKwh { get; set; }
        public decimal? LimiteMensalKwh { get; set; }
        public bool Ativo { get; set; } = true;

        public UnidadeConsumidora Unidade { get; set; } = null!;
    }
}
