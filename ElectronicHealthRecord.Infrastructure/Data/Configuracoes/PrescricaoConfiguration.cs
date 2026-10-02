using ElectronicHealthRecord.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElectronicHealthRecord.Infrastructure.Data.Configuracoes
{
    public class PrescricaoConfiguration : IEntityTypeConfiguration<Prescricao>
    {
        public void Configure(EntityTypeBuilder<Prescricao> builder)
        {
            builder.ToTable("Prescricoes");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.RegistroClinicoId)
                .IsRequired();

            builder.Property(p => p.Medicamento)
                .HasMaxLength(200);

            builder.Property(p => p.Dosagem)
                .HasMaxLength(100);

            builder.Property(p => p.Instrucoes)
                .HasMaxLength(500);
        }
    }
}
