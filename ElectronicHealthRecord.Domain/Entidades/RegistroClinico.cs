namespace ElectronicHealthRecord.Domain.Entidades;

public class RegistroClinico
{
    public Guid Id { get; private set; }
    public Guid AtendimentoId { get; private set; }
    public string Queixa { get; private set; } = string.Empty;
    public string Diagnostico { get; private set; } = string.Empty;
    public string Observacoes { get; private set; } = string.Empty;
    public DateTime CriadoEm { get; private set; }

    public Atendimento? Atendimento { get; private set; }

    private readonly List<Prescricao> _prescricoes = new();
    public IReadOnlyCollection<Prescricao> Prescricoes => _prescricoes.AsReadOnly();

    protected RegistroClinico() { }

    public RegistroClinico(
        Guid atendimentoId,
        string queixa,
        string diagnostico,
        string observacoes,
        IEnumerable<Prescricao>? prescricoes = null)
    {
        Id = Guid.NewGuid();
        AtendimentoId = atendimentoId;
        Queixa = queixa;
        Diagnostico = diagnostico;
        Observacoes = observacoes;
        CriadoEm = DateTime.UtcNow;

        if (prescricoes != null)
        {
            foreach (var prescricao in prescricoes)
            {
                prescricao.AssociarAoRegistroClinico(Id);
                _prescricoes.Add(prescricao);
            }
        }
    }
}
