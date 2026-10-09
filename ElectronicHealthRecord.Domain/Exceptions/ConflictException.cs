namespace ElectronicHealthRecord.Domain.Exceptions;

/// <summary>
/// A operação conflita com o estado atual dos dados, como duplicidade de CPF/CRM,
/// horário já ocupado ou registro já existente (HTTP 409).
/// </summary>
public class ConflictException : DomainException
{
    public ConflictException(string message) : base(message) { }
}
