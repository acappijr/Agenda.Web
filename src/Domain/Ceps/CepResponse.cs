using System.Text.Json.Serialization;

namespace Agenda.Domain.Ceps;

public class CepResponse
{
    [JsonPropertyName("status")]
    public int Status { get; set; }

    [JsonPropertyName("code")]
    public string Codigo { get; set; } = string.Empty;

    [JsonPropertyName("state")]
    public string Estado { get; set; } = string.Empty;

    [JsonPropertyName("city")]
    public string Cidade { get; set; } = string.Empty;

    [JsonPropertyName("district")]
    public string Bairro { get; set; } = string.Empty;

    [JsonPropertyName("address")]
    public string Endereco { get; set; } = string.Empty;

    public Cep ObterCep()
    {
        var cep = new Cep
        {
            Bairro = Bairro,
            Endereco = Endereco,
            Cidade = Cidade,
            Estado = Estado,
            Codigo = Codigo,
        };

        return cep;
    }
}
