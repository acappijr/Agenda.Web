using Agenda.Domain.Ceps;
using Agenda.Domain.Contatos;
using Agenda.Infrastructure.DataAccess;
using Dapper;

namespace Agenda.Infrastructure.Repositories;
public class ContatoRepository : IContatoRepository
{
    private readonly ISqlConnectionFactory _connectionFactory;

    public ContatoRepository(ISqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task AlterarContatoAsync(Contato contato)
    {
        ArgumentNullException.ThrowIfNull(contato);

        var param = new DynamicParameters();
        param.Add("@Id", contato.Id, dbType: System.Data.DbType.Int32);
        param.Add("@Nome", contato.Nome, dbType: System.Data.DbType.String, size: 80);
        param.Add("@Telefone", contato.Telefone, dbType: System.Data.DbType.String, size: 11);
        param.Add("@Email", contato.Email, dbType: System.Data.DbType.String, size: 255);
        param.Add("@CepId", contato.CepId, dbType: System.Data.DbType.Guid, size: 36);
        param.Add("@NumeroEndereco", contato.NumeroEndereco, dbType: System.Data.DbType.String, size: 15);
        param.Add("@ComplementoEndereco", contato.ComplementoEndereco, dbType: System.Data.DbType.String, size: 30);

        const string sql =
"""
UPDATE Contato
   SET Nome = @Nome, Telefone = @Telefone, Email = @Email, CepId = @CepId, NumeroEndereco = @NumeroEndereco, ComplementoEndereco = @ComplementoEndereco
 WHERE Id = @Id;
""";

        using var connection = _connectionFactory.Create();
        await connection.ExecuteAsync(sql, param);
    }

    public async Task DeletarContatoAsync(int id)
    {
        var param = new DynamicParameters();
        param.Add("@Id", id, dbType: System.Data.DbType.Int32);

        const string sql =
"""
DELETE FROM Contato
 WHERE Id = @Id;
""";

        using var connection = _connectionFactory.Create();
        await connection.ExecuteAsync(sql, param);
    }

    public async Task InserirContatoAsync(Contato contato)
    {
        ArgumentNullException.ThrowIfNull(contato);

        var param = new DynamicParameters();
        param.Add("@Nome", contato.Nome, dbType: System.Data.DbType.String, size: 80);
        param.Add("@Telefone", contato.Telefone, dbType: System.Data.DbType.String, size: 11);
        param.Add("@Email", contato.Email, dbType: System.Data.DbType.String, size: 255);
        param.Add("@CepId", contato.CepId, dbType: System.Data.DbType.Guid, size: 36);
        param.Add("@NumeroEndereco", contato.NumeroEndereco, dbType: System.Data.DbType.String, size: 15);
        param.Add("@ComplementoEndereco", contato.ComplementoEndereco, dbType: System.Data.DbType.String, size: 30);

        const string sql =
"""
INSERT INTO Contato(Nome, Telefone, Email, CepId, NumeroEndereco, ComplementoEndereco)
     VALUES (@Nome, @Telefone, @Email, @CepId, @NumeroEndereco, @ComplementoEndereco);
""";

        using var connection = _connectionFactory.Create();
        await connection.ExecuteAsync(sql, contato);
    }

    public async Task<Contato?> ObterContatoComCepAsync(int id)
    {
        var param = new DynamicParameters();
        param.Add("@Id", id, dbType: System.Data.DbType.Int32);

        const string sql =
"""
SELECT p.Id, p.Nome, p.Telefone, p.Email, p.CepId, p.NumeroEndereco, p.ComplementoEndereco,
       c.Codigo, c.Estado, c.Cidade, c.Bairro, c.Endereco
  FROM Contato p
 INNER JOIN Cep c on c.Id = p.CepId
 WHERE p.Id = @Id;
""";

        using var connection = _connectionFactory.Create();
        var contatos = await connection.QueryAsync<Contato, Cep, Contato>(sql, (contato, cep) =>
        {
            contato.Cep = cep;

            return contato;
        }, param, splitOn: "Codigo");

        return contatos?.FirstOrDefault();
    }

    public async Task<IEnumerable<Contato>> ObterContatosComCepAsync()
    {
        const string sql =
"""
SELECT p.Id, p.Nome, p.Telefone, p.Email, p.CepId, p.NumeroEndereco, p.ComplementoEndereco,
       c.Codigo, c.Estado, c.Cidade, c.Bairro, c.Endereco
  FROM Contato p
 INNER JOIN Cep c on c.Id = p.CepId;
""";

        using var connection = _connectionFactory.Create();
        var contatos = await connection.QueryAsync<Contato, Cep, Contato>(sql, (contato, cep) =>
        {
            contato.Cep = cep;

            return contato;
        }, splitOn: "Codigo");

        return contatos;
    }
}
