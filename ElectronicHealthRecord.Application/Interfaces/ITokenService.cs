using ElectronicHealthRecord.Application.DTOs.Auth;

namespace ElectronicHealthRecord.Application.Interfaces
{
    public interface ITokenService
    {
        string GerarToken(Guid id, string nome, string email, string role);
    }
}
