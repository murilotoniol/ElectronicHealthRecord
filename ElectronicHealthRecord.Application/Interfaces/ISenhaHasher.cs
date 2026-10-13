namespace ElectronicHealthRecord.Application.Interfaces
{
    public interface ISenhaHasher
    {
        string GerarHash(string senha);
        bool Verificar(string senha, string hash);
    }
}
