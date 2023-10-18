using Agenda.Domain.Ceps;
using Agenda.Infrastructure.DataAccess;
using Dapper;

namespace Agenda.Infrastructure.Repositories;
public class CepRepository : ICepRepository
{
    private readonly ISqlConnectionFactory _connectionFactory;

    public CepRepository(ISqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task InserirCepAsync(Cep cep)
    {
        ArgumentNullException.ThrowIfNull(cep);

        var param = new DynamicParameters();
        param.Add("@Id", cep.Id, dbType: System.Data.DbType.Guid, size: 36);
        param.Add("@Codigo", cep.Codigo, dbType: System.Data.DbType.String, size: 8);
        param.Add("@Estado", cep.Estado, dbType: System.Data.DbType.String, size: 2);
        param.Add("@Cidade", cep.Cidade, dbType: System.Data.DbType.String, size: 50);
        param.Add("@Bairro", cep.Bairro, dbType: System.Data.DbType.String, size: 90);
        param.Add("@Endereco", cep.Endereco, dbType: System.Data.DbType.String, size: 120);

        using var connection = _connectionFactory.Create();
        const string sql =
"""
INSERT INTO Cep(Id, Codigo, Estado, Cidade, Bairro, Endereco)
     VALUES (@Id, @Codigo, @Estado, @Cidade, @Bairro, @Endereco);
""";

        await connection.ExecuteAsync(sql, param);
    }

    public async Task<Cep?> ObterCepPorCodigoAsync(string codigo)
    {
        var param = new DynamicParameters();
        param.Add("@Codigo", codigo, dbType: System.Data.DbType.String, size: 8);

        const string sql =
"""
SELECT Id, Codigo, Estado, Cidade, Bairro, Endereco
  FROM Cep
 WHERE Codigo = @Codigo
""";

        using var connection = _connectionFactory.Create();
        var cep = await connection.QueryFirstOrDefaultAsync<Cep>(sql, param);

        return cep;
    }
}
