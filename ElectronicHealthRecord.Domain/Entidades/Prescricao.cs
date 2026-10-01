namespace ElectronicHealthRecord.Domain.Entidades;

public class Prescricao
{
    public Guid Id { get; private set; }
    public Guid RegistroClinicoId { get; private set; }
    public string Medicamento { get; private set; } = string.Empty;
    public string Dosagem { get; private set; } = string.Empty;
    public string Instrucoes { get; private set; } = string.Empty;

    protected Prescricao() { }

    public Prescricao(string medicamento, string dosagem, string instrucoes)
    {
        Id = Guid.NewGuid();
        Medicamento = medicamento;
        Dosagem = dosagem;
        Instrucoes = instrucoes;
    }

    internal void AssociarAoRegistroClinico(Guid registroClinicoId)
    {
        RegistroClinicoId = registroClinicoId;
    }
}
