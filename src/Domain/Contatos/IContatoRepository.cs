namespace Agenda.Domain.Contatos;
public interface IContatoRepository
{
    Task AlterarContatoAsync(Contato contato);
    Task DeletarContatoAsync(int id);
    Task InserirContatoAsync(Contato contato);
    Task<Contato?> ObterContatoComCepAsync(int id);
    Task<IEnumerable<Contato>> ObterContatosComCepAsync();
}
