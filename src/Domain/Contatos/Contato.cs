using Agenda.Domain.Ceps;

namespace Agenda.Domain.Contatos;

public class Contato
{
    public Contato()
    {
    }
    public Contato(int id, string nome, string? telefone, string? email, Guid cepId, string? numeroEndereco, string? complementoEndereco)
    {
        Id = id;
        Nome = nome;
        Telefone = telefone;
        Email = email;
        CepId = cepId;
        NumeroEndereco = numeroEndereco;
        ComplementoEndereco = complementoEndereco;
    }

    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? Telefone { get; set; }
    public string? Email { get; set; }
    public Guid CepId { get; set; }
    public Cep Cep { get; set; } = new();
    public string? NumeroEndereco { get; set; }
    public string? ComplementoEndereco { get; set; }
}
