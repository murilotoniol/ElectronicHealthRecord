using ElectronicHealthRecord.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElectronicHealthRecord.Infrastructure.Data.Configuracoes
{
    public class ProfissionalConfiguration : IEntityTypeConfiguration<Profissional>
    {
        public void Configure(EntityTypeBuilder<Profissional> builder)
        {
            builder.ToTable("Profissionais");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(p => p.RegistroCrm)
                .IsRequired()
                .HasMaxLength(6);

            builder.Property(p => p.Especialidade)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
