namespace ElectronicHealthRecord.Domain.Exceptions;

/// <summary>
/// Violação de uma regra de negócio, como transição de status inválida (HTTP 422).
/// </summary>
public class BusinessRuleException : DomainException
{
    public BusinessRuleException(string message) : base(message) { }
}
