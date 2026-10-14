using ElectronicHealthRecord.Domain.Entidades;
using ElectronicHealthRecord.Domain.Enums;
using ElectronicHealthRecord.Domain.Exceptions;
using FluentAssertions;
using Xunit;

namespace ElectronicHealthRecord.Tests.Domain;

public class AtendimentoTests
{
    [Fact]
    public void Construtor_AoCriarAtendimento_DeveInicializarComStatusAgendadoEIdValido()
    {
        var pacienteId = Guid.NewGuid();
        var profissionalId = Guid.NewGuid();
        var dataHora = DateTime.UtcNow.AddDays(1);

        var atendimento = new Atendimento(pacienteId, profissionalId, dataHora);

        atendimento.Id.Should().NotBeEmpty();
        atendimento.PacienteId.Should().Be(pacienteId);
        atendimento.ProfissionalId.Should().Be(profissionalId);
        atendimento.DataHora.Should().Be(dataHora);
        atendimento.Status.Should().Be(StatusAtendimento.Agendado);
    }

    [Fact]
    public void Realizar_QuandoStatusForAgendado_DeveMudarStatusParaRealizado()
    {
        var atendimento = new Atendimento(Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow);

        atendimento.Realizar();

        atendimento.Status.Should().Be(StatusAtendimento.Realizado);
    }

    [Fact]
    public void Realizar_QuandoStatusJaForRealizado_DeveLancarBusinessRuleException()
    {
        var atendimento = new Atendimento(Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow);
        atendimento.Realizar();

        Action acao = () => atendimento.Realizar();

        acao.Should().Throw<BusinessRuleException>()
            .WithMessage("*Somente atendimentos agendados podem ser realizados*");
    }

    [Fact]
    public void Realizar_QuandoStatusForCancelado_DeveLancarBusinessRuleException()
    {
        var atendimento = new Atendimento(Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow);
        atendimento.Cancelar();

        Action acao = () => atendimento.Realizar();

        acao.Should().Throw<BusinessRuleException>()
            .WithMessage("*Somente atendimentos agendados podem ser realizados*");
    }

    [Fact]
    public void Cancelar_QuandoStatusForAgendado_DeveMudarStatusParaCancelado()
    {
        var atendimento = new Atendimento(Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow);

        atendimento.Cancelar();

        atendimento.Status.Should().Be(StatusAtendimento.Cancelado);
    }

    [Fact]
    public void Cancelar_QuandoStatusJaForCancelado_DeveLancarBusinessRuleException()
    {
        var atendimento = new Atendimento(Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow);
        atendimento.Cancelar();

        Action acao = () => atendimento.Cancelar();

        acao.Should().Throw<BusinessRuleException>()
            .WithMessage("*Somente atendimentos agendados podem ser cancelados*");
    }
}