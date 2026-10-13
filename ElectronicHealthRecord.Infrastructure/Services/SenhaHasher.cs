using ElectronicHealthRecord.Application.Interfaces;

namespace ElectronicHealthRecord.Infrastructure.Services
{
    public class SenhaHasher : ISenhaHasher
    {
        public string GerarHash(string senha) => BCrypt.Net.BCrypt.HashPassword(senha);

        public bool Verificar(string senha, string hash) => BCrypt.Net.BCrypt.Verify(senha, hash);
    }
}
