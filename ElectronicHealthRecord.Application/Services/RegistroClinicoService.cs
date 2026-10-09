using ElectronicHealthRecord.Application.DTOs.Comum;
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
        private readonly IPacienteRepository _pacienteRepository;

        public RegistroClinicoService(
            IRegistroClinicoRepository registroClinicoRepository,
            IAtendimentoRepository atendimentoRepository,
            IPacienteRepository pacienteRepository)
        {
            _registroClinicoRepository = registroClinicoRepository;
            _atendimentoRepository = atendimentoRepository;
            _pacienteRepository = pacienteRepository;
        }

        public async Task<RegistroClinicoResponse> ObterPorIdAsync(Guid id)
        {
            var registroClinico = await _registroClinicoRepository.ObterPorIdAsync(id);
            if (registroClinico == null)
            {
                throw new KeyNotFoundException($"Não foi encontrado um Registro Clínico com o id {id}");
            }

            return MapearParaResponse(registroClinico);
        }

        public async Task<IEnumerable<RegistroClinicoResponse>> ObterTodosAsync()
        {
            var registrosClinicos = await _registroClinicoRepository.ObterTodosAsync();
            return registrosClinicos.Select(MapearParaResponse);
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

            var registroExistente = await _registroClinicoRepository.ObterPorAtendimentoIdAsync(request.AtendimentoId);
            if (registroExistente != null)
            {
                throw new InvalidOperationException("Este atendimento já possui um registro clínico cadastrado.");
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

            return MapearParaResponse(registroClinico);
        }

        public async Task<PaginacaoResponse<RegistroClinicoResponse>> ObterHistoricoPorPacienteAsync(
            Guid pacienteId, PaginacaoRequest paginacao)
        {
            var paciente = await _pacienteRepository.ObterPorIdAsync(pacienteId);
            if (paciente == null)
            {
                throw new KeyNotFoundException($"Paciente com ID {pacienteId} não encontrado.");
            }

            var pagina = paginacao.Pagina < 1 ? 1 : paginacao.Pagina;
            var tamanhoPagina = paginacao.TamanhoPagina < 1 ? 10 : paginacao.TamanhoPagina;

            var (itens, totalItens) = await _registroClinicoRepository.ObterHistoricoPorPacienteAsync(
                pacienteId, pagina, tamanhoPagina);

            var itensResponse = itens.Select(MapearParaResponse);

            var totalPaginas = totalItens == 0 ? 0 : (int)Math.Ceiling((double)totalItens / tamanhoPagina);

            return new PaginacaoResponse<RegistroClinicoResponse>(
                itensResponse,
                totalItens,
                pagina,
                totalPaginas
            );
        }

        private static RegistroClinicoResponse MapearParaResponse(RegistroClinico registro)
        {
            return new RegistroClinicoResponse(
                registro.Id,
                registro.AtendimentoId,
                registro.Queixa,
                registro.Diagnostico,
                registro.Observacoes,
                registro.CriadoEm,
                registro.Prescricoes.Select(p => new PrescricaoResponse(
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
