namespace ElectronicHealthRecord.Models
{
    public class Patient
    {
        public Guid Id { get; }
        public string Name { get; }
        public string Cpf { get; }
        public DateOnly Birthday { get; }
        public string Phone { get; set; }

        public Patient(string name, string cpf, DateOnly birthday, string phone)
        {
            Id = Guid.NewGuid();
            Name = name;
            Cpf = cpf;
            Birthday = birthday;
            Phone = phone;
        }
    }
}
