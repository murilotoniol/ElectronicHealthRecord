namespace ElectronicHealthRecord.Domain.Exceptions;

/// <summary>E-mail ou senha inválidos no login (HTTP 401).</summary>
public class CredenciaisInvalidasException : DomainException
{
    public CredenciaisInvalidasException() : base("E-mail ou senha inválidos.") { }
}
