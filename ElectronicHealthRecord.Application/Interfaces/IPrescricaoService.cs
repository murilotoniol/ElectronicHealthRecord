using ElectronicHealthRecord.Application.DTOs.Prescricao;

namespace ElectronicHealthRecord.Application.Interfaces
{
    public interface IPrescricaoService
    {
        Task<PrescricaoResponse> ObterPorIdAsync(Guid id);
        Task<IEnumerable<PrescricaoResponse>> ObterTodosAsync();
        Task<PrescricaoResponse> CriarAsync(CriarPrescricaoRequest request);
    }
}
