using ElectronicHealthRecord.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElectronicHealthRecord.Infrastructure.Data.Configuracoes
{
    public class RegistroClinicoConfiguration : IEntityTypeConfiguration<RegistroClinico>
    {
        public void Configure(EntityTypeBuilder<RegistroClinico> builder)
        {
            builder.ToTable("RegistrosClinicos");

            builder.HasKey(r => r.Id);

            // 1:1
            builder.HasOne(r => r.Atendimento)
                .WithOne()
                .HasForeignKey<RegistroClinico>(r => r.AtendimentoId);

            builder.Property(r => r.Queixa)
                .HasMaxLength(200);

            builder.Property(r => r.Diagnostico)
                .HasMaxLength(200);

            builder.Property(r => r.Observacoes)
                .HasMaxLength(200);

            builder.Property(r => r.CriadoEm)
                .IsRequired();

            // 1:n
            builder.HasMany(r => r.Prescricoes)
                .WithOne()
                .HasForeignKey(p => p.RegistroClinicoId);
        }
    }
}
