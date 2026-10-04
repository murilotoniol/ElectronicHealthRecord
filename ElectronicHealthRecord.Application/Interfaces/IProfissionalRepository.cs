using ElectronicHealthRecord.Domain.Entidades;

namespace ElectronicHealthRecord.Application.Interfaces
{
    public interface IProfissionalRepository
    {
        Task<Profissional?> ObterPorIdAsync(Guid id);
        Task<Profissional?> ObterPorCrmAsync(string registroCrm);
        Task<IEnumerable<Profissional>> ObterTodosAsync();
        Task CriarAsync(Profissional profissional);
    }
}
