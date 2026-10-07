namespace ElectronicHealthRecord.Application.DTOs.Prescricao
{
    public record CriarPrescricaoRequest(
        string Medicamento,
        string Dosagem,
        string Instrucoes
    );
}
