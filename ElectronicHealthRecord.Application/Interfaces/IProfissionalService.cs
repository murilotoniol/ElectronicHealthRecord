using ElectronicHealthRecord.Application.DTOs.Profissional;

namespace ElectronicHealthRecord.Application.Interfaces
{
    public interface IProfissionalService
    {
        Task<ProfissionalResponse> CriarAsync(CriarProfissionalRequest request);
        Task<ProfissionalResponse> ObterPorIdAsync(Guid id);
        Task<IEnumerable<ProfissionalResponse>> ObterTodosAsync();
    }
}
