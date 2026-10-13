using ElectronicHealthRecord.Application.DTOs.Auth;

namespace ElectronicHealthRecord.Application.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponse> LoginAsync(LoginRequest request);
    }
}
