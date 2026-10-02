using ElectronicHealthRecord.Domain.Entidades;
using Microsoft.EntityFrameworkCore;

namespace ElectronicHealthRecord.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Atendimento> Atendimentos => Set<Atendimento>();
        public DbSet<Paciente> Pacientes => Set<Paciente>();
        public DbSet<Prescricao> Prescricoes => Set<Prescricao>();
        public DbSet<Profissional> Profissionais => Set<Profissional>();
        public DbSet<RegistroClinico> RegistrosClinicos => Set<RegistroClinico>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
