using ElectronicHealthRecord.Domain.Entidades;

namespace ElectronicHealthRecord.Application.Interfaces
{
    public interface IPrescricaoRepository
    {
        Task<Prescricao?> ObterPorIdAsync(Guid id);
        Task<IEnumerable<Prescricao>> ObterTodosAsync();
        Task CriarAsync(Prescricao prescricao);
    }
}
