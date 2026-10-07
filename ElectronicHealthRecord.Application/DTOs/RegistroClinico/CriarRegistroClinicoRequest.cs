using ElectronicHealthRecord.Application.DTOs.Prescricao;

namespace ElectronicHealthRecord.Application.DTOs.RegistroClinico
{
    public record CriarRegistroClinicoRequest(
        Guid AtendimentoId,
        string Queixa,
        string Diagnostico,
        string Observacoes,
        IEnumerable<CriarPrescricaoRequest>? Prescricoes
    );
}
