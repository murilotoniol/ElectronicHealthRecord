using ElectronicHealthRecord.Domain.Entidades;

namespace ElectronicHealthRecord.Application.Interfaces
{
    public interface IAtendimentoRepository
    {
        Task<Atendimento?> ObterPorIdAsync(Guid id);
        Task<IEnumerable<Atendimento>> ObterTodosAsync();
        Task CriarAsync(Atendimento atendimento);
        Task<bool> ExisteConflitoHorarioAsync(Guid profissionalId, DateTime dataHora);
        Task AtualizarAsync(Atendimento atendimento);
    }
}
