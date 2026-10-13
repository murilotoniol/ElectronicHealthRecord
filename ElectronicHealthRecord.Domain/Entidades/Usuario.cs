using ElectronicHealthRecord.Domain.Enums;

namespace ElectronicHealthRecord.Domain.Entidades;

public class Usuario
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string SenhaHash { get; private set; } = string.Empty;
    public PerfilUsuario Perfil { get; private set; }

    protected Usuario() { }

    public Usuario(string nome, string email, string senhaHash, PerfilUsuario perfil)
    {
        Id = Guid.NewGuid();
        Nome = nome;
        Email = NormalizarEmail(email);
        SenhaHash = senhaHash;
        Perfil = perfil;
    }

    public static string NormalizarEmail(string email) => email.Trim().ToLowerInvariant();
}
