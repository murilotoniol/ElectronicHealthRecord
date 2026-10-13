namespace ElectronicHealthRecord.Application.DTOs.Auth
{
    public record LoginResponse(
        string Token,
        string Nome,
        string Email,
        string Role
    );
}
