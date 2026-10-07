using ElectronicHealthRecord.Application.DTOs.RegistroClinico;

namespace ElectronicHealthRecord.Application.Interfaces
{
    public interface IRegistroClinicoService
    {
        Task<RegistroClinicoResponse> ObterPorIdAsync(Guid id);
        Task<IEnumerable<RegistroClinicoResponse>> ObterTodosAsync();
        Task<RegistroClinicoResponse> CriarAsync(CriarRegistroClinicoRequest request);
    }
}
