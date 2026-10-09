using ElectronicHealthRecord.Domain.Exceptions;
using ElectronicHealthRecord.Domain.Enums;

namespace ElectronicHealthRecord.Domain.Entidades;

public class Atendimento
{
    public Guid Id { get; private set; }
    public Guid PacienteId { get; private set; }
    public Guid ProfissionalId { get; private set; }
    public DateTime DataHora { get; private set; }
    public StatusAtendimento Status { get; private set; }

    public Paciente? Paciente { get; private set; }
    public Profissional? Profissional { get; private set; }

    protected Atendimento() { }

    public Atendimento(Guid pacienteId, Guid profissionalId, DateTime dataHora)
    {
        Id = Guid.NewGuid();
        PacienteId = pacienteId;
        ProfissionalId = profissionalId;
        DataHora = dataHora;
        Status = StatusAtendimento.Agendado;
    }

    public void Realizar()
    {
        if (Status != StatusAtendimento.Agendado)
        {
            throw new BusinessRuleException($"Não é possível realizar um atendimento com status '{Status}'. Somente atendimentos agendados podem ser realizados.");
        }

        Status = StatusAtendimento.Realizado;
    }

    public void Cancelar()
    {
        if (Status != StatusAtendimento.Agendado)
        {
            throw new BusinessRuleException($"Não é possível cancelar um atendimento com status '{Status}'. Somente atendimentos agendados podem ser cancelados.");
        }

        Status = StatusAtendimento.Cancelado;
    }
}
