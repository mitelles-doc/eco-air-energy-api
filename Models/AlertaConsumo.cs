namespace EcoAir.EnergyApi.Models
{
    public class AlertaConsumo
    {
        public int Id { get; set; }
        public int UnidadeConsumidoraId { get; set; }
        public int LeituraEnergiaId { get; set; }
        public string Tipo { get; set; } = string.Empty;      // LIMITE_DIARIO_ULTRAPASSADO etc.
        public string Mensagem { get; set; } = string.Empty;
        public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
        public bool Resolvido { get; set; } = false;

        public UnidadeConsumidora Unidade { get; set; } = null!;
        public LeituraEnergia Leitura { get; set; } = null!;
    }
}
