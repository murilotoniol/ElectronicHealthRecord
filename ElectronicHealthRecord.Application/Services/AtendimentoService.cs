using ElectronicHealthRecord.Application.DTOs.Atendimento;
using ElectronicHealthRecord.Application.Interfaces;
using ElectronicHealthRecord.Domain.Exceptions;
using ElectronicHealthRecord.Domain.Entidades;

namespace ElectronicHealthRecord.Application.Services
{
    public class AtendimentoService : IAtendimentoService
    {
        private readonly IAtendimentoRepository _atendimentoRepository;
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IProfissionalRepository _profissionalRepository;

        public AtendimentoService(IAtendimentoRepository atendimentoRepository, IPacienteRepository pacienteRepository, IProfissionalRepository profissionalRepository)
        {
            _atendimentoRepository = atendimentoRepository;
            _pacienteRepository = pacienteRepository;
            _profissionalRepository = profissionalRepository;
        }

        public async Task<AtendimentoResponse> ObterPorIdAsync(Guid id)
        {
            var atendimento = await _atendimentoRepository.ObterPorIdAsync(id);

            if (atendimento == null)
            {
                throw new NotFoundException($"Atendimento com ID {id} não encontrado.");
            }

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

        public async Task<AtendimentoResponse> CriarAsync(CriarAtendimentoRequest request)
        {
            var paciente = await _pacienteRepository.ObterPorIdAsync(request.PacienteId);

            if (paciente == null)
            {
                throw new NotFoundException($"Paciente com ID {request.PacienteId} não encontrado.");
            }

            var profissional = await _profissionalRepository.ObterPorIdAsync(request.ProfissionalId);

            if (profissional == null)
            {
                throw new NotFoundException($"Profissional com ID {request.ProfissionalId} não encontrado.");
            }

            var atendimentoExistente = await _atendimentoRepository.ExisteConflitoHorarioAsync(request.ProfissionalId, request.DataHora);

            if (atendimentoExistente)
            {
                throw new ConflictException("Já existe um atendimento marcado para este profissional no mesmo horário.");
            }

            var atendimento = new Atendimento(
                request.PacienteId,
                request.ProfissionalId,
                request.DataHora
            );

            await _atendimentoRepository.CriarAsync(atendimento);

            return new AtendimentoResponse(
                atendimento.Id,
                atendimento.PacienteId,
                atendimento.ProfissionalId,
                atendimento.DataHora,
                atendimento.Status
            );
        }

        public async Task<AtendimentoResponse> RealizarAsync(Guid id)
        {
            var atendimento = await _atendimentoRepository.ObterPorIdAsync(id);

            if (atendimento == null)
            {
                throw new NotFoundException($"Atendimento com ID {id} não encontrado.");
            }

            atendimento.Realizar();
            
            await _atendimentoRepository.AtualizarAsync(atendimento);
            
            return new AtendimentoResponse(
                atendimento.Id,
                atendimento.PacienteId,
                atendimento.ProfissionalId,
                atendimento.DataHora,
                atendimento.Status
            );
        }

        public async Task<AtendimentoResponse> CancelarAsync(Guid id)
        {
            var atendimento = await _atendimentoRepository.ObterPorIdAsync(id);

            if (atendimento == null)
            {
                throw new NotFoundException($"Atendimento com ID {id} não encontrado.");
            }

            atendimento.Cancelar();

            await _atendimentoRepository.AtualizarAsync(atendimento);

            return new AtendimentoResponse(
                atendimento.Id,
                atendimento.PacienteId,
                atendimento.ProfissionalId,
                atendimento.DataHora,
                atendimento.Status
            );
        }
    }
}
