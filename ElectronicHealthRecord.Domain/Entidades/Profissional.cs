namespace ElectronicHealthRecord.Domain.Entidades;

public class Profissional
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; } = string.Empty;
    public string RegistroCrm { get; private set; } = string.Empty;
    public string Especialidade { get; private set; } = string.Empty;

    protected Profissional() { }

    public Profissional(string nome, string registroCrm, string especialidade)
    {
        Id = Guid.NewGuid();
        Nome = nome;
        RegistroCrm = registroCrm;
        Especialidade = especialidade;
    }
}
