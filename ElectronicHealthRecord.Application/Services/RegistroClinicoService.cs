using ElectronicHealthRecord.Application.DTOs.RegistroClinico;
using ElectronicHealthRecord.Application.Interfaces;
using ElectronicHealthRecord.Domain.Entidades;

namespace ElectronicHealthRecord.Application.Services
{
    public class RegistroClinicoService : IRegistroClinicoService
    {
        private readonly IRegistroClinicoRepository _registroClinicoRepository;
        private readonly IAtendimentoRepository _atendimentoRepository;

        public RegistroClinicoService(IRegistroClinicoRepository registroClinicoRepository, IAtendimentoRepository atendimentoRepository)
        {
            _registroClinicoRepository = registroClinicoRepository;
            _atendimentoRepository = atendimentoRepository;
        }

        public async Task<RegistroClinicoResponse> ObterPorIdAsync(Guid id)
        {
            var registroClinico = await _registroClinicoRepository.ObterPorIdAsync(id);
            if (registroClinico == null)
            {
                throw new KeyNotFoundException($"Não foi encontrado um Registro Clinico com o id {id}");
            }

            return new RegistroClinicoResponse(
                registroClinico.Id,
                registroClinico.AtendimentoId,
                registroClinico.Queixa,
                registroClinico.Diagnostico,
                registroClinico.Observacoes,
                registroClinico.CriadoEm
            );
        }

        public async Task<IEnumerable<RegistroClinicoResponse>> ObterTodosAsync()
        {
            var registrosClinicos = await _registroClinicoRepository.ObterTodosAsync();

            return registrosClinicos.Select(p => new RegistroClinicoResponse(
                p.Id,
                p.AtendimentoId,
                p.Queixa,
                p.Diagnostico,
                p.Observacoes,
                p.CriadoEm
            ));
        }

        public async Task<RegistroClinicoResponse> CriarAsync(CriarRegistroClinicoRequest request)
        {
            var atendimento = await _atendimentoRepository.ObterPorIdAsync(request.AtendimentoId);
            if (atendimento == null)
            {
                throw new KeyNotFoundException($"Atendimento com ID {request.AtendimentoId} não encontrado.");
            }

            if (atendimento.Status != Domain.Enums.StatusAtendimento.Realizado)
            {
                throw new InvalidOperationException("Um registro clínico só pode ser criado para atendimentos com status 'Realizado'.");
            }

            var registroClinico = new RegistroClinico(
                request.AtendimentoId,
                request.Queixa,
                request.Diagnostico,
                request.Observacoes
                //adicionar prescricoes no DTO request e aqui
            );

            await _registroClinicoRepository.CriarAsync(registroClinico);

            return new RegistroClinicoResponse(
                registroClinico.Id,
                registroClinico.AtendimentoId,
                registroClinico.Queixa,
                registroClinico.Diagnostico,
                registroClinico.Observacoes,
                registroClinico.CriadoEm
            );
        }
    }
}
