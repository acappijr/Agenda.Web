using Microsoft.Data.SqlClient;

namespace Agenda.Infrastructure.DataAccess;
public interface ISqlConnectionFactory
{
    SqlConnection Create();
}