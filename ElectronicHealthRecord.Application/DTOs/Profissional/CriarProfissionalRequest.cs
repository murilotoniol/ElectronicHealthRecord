namespace ElectronicHealthRecord.Application.DTOs.Profissional
{
    public record CriarProfissionalRequest(
        string Nome,
        string RegistroCrm,
        string Especialidade
    );
}
