using Agenda.Domain.Ceps;
using Agenda.Infrastructure.DataAccess;
using Dapper;

namespace Agenda.Infrastructure.Repositories;
public class CepRepository : ICepRepository
{
    private readonly SqlConnectionFactory _connectionFactory;

    public CepRepository(SqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task InserirCepAsync(Cep cep)
    {
        using var connection = _connectionFactory.Create();
        const string sql =
"""
INSERT INTO Cep(Codigo, Estado, Cidade, Bairro, Endereco)
     VALUES (@Codigo, @Estado, @Cidade, @Bairro, @Endereco);
""";

        await connection.ExecuteAsync(sql, cep);
    }
}
