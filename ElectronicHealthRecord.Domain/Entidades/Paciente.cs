namespace ElectronicHealthRecord.Domain.Entidades;

public class Paciente
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; } = string.Empty;
    public string Cpf { get; private set; } = string.Empty;
    public DateOnly DataNascimento { get; private set; }
    public string Telefone { get; private set; } = string.Empty;

    protected Paciente() { }

    public Paciente(string nome, string cpf, DateOnly dataNascimento, string telefone)
    {
        Id = Guid.NewGuid();
        Nome = nome;
        Cpf = cpf;
        DataNascimento = dataNascimento;
        Telefone = telefone;
    }

    public void AtualizarTelefone(string novoTelefone)
    {
        Telefone = novoTelefone;
    }
}
