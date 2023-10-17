namespace Agenda.Domain.Pessoas;
public interface IPessoaRepository
{
    Task InserirPessoa(Pessoa pessoa);
}
