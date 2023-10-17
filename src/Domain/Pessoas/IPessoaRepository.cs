namespace Agenda.Domain.Pessoas;
public interface IPessoaRepository
{
    Task InserirPessoaAsync(Pessoa pessoa);
}
