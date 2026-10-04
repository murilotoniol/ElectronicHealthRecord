namespace ElectronicHealthRecord.Application.DTOs.Profissional
{
    public record ProfissionalResponse(
        Guid Id,
        string Nome,
        string RegistroCrm,
        string Especialidade
    );
}
