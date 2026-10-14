using ElectronicHealthRecord.Domain.Entidades;
using FluentAssertions;

namespace ElectronicHealthRecord.Tests.Domain;

public class RegistroClinicoTests
{
    [Fact]
    public void Construtor_AoInstanciar_DevePreencherCamposEAssociarPrescricoes()
    {
        var atendimentoId = Guid.NewGuid();
        var prescricoes = new List<Prescricao>
        {
            new("Dipirona 500mg", "1 comprimido a cada 6h", "Tomar se dor"),
            new("Amoxicilina 500mg", "1 cápsula a cada 8h por 7 dias", "Uso contínuo")
        };

        var registro = new RegistroClinico(
            atendimentoId,
            "Cefaleia e febre",
            "Sinusite aguda",
            "Paciente orientado a repousar",
            prescricoes
        );

        registro.Id.Should().NotBeEmpty();
        registro.AtendimentoId.Should().Be(atendimentoId);
        registro.Queixa.Should().Be("Cefaleia e febre");
        registro.Diagnostico.Should().Be("Sinusite aguda");
        registro.CriadoEm.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(2));
        registro.Prescricoes.Should().HaveCount(2);

        registro.Prescricoes.Should().OnlyContain(p => p.RegistroClinicoId == registro.Id);
    }

    [Fact]
    public void Construtor_SemPrescricoes_DeveInicializarListaVazia()
    {
        var registro = new RegistroClinico(
            Guid.NewGuid(),
            "Dor no joelho",
            "Entorse leve",
            "Gelo no local"
        );

        registro.Prescricoes.Should().BeEmpty();
    }
}