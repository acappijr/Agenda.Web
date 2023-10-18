using Agenda.Domain.Contatos;

namespace Agenda.Application.Services;
public interface IContatoService
{
    Task CadastrarContatoAsync(ContatoViewModel model);
    Task<IEnumerable<ContatoViewModel>> ObterContatosComCepAsync();
    Task<ContatoViewModel?> ObterContatoComCepAsync(int id);
    Task AlterarContatoAsync(ContatoViewModel model);
    Task DeletarContatoAsync(int id);
}