namespace ElectronicHealthRecord.Domain.Exceptions;

/// <summary>Recurso solicitado não existe (HTTP 404).</summary>
public class NotFoundException : DomainException
{
    public NotFoundException(string message) : base(message) { }
}
