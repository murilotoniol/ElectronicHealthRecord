using ElectronicHealthRecord.Application.DTOs.Atendimento;
using ElectronicHealthRecord.Application.Interfaces;
using ElectronicHealthRecord.Domain.Entidades;

namespace ElectronicHealthRecord.Application.Services
{
    public class AtendimentoService : IAtendimentoService
    {
        private readonly IAtendimentoRepository _atendimentoRepository;

        public AtendimentoService(IAtendimentoRepository atendimentoRepository)
        {
            _atendimentoRepository = atendimentoRepository;
        }

        public async Task<AtendimentoResponse> CriarAsync(CriarAtendimentoRequest request)
        {
            var atendimento = new Atendimento(
                request.PacienteId,
                request.ProfissionalId,
                request.DataHora
            );

            await _atendimentoRepository.AdicionarAsync(atendimento);

            return new AtendimentoResponse(
                atendimento.Id,
                atendimento.PacienteId,
                atendimento.ProfissionalId,
                atendimento.DataHora,
                atendimento.Status
            );
        }

        public async Task<AtendimentoResponse> ObterPorIdAsync(Guid id)
        {
            var atendimento = await _atendimentoRepository.ObterPorIdAsync(id);

            return new AtendimentoResponse(
                atendimento.Id,
                atendimento.PacienteId,
                atendimento.ProfissionalId,
                atendimento.DataHora,
                atendimento.Status
            );
        }

        public async Task<IEnumerable<AtendimentoResponse>> ObterTodosAsync()
        {
                        var atendimentos = await _atendimentoRepository.ObterTodosAsync();

            return atendimentos.Select(a => new AtendimentoResponse(
                a.Id,
                a.PacienteId,
                a.ProfissionalId,
                a.DataHora,
                a.Status
            ));
        }
    }
}
