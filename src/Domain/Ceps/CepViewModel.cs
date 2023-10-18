using System.ComponentModel.DataAnnotations;

namespace Agenda.Domain.Ceps;
public class CepViewModel
{

    public Guid Id { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(8)]
    public string Codigo { get; set; } = string.Empty;

    public string? Estado { get; set; }

    public string? Cidade { get; set; }

    public string? Bairro { get; set; }

    public string? Endereco { get; set; }

    public static CepViewModel MapearCepParaCepViewModel(Cep cep)
    {
        ArgumentNullException.ThrowIfNull(cep);

        var cepViewModel = new CepViewModel
        {
            Id = cep.Id,
            Codigo = cep.Codigo,
            Bairro = cep.Bairro,
            Cidade = cep.Cidade,
            Endereco = cep.Endereco,
            Estado = cep.Estado
        };

        return cepViewModel;
    }
}
