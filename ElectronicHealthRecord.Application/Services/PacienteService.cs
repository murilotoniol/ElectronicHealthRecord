using ElectronicHealthRecord.Application.DTOs.Pacientes;
using ElectronicHealthRecord.Application.Interfaces;
using ElectronicHealthRecord.Domain.Entidades;

namespace ElectronicHealthRecord.Application.Services
{
    public class PacienteService : IPacienteService
    {
        private readonly IPacienteRepository _pacienteRepository;

        public PacienteService(IPacienteRepository pacienteRepository)
        {
            _pacienteRepository = pacienteRepository;
        }

        public async Task<PacienteResponse> CriarAsync(CriarPacienteRequest request)
        {
            var pacienteExistente = await _pacienteRepository.ObterPorCpfAsync(request.Cpf);
            if (pacienteExistente != null)
            {
                throw new InvalidOperationException("Já existe um paciente cadastrado com este CPF.");
            }

            var paciente = new Paciente(
                request.Nome,
                request.Cpf,
                request.DataNascimento,
                request.Telefone
                );

            await _pacienteRepository.AdicionarAsync(paciente);

            return new PacienteResponse(
                paciente.Id,
                paciente.Nome,
                paciente.Cpf,
                paciente.DataNascimento,
                paciente.Telefone
            );
        }

        public async Task<PacienteResponse> ObterPorIdAsync(Guid id)
        {
            var paciente = await _pacienteRepository.ObterPorIdAsync(id);
            if (paciente == null)
            {
                throw new KeyNotFoundException("Paciente não encontrado.");
            }

            return new PacienteResponse(
                paciente.Id,
                paciente.Nome,
                paciente.Cpf,
                paciente.DataNascimento,
                paciente.Telefone
            );
        }

        public async Task<IEnumerable<PacienteResponse>> ObterTodosAsync()
        {
            var pacientes = await _pacienteRepository.ObterTodosAsync();

            return pacientes.Select(p => new PacienteResponse(
                p.Id,
                p.Nome,
                p.Cpf,
                p.DataNascimento,
                p.Telefone
            ));
        }
    }
}
