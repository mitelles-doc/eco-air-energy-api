using EcoAir.EnergyApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EcoAir.EnergyApi.Data
{
    public class EcoAirContext : DbContext
    {
        // Chamado pela API em runtime
        public EcoAirContext(DbContextOptions<EcoAirContext> options) : base(options)
        {
        }

        // Chamado pelo EF Core em design-time (migrations)
        public EcoAirContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Server=(localdb)\\MSSQLLocalDB;Database=EcoAirEnergyDb;Trusted_Connection=True;MultipleActiveResultSets=True");
            }
        }

        public DbSet<UnidadeConsumidora> Unidades { get; set; }
        public DbSet<LeituraEnergia> Leituras { get; set; }
        public DbSet<LimiteConsumo> Limites { get; set; }
        public DbSet<AlertaConsumo> Alertas { get; set; }
    }
}
