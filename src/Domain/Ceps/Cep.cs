namespace Agenda.Domain.Ceps;
public class Cep
{
    public Cep()
    {
    }

    public Cep(Guid id, string codigo, string estado, string cidade, string? bairro, string? endereco)
    {
        Id = id;
        Codigo = codigo;
        Estado = estado;
        Cidade = cidade;
        Bairro = bairro;
        Endereco = endereco;
    }

    public Guid Id { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public string Estado { get; set; } = string.Empty;
    public string Cidade { get; set; } = string.Empty;
    public string? Bairro { get; set; }
    public string? Endereco { get; set; }
}
