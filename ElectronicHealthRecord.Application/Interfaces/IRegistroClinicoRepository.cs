using ElectronicHealthRecord.Domain.Entidades;

namespace ElectronicHealthRecord.Application.Interfaces
{
    public interface IRegistroClinicoRepository
    {
        Task<IEnumerable<RegistroClinico>> ObterTodosAsync();
        Task<RegistroClinico?> ObterPorIdAsync(Guid id);
        Task<RegistroClinico?> ObterPorAtendimentoIdAsync(Guid atendimentoId);
        Task CriarAsync(RegistroClinico registroClinico);
        Task<(IEnumerable<RegistroClinico> Itens, int TotalItens)> ObterHistoricoPorPacienteAsync(Guid pacienteId, int pagina, int tamanhoPagina);
    }
}
