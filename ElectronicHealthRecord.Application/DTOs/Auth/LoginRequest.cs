namespace ElectronicHealthRecord.Application.DTOs.Auth
{
    public record LoginRequest(
        string Email,
        string Senha
    );
}
