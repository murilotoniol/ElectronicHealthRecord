namespace ElectronicHealthRecord.Application.DTOs.Pacientes
{
    public record PacienteResponse(
        Guid Id,
        string Nome,
        string Cpf,
        DateOnly DataNascimento,
        string Telefone
    );
}
