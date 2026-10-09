using ElectronicHealthRecord.Application.DTOs.Profissional;
using ElectronicHealthRecord.Application.Interfaces;
using ElectronicHealthRecord.Domain.Exceptions;
using ElectronicHealthRecord.Domain.Entidades;

namespace ElectronicHealthRecord.Application.Services
{
    public class ProfissionalService : IProfissionalService
    {
        private readonly IProfissionalRepository _profissionalRepository;

        public ProfissionalService(IProfissionalRepository profissionalRepository)
        {
            _profissionalRepository = profissionalRepository;
        }

        public async Task<ProfissionalResponse> CriarAsync(CriarProfissionalRequest request)
        {
            var profissionalExistente = await _profissionalRepository.ObterPorCrmAsync(request.RegistroCrm);
            if (profissionalExistente != null)
            {
                throw new ConflictException("Já existe um profissional com o mesmo registro CRM.");
            }

            var profissional = new Profissional(
                request.Nome,
                request.RegistroCrm,
                request.Especialidade
            );

            await _profissionalRepository.CriarAsync(profissional);

            return new ProfissionalResponse(
                profissional.Id,
                profissional.Nome,
                profissional.RegistroCrm,
                profissional.Especialidade
            );
        }

        public async Task<ProfissionalResponse> ObterPorIdAsync(Guid id)
        {
            var profissional = await _profissionalRepository.ObterPorIdAsync(id);
            if (profissional == null)
            {
                throw new NotFoundException("Profissional não encontrado.");
            }

            return new ProfissionalResponse(
                profissional.Id,
                profissional.Nome,
                profissional.RegistroCrm,
                profissional.Especialidade
            );
        }

        public async Task<IEnumerable<ProfissionalResponse>> ObterTodosAsync()
        {
            var profissionais = await _profissionalRepository.ObterTodosAsync();
            return profissionais.Select(p => new ProfissionalResponse(
                p.Id,
                p.Nome,
                p.RegistroCrm,
                p.Especialidade
            ));
        }
    }
}
