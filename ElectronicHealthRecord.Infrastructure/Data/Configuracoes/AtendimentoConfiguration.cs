using ElectronicHealthRecord.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElectronicHealthRecord.Infrastructure.Data.Configuracoes
{
    public class AtendimentoConfiguration : IEntityTypeConfiguration<Atendimento>
    {
        public void Configure(EntityTypeBuilder<Atendimento> builder)
        {
            builder.ToTable("Atendimentos");

            builder.HasKey(a => a.Id);

            builder.HasOne(a => a.Paciente)
                .WithMany()
                .HasForeignKey(a => a.PacienteId);

            builder.HasOne(a => a.Profissional)
                .WithMany()
                .HasForeignKey(a => a.ProfissionalId);

            builder.Property(a => a.DataHora)
                .IsRequired();
        }
    }
}
