using ElectronicHealthRecord.Domain.Entidades;

namespace ElectronicHealthRecord.Application.Interfaces
{
    public interface IPacienteRepository
    {
        Task<Paciente?> ObterPorIdAsync(Guid id);
        Task<Paciente?> ObterPorCpfAsync(string cpf);
        Task<IEnumerable<Paciente>> ObterTodosAsync();
        Task AdicionarAsync(Paciente paciente);
    }
}
