using ElectronicHealthRecord.Domain.Enums;

namespace ElectronicHealthRecord.Application.DTOs.Atendimento
{
    public record CriarAtendimentoRequest(
        Guid PacienteId,
        Guid ProfissionalId,
        DateTime DataHora,
        StatusAtendimento Status
    );
}
