namespace Agenda.Domain.Ceps;

public class Cep
{
    public Guid Id { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public string Estado { get; set; } = string.Empty;
    public string Cidade { get; set; } = string.Empty;
    public string? Bairro { get; set; }
    public string? Endereco { get; set; }
}
