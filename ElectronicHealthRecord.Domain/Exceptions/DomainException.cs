namespace ElectronicHealthRecord.Domain.Exceptions;

/// <summary>
/// Classe base para exceções esperadas da aplicação (regras de negócio, recursos
/// inexistentes, conflitos). Somente exceções que herdam desta classe têm a mensagem
/// exposta ao cliente; qualquer outra exceção é tratada como erro interno (500).
/// </summary>
public abstract class DomainException : Exception
{
    protected DomainException(string message) : base(message) { }
}
