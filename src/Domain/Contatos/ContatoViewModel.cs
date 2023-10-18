using Agenda.Domain.Ceps;
using System.ComponentModel.DataAnnotations;

namespace Agenda.Domain.Contatos;

public class ContatoViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public string Nome { get; set; } = string.Empty;

    [StringLength(11)]
    public string? Telefone { get; set; }

    [DataType(DataType.EmailAddress, ErrorMessage = "E-mail em formato inválido.")]
    public string? Email { get; set; }

    public CepViewModel Cep { get; set; } = new();

    public string? NumeroEndereco { get; set; }

    public string? ComplementoEndereco { get; set; }

    public (Contato contato, Cep cep) ObterEntidadeContatoCep()
    {
        var contato = new Contato(Id, Nome, Telefone, Email, Cep.Id, NumeroEndereco, ComplementoEndereco);
        var cep = new Cep(Cep.Id, Cep.Codigo, Cep.Estado!, Cep.Cidade!, Cep.Bairro, Cep.Endereco);

        return (contato, cep);
    }

    public static ContatoViewModel MapearContatoParaViewModel(Contato contato)
    {
        ArgumentNullException.ThrowIfNull(contato);

        var contatoViewModel = new ContatoViewModel
        {
            Id = contato.Id,
            Telefone = contato.Telefone,
            Cep = CepViewModel.MapearCepParaCepViewModel(contato.Cep),
            Email = contato.Email,
            ComplementoEndereco = contato.ComplementoEndereco,
            Nome = contato.Nome,
            NumeroEndereco = contato.NumeroEndereco
        };

        return contatoViewModel;
    }

    public static IEnumerable<ContatoViewModel> MapearContatoParaViewModel(IEnumerable<Contato> contatos)
    {
        var contatosViewModel = contatos.Select(p => new ContatoViewModel
        {
            Id = p.Id,
            Telefone = p.Telefone,
            Cep = CepViewModel.MapearCepParaCepViewModel(p.Cep),
            Email = p.Email,
            ComplementoEndereco = p.ComplementoEndereco,
            Nome = p.Nome,
            NumeroEndereco = p.NumeroEndereco
        });

        return contatosViewModel;
    }
}


