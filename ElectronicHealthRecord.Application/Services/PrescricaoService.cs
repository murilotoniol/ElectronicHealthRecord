using ElectronicHealthRecord.Application.DTOs.Prescricao;
using ElectronicHealthRecord.Application.Interfaces;
using ElectronicHealthRecord.Domain.Entidades;

namespace ElectronicHealthRecord.Application.Services
{
    public class PrescricaoService : IPrescricaoService
    {
        private readonly IPrescricaoRepository _prescricaoRepository;

        public PrescricaoService(IPrescricaoRepository prescricaoRepository)
        {
            _prescricaoRepository = prescricaoRepository;
        }

        public async Task<PrescricaoResponse> ObterPorIdAsync(Guid id)
        {
            var prescricao = await _prescricaoRepository.ObterPorIdAsync(id);

            if (prescricao == null)
            {
                throw new KeyNotFoundException("Instrução não encontrada");
            }

            return new PrescricaoResponse(
                prescricao.Id,
                prescricao.RegistroClinicoId,
                prescricao.Medicamento,
                prescricao.Dosagem,
                prescricao.Instrucoes
            );
        }

        public async Task<IEnumerable<PrescricaoResponse>> ObterTodosAsync()
        {
            var prescricoes = await _prescricaoRepository.ObterTodosAsync();

            return prescricoes.Select(p => new PrescricaoResponse(
                p.Id,
                p.RegistroClinicoId,
                p.Medicamento,
                p.Dosagem,
                p.Instrucoes
            ));
        }

        public async Task<PrescricaoResponse> CriarAsync(CriarPrescricaoRequest request)
        {
            var prescricao = new Prescricao(
                request.Medicamento,
                request.Dosagem,
                request.Instrucoes
            );

            await _prescricaoRepository.CriarAsync(prescricao);

            return new PrescricaoResponse(
                prescricao.Id,
                prescricao.RegistroClinicoId,
                prescricao.Medicamento,
                prescricao.Dosagem,
                prescricao.Instrucoes
            );
        }
    }
}
