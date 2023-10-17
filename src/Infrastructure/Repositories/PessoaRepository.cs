using Agenda.Domain.Pessoas;
using Agenda.Infrastructure.DataAccess;
using Dapper;

namespace Agenda.Infrastructure.Repositories;
public class PessoaRepository : IPessoaRepository
{
    private readonly SqlConnectionFactory _connectionFactory;

    public PessoaRepository(SqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task InserirPessoaAsync(Pessoa pessoa)
    {
        using var connection = _connectionFactory.Create();
        const string sql =
"""
INSERT INTO Pessoa(Nome, Telefone, Email, CepId, NumeroEndereco, ComplementoEndereco)
     VALUES (@Nome, @Telefone, @Email, @CepId, @NumeroEndereco, @ComplementoEndereco);
""";

        await connection.ExecuteAsync(sql, pessoa);
    }
}
