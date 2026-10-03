namespace ElectronicHealthRecord.Application.DTOs.Pacientes
{
    public record CriarPacienteRequest(
        string Nome,
        string Cpf,
        DateOnly DataNascimento,
        string Telefone
        );
}
