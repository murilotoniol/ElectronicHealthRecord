using ElectronicHealthRecord.Domain.Enums;

namespace ElectronicHealthRecord.Application.DTOs.Atendimento
{
    public record AtendimentoResponse(
        Guid Id,
        Guid PacienteId,
        Guid ProfissionalId,
        DateTime DataHora,
        StatusAtendimento Status
    );
}
