using ElectronicHealthRecord.Application.DTOs.Atendimento;

namespace ElectronicHealthRecord.Application.Interfaces
{
    public interface IAtendimentoService
    {
        Task<AtendimentoResponse> CriarAsync(CriarAtendimentoRequest request);
        Task<AtendimentoResponse> ObterPorIdAsync(Guid id);
        Task<IEnumerable<AtendimentoResponse>> ObterTodosAsync();
        Task<AtendimentoResponse> RealizarAsync(Guid id);
        Task<AtendimentoResponse> CancelarAsync(Guid id);
    }
}
