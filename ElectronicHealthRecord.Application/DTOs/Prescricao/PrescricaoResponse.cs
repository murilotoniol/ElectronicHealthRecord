namespace ElectronicHealthRecord.Application.DTOs.Prescricao
{
    public record PrescricaoResponse(
        Guid Id,
        Guid RegistroClinicoId,
        string Medicamento,
        string Dosagem,
        string Instrucoes
    );
}
