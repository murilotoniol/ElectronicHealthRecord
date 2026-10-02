using ElectronicHealthRecord.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElectronicHealthRecord.Infrastructure.Data.Configuracoes;

public class PacienteConfiguration : IEntityTypeConfiguration<Paciente>
{
    public void Configure(EntityTypeBuilder<Paciente> builder)
    {
        builder.ToTable("Pacientes");

        builder.HasKey(propa => propa.Id);

        builder.Property(propa => propa.Nome)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(propa => propa.Cpf)
            .IsRequired()
            .HasMaxLength(14);

        builder.HasIndex(propa => propa.Cpf)
            .IsUnique();

        builder.Property(propa => propa.Telefone)
            .HasMaxLength(20);
    }
}
