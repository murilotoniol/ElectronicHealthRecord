using ElectronicHealthRecord.Application.DTOs.Prescricao;
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
                registroClinico.CriadoEm,
                registroClinico.Prescricoes.Select(p => new PrescricaoResponse(
                    p.Id,
                    p.RegistroClinicoId,
                    p.Medicamento,
                    p.Dosagem,
                    p.Instrucoes
                ))
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
                p.CriadoEm,
                p.Prescricoes.Select(p => new PrescricaoResponse(
                    p.Id,
                    p.RegistroClinicoId,
                    p.Medicamento,
                    p.Dosagem,
                    p.Instrucoes
                ))
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

            var prescricoes = request.Prescricoes?.Select(p => new Prescricao(
                p.Medicamento,
                p.Dosagem,
                p.Instrucoes
            ));

            var registroClinico = new RegistroClinico(
                request.AtendimentoId,
                request.Queixa,
                request.Diagnostico,
                request.Observacoes,
                prescricoes
            );

            await _registroClinicoRepository.CriarAsync(registroClinico);

            return new RegistroClinicoResponse(
                registroClinico.Id,
                registroClinico.AtendimentoId,
                registroClinico.Queixa,
                registroClinico.Diagnostico,
                registroClinico.Observacoes,
                registroClinico.CriadoEm,
                registroClinico.Prescricoes.Select(p => new PrescricaoResponse(
                    p.Id,
                    p.RegistroClinicoId,
                    p.Medicamento,
                    p.Dosagem,
                    p.Instrucoes
                ))
            );
        }
    }
}
