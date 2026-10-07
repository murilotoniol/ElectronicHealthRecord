using ElectronicHealthRecord.Domain.Entidades;

namespace ElectronicHealthRecord.Application.Interfaces
{
    public interface IRegistroClinicoRepository
    {
        Task<IEnumerable<RegistroClinico>> ObterTodosAsync();
        Task<RegistroClinico?> ObterPorIdAsync(Guid id);
        Task CriarAsync(RegistroClinico registroClinico);
    }
}
