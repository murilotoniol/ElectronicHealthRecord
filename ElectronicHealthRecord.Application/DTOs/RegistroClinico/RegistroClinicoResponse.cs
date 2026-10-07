namespace ElectronicHealthRecord.Application.DTOs.RegistroClinico
{
    public record RegistroClinicoResponse(
        Guid Id,
        Guid AtendimentoId,
        string Queixa,
        string Diagnostico,
        string Observacoes,
        DateTime CriadoEm
    );
}
