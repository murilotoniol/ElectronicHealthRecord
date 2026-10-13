using ElectronicHealthRecord.Domain.Entidades;

namespace ElectronicHealthRecord.Application.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> ObterPorEmailAsync(string email);
        Task CriarAsync(Usuario usuario);
    }
}
