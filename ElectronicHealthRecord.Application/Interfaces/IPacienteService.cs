using ElectronicHealthRecord.Application.DTOs.Pacientes;

namespace ElectronicHealthRecord.Application.Interfaces
{
    public interface IPacienteService
    {
        Task<PacienteResponse> CriarAsync(CriarPacienteRequest request);
        Task<PacienteResponse> ObterPorIdAsync(Guid id);
        Task<IEnumerable<PacienteResponse>> ObterTodosAsync();
    }
}
